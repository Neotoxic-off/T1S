using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Models.PE
{
    public class HeaderInfo
    {
        public string? FilePath { get; set; }
        public bool IsValidPE { get; set; } = true;
        public ushort NumberOfSections { get; set; }
        public uint TimeDateStamp { get; set; }
        public uint EntryPointRVA { get; set; }
        public string? EntryPointSection { get; set; }
        public List<Section> Sections { get; set; } = new List<Section>();
    }
}
