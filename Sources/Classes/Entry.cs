namespace Backuper
{
    public class Entry
    {
        public bool IsEnabled { get; set; }
        public int Interval { get; set; } = 10;
        public ulong LastHash { get; set; }
        public string Name { get; set; } = "";
        public string MonitorPath { get; set; } = "";
        public string BackupPath { get; set; } = "";
        public string Ignore { get; set; } = "";
    }
}
