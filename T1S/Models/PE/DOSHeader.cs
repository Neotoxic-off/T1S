using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Models.PE
{
    public class DOSHeader
    {
        public ushort e_magic { get; set; }
        public ushort e_cblp { get; set; }
        public ushort e_cp { get; set; }
        public ushort e_crlc { get; set; }
        public ushort e_cparhdr { get; set; }
        public ushort e_minalloc { get; set; }
        public ushort e_maxalloc { get; set; }
        public ushort e_ss { get; set; }
        public ushort e_sp { get; set; }
        public ushort e_csum { get; set; }
        public ushort e_ip { get; set; }
        public ushort e_cs { get; set; }
        public ushort e_lfarlc { get; set; }
        public ushort e_ovno { get; set; }
        public ushort[] e_res1 { get; set; } = new ushort[4];
        public ushort e_oemid { get; set; }
        public ushort e_oeminfo { get; set; }
        public ushort[] e_res2 { get; set; } = new ushort[10];
        public int e_lfanew { get; set; }
    }
}
