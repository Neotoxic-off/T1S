using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Dialogs
{
    public static class FileDialog
    {
        public static OpenFileDialog Setup(string title, string filter, string defaultDirectory)
        {
            OpenFileDialog? fileDialog = new OpenFileDialog()
            {
                Title = title,
                Filter = filter,
                InitialDirectory = defaultDirectory
            };

            return fileDialog;
        }
    }
}
