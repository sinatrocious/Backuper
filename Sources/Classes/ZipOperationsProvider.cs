using System;
using System.IO.Compression;

namespace Backuper
{
    public class ZipOperationsProvider : IZipOperationsProvider
    {
        ZipArchive? _zipArchive;

        public void Create(string file)
        {
            _zipArchive?.Dispose();
            _zipArchive = ZipFile.Open(file, ZipArchiveMode.Update);
        }

        public void Close()
        {
            _zipArchive?.Dispose();
            _zipArchive = null;
        }

        public void AddEntry(string file, string entry)
        {
            if (_zipArchive == null)
                throw new InvalidOperationException("Call Create() first");
            _zipArchive.CreateEntryFromFile(file, entry);
        }
    }
}
