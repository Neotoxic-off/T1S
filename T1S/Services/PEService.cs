using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using T1S.Models.PE;

namespace T1S.Services
{
    public class PEService
    {
        public static PEFile Load(string path)
        {
            using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using BinaryReader reader = new BinaryReader(stream);
            PEFile file = new PEFile { FilePath = path };

            file.DOSHeader = ReadDosHeader(reader);
            ValidatePESignature(reader, file.DOSHeader.e_lfanew);
            ReadCoffHeader(reader, file);
            file.OptionalHeader = ReadOptionalHeader(reader);
            file.Sections = ReadSectionHeaders(reader, file.NumberOfSections);

            return file;
        }

        private static DOSHeader ReadDosHeader(BinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            return new DOSHeader
            {
                e_magic = reader.ReadUInt16(),
                e_cblp = reader.ReadUInt16(),
                e_cp = reader.ReadUInt16(),
                e_crlc = reader.ReadUInt16(),
                e_cparhdr = reader.ReadUInt16(),
                e_minalloc = reader.ReadUInt16(),
                e_maxalloc = reader.ReadUInt16(),
                e_ss = reader.ReadUInt16(),
                e_sp = reader.ReadUInt16(),
                e_csum = reader.ReadUInt16(),
                e_ip = reader.ReadUInt16(),
                e_cs = reader.ReadUInt16(),
                e_lfarlc = reader.ReadUInt16(),
                e_ovno = reader.ReadUInt16(),
                e_res1 = new ushort[]
                {
                    reader.ReadUInt16(),
                    reader.ReadUInt16(),
                    reader.ReadUInt16(),
                    reader.ReadUInt16()
                },
                e_oemid = reader.ReadUInt16(),
                e_oeminfo = reader.ReadUInt16(),
                e_res2 = new ushort[]
                {
                    reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadUInt16(),
                    reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadUInt16()
                },
                e_lfanew = reader.ReadInt32()
            };
        }

        private static void ValidatePESignature(BinaryReader reader, int peHeaderOffset)
        {
            reader.BaseStream.Seek(peHeaderOffset, SeekOrigin.Begin);
            uint signature = reader.ReadUInt32();

            if (signature != 0x4550) // "PE\0\0"
                throw new InvalidDataException("Invalid PE signature.");
        }

        private static void ReadCoffHeader(BinaryReader reader, PEFile file)
        {
            ushort machine = reader.ReadUInt16();
            file.Machine = machine switch
            {
                0x014c => PEFile.Architecture.x86,
                0x8664 => PEFile.Architecture.x64,
                0x01c4 => PEFile.Architecture.ARM,
                0x01c2 => PEFile.Architecture.ARM64,
                _ => PEFile.Architecture.Unknown
            };

            file.NumberOfSections = reader.ReadUInt16();
            file.TimeDateStamp = reader.ReadUInt32();

            reader.BaseStream.Seek(12, SeekOrigin.Current); // Skip PointerToSymbolTable, NumberOfSymbols, SizeOfOptionalHeader, Characteristics
        }

        private static byte[] ReadOptionalHeader(BinaryReader reader)
        {
            ushort magic = reader.ReadUInt16();
            bool isPE32Plus = magic == 0x20b;
            int size = isPE32Plus ? 240 : 224;

            reader.BaseStream.Seek(-2, SeekOrigin.Current); // rewind to include magic in full read
            return reader.ReadBytes(size);
        }

        private static List<PESection> ReadSectionHeaders(BinaryReader reader, int count)
        {
            List<PESection> sections = new List<PESection>();

            for (int i = 0; i < count; i++)
            {
                PESection section = new PESection
                {
                    Name = Encoding.UTF8.GetString(reader.ReadBytes(8)).TrimEnd('\0'),
                    VirtualSize = reader.ReadUInt32(),
                    VirtualAddress = reader.ReadUInt32(),
                    SizeOfRawData = reader.ReadUInt32(),
                    PointerToRawData = reader.ReadUInt32()
                };

                reader.BaseStream.Seek(16, SeekOrigin.Current); // Skip rest
                sections.Add(section);
            }

            return sections;
        }
    }
}
