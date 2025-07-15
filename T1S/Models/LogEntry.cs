using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Models
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string? Message { get; set; }
        public LogLevel Level { get; set; } = LogLevel.INFO;
    }

    public enum LogLevel
    {
        INFO,
        WARN,
        ERRO
    }
}
