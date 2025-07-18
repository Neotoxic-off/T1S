using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Models.Windows
{
    public class AntiDebugWindowsApi
    {
        public string? Name { get; set; }
        public bool? IsPresent { get; set; } = false;
        public string? Description { get; set; }
    }
}
