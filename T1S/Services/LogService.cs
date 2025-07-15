using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace T1S.Services
{

    public class LogService
    {
        private static readonly Lazy<LogService> _instance = new(() => new LogService());
        public static LogService Instance => _instance.Value;

        public ObservableCollection<Models.LogEntry> Logs { get; } = new();

        public event Action<Models.LogEntry>? LogAdded;

        private LogService() { }

        public void Log(string message, Models.LogLevel level = Models.LogLevel.Info)
        {
            var entry = new Models.LogEntry { Message = message, Level = level };
            App.Current.Dispatcher.Invoke(() =>
            {
                Logs.Add(entry);
                LogAdded?.Invoke(entry);
            });
        }
    }

}
