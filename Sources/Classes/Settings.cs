using System.Collections.Generic;

namespace Backuper
{
    public partial class Settings
    {
        public bool IsMaximized { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double SplitterWidth { get; set; }
        public List<Entry> Entries { get; set; } = new List<Entry>();
    }
}
