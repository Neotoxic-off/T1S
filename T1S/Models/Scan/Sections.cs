using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Models.Scan
{
    public class Sections
    {
        public Section text { get; set; } = new Section();
        public Section bss { get; set; } = new Section();
        public Section rdata { get; set; } = new Section();
        public Section edata { get; set; } = new Section();
        public Section idata { get; set; } = new Section();
        public Section reloc { get; set; } = new Section();
        public Section rsrc { get; set; } = new Section();
        public Section tls { get; set; } = new Section();
    }
}
