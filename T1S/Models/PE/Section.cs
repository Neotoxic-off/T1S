using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Models.PE
{
    public class Section
    {
        public string Name { get; set; } = string.Empty;
        public uint VirtualAddress { get; set; }
        public uint VirtualSize { get; set; }
    }
}
