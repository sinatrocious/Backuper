using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backuper
{
    public class ViewModelEntry : NotifyPropertyChanged
    {
        public bool IsEnabled
        {
            get => Entry.IsEnabled;
            set
            {
                Entry.IsEnabled = value;
                OnPropertyChanged();
            }
        }

        public int Interval
        {
            get => Entry.Interval;
            set
            {
                if (value < 1)
                    throw new ArgumentException("Can't be less than 1 minute", nameof(Interval));
                Entry.Interval = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => Entry.Name;
            set
            {
                Entry.Name = value;
                OnPropertyChanged();
            }
        }

        public string MonitorPath
        {
            get => Entry.MonitorPath;
            set
            {
                Entry.MonitorPath = value;
                OnPropertyChanged();
            }
        }

        public string BackupPath
        {
            get => Entry.BackupPath;
            set
            {
                Entry.BackupPath = value;
                OnPropertyChanged();
            }
        }

        public string Ignore
        {
            get => Entry.Ignore;
            set
            {
                Entry.Ignore = value;
                OnPropertyChanged();
            }
        }

        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                CommandBackup.Update();
            }
        }

        public Entry Entry { get; }

        public DelegateCommand CommandMonitorPath { get; }
        public DelegateCommand CommandBackupPath { get; }
        public DelegateCommand CommandBackup { get; }

        int _minutesSinceLastCheck;

        public ViewModelEntry(Entry entry)
        {
            Entry = entry;
            CommandMonitorPath = new DelegateCommand(o =>
            {
                // TODO: dialog service
                using var dialog = new FolderBrowserDialog()
                {
                    SelectedPath = MonitorPath,
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                    MonitorPath = dialog.SelectedPath;
            });
            CommandBackupPath = new DelegateCommand(o =>
            {
                // TODO: dialog service
                using var dialog = new FolderBrowserDialog
                {
                    SelectedPath = BackupPath,
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                    BackupPath = dialog.SelectedPath;
            });
            CommandBackup = new DelegateCommand(o => Backup(), o => !IsBusy);
        }

        public void Check()
        {
            if (++_minutesSinceLastCheck >= Interval)
                Backup();
        }

        public async void Backup()
        {
            IsBusy = true;
            Backup backup = new(Entry, new FileOperationsProvider(), new ZipOperationsProvider());
            await Task.Run(() =>
            {
                try
                {
                    backup.Work();
                }
                catch(Exception e)
                {
                    // TODO: exception handling and logging
                    System.Windows.MessageBox.Show(e.ToString(), "Backuper error");
                }
            });
            await Task.Delay(500); // user awareness
            IsBusy = false;
            _minutesSinceLastCheck = 0;
        }
    }
}
