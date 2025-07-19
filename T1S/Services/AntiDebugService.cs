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
        public static ObservableCollection<AntiDebugWindowsApi> Scan(ObservableCollection<string> binaryStrings)
        {
            List<AntiDebugWindowsApi> results = new List<AntiDebugWindowsApi>();

            foreach (var api in WindowsAPI.AntiDebugAPI)
            {
                results.Add(new AntiDebugWindowsApi
                {
                    Name = api.Name,
                    IsPresent = binaryStrings.Any(str => str.Contains(api?.Name, StringComparison.OrdinalIgnoreCase))
                });
            }

            return new ObservableCollection<AntiDebugWindowsApi>(results);
        }
    }
}
