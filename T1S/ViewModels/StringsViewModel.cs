using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace T1S.ViewModels
{
    public partial class StringViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> pagedStrings = new ObservableCollection<string>();

        [ObservableProperty]
        private int _currentPage = 0;

        [ObservableProperty]
        private int _pageSize = 100;

        private List<string> _allStrings = new List<string>();

        public int TotalPages => (_allStrings.Count + PageSize - 1) / PageSize;

        public void SetStrings(IEnumerable<string> all)
        {
            _allStrings = all.ToList();
            CurrentPage = 0;
            UpdatePagedStrings();
        }

        [RelayCommand(CanExecute = nameof(CanGoNext))]
        public void NextPage()
        {
            if (CanGoNext())
            {
                CurrentPage++;
            }
        }

        [RelayCommand(CanExecute = nameof(CanGoPrevious))]
        public void PreviousPage()
        {
            if (CanGoPrevious())
            {
                CurrentPage--;
            }
        }

        [RelayCommand]
        public void Dump()
        {
            System.IO.BinaryWriter reader = new System.IO.BinaryWriter(
                System.IO.File.OpenWrite(Utils.Constants.Files.STRINGS_FILE)
            );

            foreach (string str in _allStrings)
            {
                reader.Write(str);
                reader.Write(Environment.NewLine);
            }

            reader.Close();
        }

        private void UpdatePagedStrings()
        {
            List<string> items = _allStrings
                .Skip(CurrentPage * PageSize)
                .Take(PageSize)
                .ToList();

            PagedStrings.Clear();

            foreach (string item in items)
            {
                PagedStrings.Add(item);
            }

            OnPropertyChanged(nameof(TotalPages));
            NextPageCommand.NotifyCanExecuteChanged();
            PreviousPageCommand.NotifyCanExecuteChanged();
        }

        private bool CanGoNext() => CurrentPage < TotalPages - 1;
        private bool CanGoPrevious() => CurrentPage > 0;

        partial void OnCurrentPageChanged(int oldValue, int newValue)
        {
            UpdatePagedStrings();
        }
    }
}
