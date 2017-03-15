using System.IO;

namespace AutoBackup
{
    internal class FileWatcher : IFileWatcher
    {
        public FileSystemWatcher GetWatcher
        {
            get
            {
                return new FileSystemWatcher();
            }
        }
    }
}
