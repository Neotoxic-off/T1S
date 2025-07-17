using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using T1S.Models.Windows;
using T1S.Utils.Constants;

namespace T1S.Services
{
    public static class AntiDebugService
    {
        public static ObservableCollection<AntiDebugWindowsApi> Scan(List<string> binaryStrings)
        {
            List<AntiDebugWindowsApi> detections = new List<AntiDebugWindowsApi>();

            foreach (string str in binaryStrings)
            {
                detections.AddRange(
                    WindowsAPI.AntiDebugAPI.Where(
                        api => str.Contains(api?.Name, StringComparison.OrdinalIgnoreCase)
                    ).ToList()
                );
            }

            return new ObservableCollection<AntiDebugWindowsApi>(detections);
        }
    }
}
