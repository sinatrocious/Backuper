using System;

namespace Backuper
{
    public interface IZipOperationsProvider
    {
        void Create(string file);
        void Close();
        void AddEntry(string file, string entry);
    }
}
