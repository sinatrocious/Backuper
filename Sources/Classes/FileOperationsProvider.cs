using System;
using System.IO;

namespace Backuper
{
    public class FileOperationsProvider : IFileOperationsProvider
    {
        public string[] GetFiles(string path) =>
            Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        public DateTime GetLastWriteTime(string file) =>
            File.GetLastWriteTime(file);

        public void FileMove(string sourceFile, string targetFile)
            => File.Move(sourceFile, targetFile);
    }
}
