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
using T1S.Models.PE;
using T1S.Models.Scan;
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
        private ObservableCollection<AntiDebugWindowsApi>? _antiDebugWindowsApis;

        [ObservableProperty]
        private Sections? _sections = new Sections();

        [ObservableProperty]
        private ObservableCollection<string>? _strings = new ObservableCollection<string>();

        [ObservableProperty]
        private PEFile? _peFileScan;

        [ObservableProperty]
        private StringViewModel stringViewModel = new();

        public MainViewModel()
        {
        }

        [RelayCommand]
        private void Browse()
        {
            SetFile();
        }

        [RelayCommand]
        private Task Scan()
        {
            if (string.IsNullOrEmpty(BinaryPath) == false)
            {
                LogService.Instance.Log($"{Logs.LOG_SCAN_STARTED}");

                LoadPE();
                LoadStrings();
                GetAntiDebugWindowsApis();

                LogService.Instance.Log($"{Logs.LOG_SCAN_COMPLETED}");
            }

            return Task.CompletedTask;
        }

        private void SetFile()
        {
            OpenFileDialog? fileDialog = Dialogs.FileDialog.Setup(
                "Select a binary file",
                "Executable Files (*.exe;*.dll)|*.exe;*.dll|All Files (*.*)|*.*",
                Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86)
            );
            bool? success = fileDialog.ShowDialog();

            if (success == true)
            {
                BinaryPath = fileDialog.FileName;
                LogService.Instance.Log($"{Logs.LOG_BINARY_SET} '{BinaryPath}'");
            } else
            {
                BinaryPath = null;
                LogService.Instance.Log($"{Logs.LOG_BINARY_NULLED}", Models.LogLevel.WARN);
            }
        }

        private void LoadStrings()
        {
            Strings = StringService.Extract(BinaryPath);
            StringViewModel.SetStrings(Strings);
        }

        private void GetAntiDebugWindowsApis()
        {
            AntiDebugWindowsApis = AntiDebugService.Scan(Strings);
        }

        private void LoadPE()
        {
            PeFileScan = PEService.Load(BinaryPath);
            Sections = Entropy.GetSectionEntropies(BinaryPath, PeFileScan.Sections);
        }
    }
}
