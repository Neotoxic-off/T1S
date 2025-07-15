using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Utils.Constants
{
    public static class WindowsAPI
    {
        public static readonly string[] AntiDebugAPI = new[]
        {
            "IsDebuggerPresent",
            "CheckRemoteDebuggerPresent",
            "NtQueryInformationProcess",
            "ZwQueryInformationProcess",
            "OutputDebugStringA",
            "OutputDebugStringW",
            "GetTickCount",
            "QueryPerformanceCounter"
        };
    }
}
