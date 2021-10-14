using System;

namespace Backuper
{
    public interface IFileOperationsProvider
    {
        string[] GetFiles(string path);
        DateTime GetLastWriteTime(string file);
        void FileMove(string sourceFile, string targetFile);
    }
}
