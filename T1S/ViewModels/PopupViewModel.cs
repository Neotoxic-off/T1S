using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace T1S.ViewModels
{
    public partial class PopupViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _text = String.Empty;

        [ObservableProperty]
        private string _title = String.Empty;

        [RelayCommand]
        private void Close()
        {
            Application.Current.Windows
                .OfType<Windows.PopupWindow>()
                .FirstOrDefault()?
                .Close();
        }
    }
}
