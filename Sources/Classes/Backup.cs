using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Backuper
{
    public class Backup
    {
        readonly Entry _entry;
        readonly IFileOperationsProvider _fileOperationsProvider;
        readonly IZipOperationsProvider _zipOperationsProvider;

        public Backup(Entry entry, IFileOperationsProvider fileOperationsProvider, IZipOperationsProvider zipOperationsProvider)
        {
            _entry = entry;
            _fileOperationsProvider = fileOperationsProvider;
            _zipOperationsProvider = zipOperationsProvider;
        }

        public void Work()
        {
            if (_entry.LastHash != CalculateHash())
            {
                Zip();
                _entry.LastHash = CalculateHash();
            }
        }

        ulong CalculateHash()
        {
            var files = GetFiles();
            // add modify time
            files = files.Select(o => o + _fileOperationsProvider.GetLastWriteTime(o)).ToArray();
            // compute md5 hash as uint from string
            using var md5 = MD5.Create();
            return BitConverter.ToUInt64(md5.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(files))));
        }

        void Zip()
        {
            // zip using tmp-file
            var tmp = Path.GetTempFileName();
            try
            {
                _zipOperationsProvider.Create(tmp);
                foreach (var file in GetFiles())
                    _zipOperationsProvider.AddEntry(file, Path.GetRelativePath(_entry.MonitorPath, file));
            }
            finally
            {
                _zipOperationsProvider.Close();
            }
            _fileOperationsProvider.FileMove(tmp, Path.Combine(_entry.BackupPath, $"{DateTime.Now:yyMMddHHmmss}.zip"));
        }

        string[] GetFiles()
        {
            var files = _fileOperationsProvider.GetFiles(_entry.MonitorPath);
            // remove ignored
            var ignored = _entry.Ignore.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(o => Path.Combine(_entry.MonitorPath, o));
            return files.Where(file => !ignored.Any(o => file.StartsWith(o))).ToArray();
        }
    }
}
