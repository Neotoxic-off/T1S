using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1S.Models.PE;

namespace T1S.Services
{
    public class PEService
    {
        public static PEFile Load(string path)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(stream);

            var file = new PEFile { FilePath = path };

            // === DOS Header ===
            stream.Seek(0x3C, SeekOrigin.Begin);
            int peHeaderOffset = reader.ReadInt32();

            // === PE Signature ===
            stream.Seek(peHeaderOffset, SeekOrigin.Begin);
            if (reader.ReadUInt32() != 0x4550) // "PE\0\0"
                throw new InvalidDataException("Invalid PE file");

            // === COFF Header ===
            reader.BaseStream.Seek(2, SeekOrigin.Current); // Skip Machine
            file.NumberOfSections = reader.ReadUInt16();
            file.TimeDateStamp = reader.ReadUInt32();
            reader.BaseStream.Seek(12, SeekOrigin.Current); // Skip rest

            // === Optional Header ===
            ushort magic = reader.ReadUInt16();
            bool isPE32Plus = magic == 0x20b;
            int optHeaderSize = isPE32Plus ? 240 : 224;
            reader.BaseStream.Seek(-2, SeekOrigin.Current); // Rewind for full read
            file.OptionalHeader = reader.ReadBytes(optHeaderSize);

            // === Section Headers ===
            for (int i = 0; i < file.NumberOfSections; i++)
            {
                var section = new PESection();
                section.Name = Encoding.UTF8.GetString(reader.ReadBytes(8)).TrimEnd('\0');
                section.VirtualSize = reader.ReadUInt32();
                section.VirtualAddress = reader.ReadUInt32();
                section.SizeOfRawData = reader.ReadUInt32();
                section.PointerToRawData = reader.ReadUInt32();
                reader.BaseStream.Seek(16, SeekOrigin.Current); // Skip rest
                file.Sections.Add(section);
            }

            return file;
        }
    }
}
