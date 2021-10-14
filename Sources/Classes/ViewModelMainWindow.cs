using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace Backuper
{
    public class ViewModelMainWindow : NotifyPropertyChanged
    {
        bool _isShown = true;
        public bool IsShown
        {
            get => _isShown;
            set
            {
                _isShown = value;
                OnPropertyChanged();
                CommandShow.Update();
            }
        }

        public bool? IsMaximized
        {
            get => App.Settings.IsMaximized;
            set
            {
                // minimized
                if (value == null)
                {
                    // using dispatcher queue to call the code after setter is completed
                    App.Current.Dispatcher.InvokeAsync(() =>
                    {
#pragma warning disable CA2011
                        // refresh (by changing to opposite and then back) the source value for binding
                        IsMaximized = !IsMaximized;
                        IsMaximized = !IsMaximized;
#pragma warning restore CA2011
                        IsShown = false;
                    });
                }
                else
                {
                    App.Settings.IsMaximized = (bool)value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ViewModelEntry> Entries { get; } =
            new ObservableCollection<ViewModelEntry>(App.Settings.Entries.Select(o => new ViewModelEntry(o)));

        ViewModelEntry? _selectedEntry;
        public ViewModelEntry? SelectedEntry
        {
            get => _selectedEntry;
            set
            {
                _selectedEntry = value;
                OnPropertyChanged();
                CommandDelete.Update();
            }
        }

        public DelegateCommand CommandShow { get; }
        public DelegateCommand CommandExit { get; }
        public DelegateCommand CommandNew { get; }
        public DelegateCommand CommandDelete { get; }

        public ViewModelMainWindow()
        {
            CommandExit = new DelegateCommand(o => App.Current.Shutdown());
            CommandShow = new DelegateCommand(o => IsShown = true, o => !IsShown);
            CommandNew = new DelegateCommand(o =>
            {
                var entry = new Entry { Name = "New entry" };
                App.Settings.Entries.Add(entry);
                var item = new ViewModelEntry(entry);
                Entries.Add(item);
                SelectedEntry = item;
            });
            CommandDelete = new DelegateCommand(o =>
            {
                // TODO: confirmation
                // null check
                if (SelectedEntry is ViewModelEntry selected)
                {
                    App.Settings.Entries.Remove(selected.Entry);
                    Entries.Remove(selected);
                }
            }, o => SelectedEntry != null);
            // polling timer
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1),
                IsEnabled = true,
            };
            timer.Tick += (s, e) =>
            {
                foreach (var entry in Entries.Where(o => o.IsEnabled))
                    entry.Check();
            };
        }
    }
}
