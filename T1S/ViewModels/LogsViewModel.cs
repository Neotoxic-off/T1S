
using T1S.Models;
using T1S.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.ViewModels
{
    public partial class LogsViewModel : ObservableObject
    {
        public ObservableCollection<LogEntry> Logs => LogService.Instance.Logs;

        public LogsViewModel()
        {
            LogService.Instance.LogAdded += OnLogAdded;
        }

        private void OnLogAdded(LogEntry entry)
        {
        }
    }
}
