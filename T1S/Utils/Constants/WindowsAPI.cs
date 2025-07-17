using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1S.Models.Windows;

namespace T1S.Utils.Constants
{
    public static class WindowsAPI
    {
        public static readonly AntiDebugWindowsApi[] AntiDebugAPI = new[]
        {
            new AntiDebugWindowsApi
            {
                Name = "IsDebuggerPresent",
                Description = "Checks if the current process is being debugged"
            },
            new AntiDebugWindowsApi
            {
                Name = "CheckRemoteDebuggerPresent",
                Description = "Determines whether a specified process is being debugged by checking debug flags"
            },
            new AntiDebugWindowsApi
            {
                Name = "NtQueryInformationProcess",
                Description = "Queries various information about a process, such as debug port and debug flags, often used for stealthy debugger detection"
            },
            new AntiDebugWindowsApi
            {
                Name = "ZwQueryInformationProcess",
                Description = "Similar to NtQueryInformationProcess; used to retrieve process information for detecting debugging in low-level operations"
            },
            new AntiDebugWindowsApi
            {
                Name = "OutputDebugStringA",
                Description = "Sends an ANSI string to the debugger for output; used to detect debugger behavior by monitoring execution flow or timing"
            },
            new AntiDebugWindowsApi
            {
                Name = "OutputDebugStringW",
                Description = "Sends a wide-character (Unicode) string to the debugger for output; used in the same way as OutputDebugStringA for anti-debugging"
            },
            new AntiDebugWindowsApi
            {
                Name = "GetTickCount",
                Description = "Returns the number of milliseconds since system start; often used to detect execution delays caused by debugging"
            },
            new AntiDebugWindowsApi
            {
                Name = "QueryPerformanceCounter",
                Description = "Retrieves a high-resolution performance counter used for precise timing; helps detect single-stepping or breakpoint pauses"
            }
        };
    }
}
