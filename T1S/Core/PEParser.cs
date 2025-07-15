using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows;
    using T1S.Models;
    using T1S.Models.PE;
    using T1S.Utils.Constants;

    public class PEParser
    {
        public HeaderInfo? Parse(string path)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(stream);
            int peHeaderOffset = ReadPEHeaderOffset(reader);
            HeaderInfo info = new HeaderInfo { FilePath = path };

            info.IsValidPE = IsValidPESignature(reader, peHeaderOffset);
            if (info.IsValidPE == true)
            {
                ReadCOFFHeader(reader, info);
                ReadSectionHeaders(reader, stream, info, peHeaderOffset);
                info.EntryPointSection = ResolveSectionName(info, info.EntryPointRVA);
            }

            return info;
        }

        private int ReadPEHeaderOffset(BinaryReader reader)
        {
            reader.BaseStream.Seek(PEConstants.DOS_E_LFANEW_OFFSET, SeekOrigin.Begin);

            return reader.ReadInt32();
        }

        private bool IsValidPESignature(BinaryReader reader, int offset)
        {
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);

            if (reader.ReadUInt32() != PEConstants.PE_SIGNATURE)
            {
                return false;
            }

            return true;
        }

        private void ReadCOFFHeader(BinaryReader reader, HeaderInfo info)
        {
            reader.BaseStream.Seek(PEConstants.COFF_MACHINE_SIZE, SeekOrigin.Current);
            info.NumberOfSections = reader.ReadUInt16();
            info.TimeDateStamp = reader.ReadUInt32();
            reader.BaseStream.Seek(PEConstants.COFF_SKIP_TO_OPTIONAL, SeekOrigin.Current);
        }

        private int GetEntryOffset(bool isPE32Plus)
        {
            if (isPE32Plus)
            {
                return PEConstants.ENTRYPOINT_OFFSET_PE32_PLUS;
            }

            return PEConstants.ENTRYPOINT_OFFSET_PE32;
        }

        private bool ReadOptionalHeader(BinaryReader reader, HeaderInfo info)
        {
            bool isPE32Plus = (reader.ReadUInt16() == PEConstants.PE32_PLUS);
            int entryOffset = GetEntryOffset(isPE32Plus);

            reader.BaseStream.Seek(entryOffset, SeekOrigin.Current);
            info.EntryPointRVA = reader.ReadUInt32();

            return isPE32Plus;
        }

        private void ReadSectionHeaders(BinaryReader reader, Stream stream, HeaderInfo info, int peHeaderOffset)
        {
            bool isPE32Plus = ReadOptionalHeader(reader, info);
            int sectionHeaderOffset = peHeaderOffset +
                                      (isPE32Plus ? PEConstants.SIZEOF_NT_HEADERS_PE32_PLUS
                                                  : PEConstants.SIZEOF_NT_HEADERS_PE32);

            stream.Seek(sectionHeaderOffset, SeekOrigin.Begin);

            for (int i = 0; i < info.NumberOfSections; i++)
            {
                string name = Encoding.UTF8.GetString(reader.ReadBytes(PEConstants.SECTION_NAME_SIZE)).TrimEnd('\0');
                uint virtualSize = reader.ReadUInt32();
                uint virtualAddress = reader.ReadUInt32();

                reader.BaseStream.Seek(4 * 3, SeekOrigin.Current); // Skip raw sizes
                reader.ReadBytes(4 * 2); // Skip rest

                info.Sections.Add(new Section
                {
                    Name = name,
                    VirtualAddress = virtualAddress,
                    VirtualSize = virtualSize
                });
            }
        }


        private string ResolveSectionName(HeaderInfo info, uint rva)
        {
            foreach (var section in info.Sections)
            {
                if (rva >= section.VirtualAddress && rva < section.VirtualAddress + section.VirtualSize)
                    return section.Name;
            }

            return "Unknown";
        }
    }
}
