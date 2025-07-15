using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Utils.Constants
{
    public static class PEConstants
    {
        public const int DOS_E_LFANEW_OFFSET = 0x3C;
        public const uint PE_SIGNATURE = 0x00004550;

        public const ushort PE32 = 0x10b;
        public const ushort PE32_PLUS = 0x20b;

        public const int SIZEOF_NT_HEADERS_PE32 = 0xE0;
        public const int SIZEOF_NT_HEADERS_PE32_PLUS = 0xF8;

        public const int COFF_MACHINE_SIZE = 2;
        public const int COFF_SKIP_TO_OPTIONAL = 16;

        public const int ENTRYPOINT_OFFSET_PE32 = 10;
        public const int ENTRYPOINT_OFFSET_PE32_PLUS = 14;

        public const int SECTION_NAME_SIZE = 8;
    }
}
