using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using T1S.Models.Windows;
using T1S.Services;
using T1S.Utils;
using T1S.Utils.Constants;

namespace T1S.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _binaryPath;

        [ObservableProperty]
        private ObservableCollection<AntiDebugWindowsApi>? antiDebugWindowsApis;

        [ObservableProperty]
        private Dictionary<string, double>? _entropy;

        public MainViewModel()
        {
        }

        [RelayCommand]
        private Task Scan()
        {
            SetFile();
            GetAntiDebugWindowsApis();

            return Task.CompletedTask;
        }

        private void SetFile()
        {
            OpenFileDialog? fileDialog = Dialogs.FileDialog.Setup(
                "Select a binary file",
                "Executable Files (*.exe;*.dll)|*.exe;*.dll|All Files (*.*)|*.*",
                Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86)
            );
            fileDialog.ShowDialog();

            BinaryPath = fileDialog.FileName;
        }

        private void GetAntiDebugWindowsApis()
        {
            List<string>? strings = null;
            Models.PE.PEFile? peFile = null;

            if (BinaryPath != null)
            {
                peFile = PEService.Load(BinaryPath);
                Entropy = Utils.Entropy.GetSectionEntropies(BinaryPath, peFile.Sections);
                strings = StringService.Extract(BinaryPath);
                AntiDebugWindowsApis = AntiDebugService.Scan(strings);
            }
        }
    }
}
