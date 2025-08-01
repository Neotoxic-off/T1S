﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Models.PE
{
    public class PEFile
    {
        public enum Architecture
        {
            Unknown,
            x86,
            x64,
            ARM,
            ARM64
        }

        public string FilePath { get; set; } = "";
        public int NumberOfSections { get; set; }
        public uint TimeDateStamp { get; set; }
        public Architecture Machine { get; set; } = Architecture.Unknown;
        public byte[] OptionalHeader { get; set; } = Array.Empty<byte>();
        public List<PESection> Sections { get; set; } = new();
        public DOSHeader DOSHeader { get; set; } = new DOSHeader();
    }
}
