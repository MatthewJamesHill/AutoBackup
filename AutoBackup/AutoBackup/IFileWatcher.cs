using System.IO;

namespace AutoBackup
{
    interface IFileWatcher
    {
        FileSystemWatcher GetWatcher { get; }
    }
}
