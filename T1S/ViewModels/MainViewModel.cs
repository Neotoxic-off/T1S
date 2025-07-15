using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using T1S.Core;
using T1S.Models.PE;
using T1S.Utils;

namespace T1S.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            string filePath = @"C:\Program Files (x86)\Steam\steamapps\common\Apex Legends\r5apex_dx12.exe";

            var parser = new PEParser();
            HeaderInfo info = parser.Parse(filePath);
            Services.AntiDebugService.Scan(filePath);
        }
    }
}
