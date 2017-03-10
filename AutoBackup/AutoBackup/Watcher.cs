using System;
using System.Configuration;
using System.IO;

namespace AutoBackup
{
    internal sealed class FileWatcher
    {
        string TargetFolder;
        string BackupDirectory;
        FileSystemWatcher Watcher;
        
        public FileWatcher()
        {
            TargetFolder = string.Format($@"{ConfigurationManager.AppSettings["FolderToBackup"]}");
            BackupDirectory = string.Format($@"{ConfigurationManager.AppSettings["FolderToBackupTo"]}");
        }
        
        internal void Configure()
        {
            if (TargetFolder == "NoneSpecified")
                throw new TargetFolderNotSpecifiedException();
            Watcher = new FileSystemWatcher(TargetFolder);
            Watcher.Changed += new FileSystemEventHandler(Backup);
            Watcher.Created += new FileSystemEventHandler(Backup);
            Watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
        }
        
        internal void BeginWatching()
        {
            Watcher.EnableRaisingEvents = true;
            Console.WriteLine($"Monitoring and backing up {TargetFolder}\n");
        }

        private void Backup(object sender, FileSystemEventArgs e)
        {            
            string fileName = e.FullPath.Substring(TargetFolder.Length + 1);
            if (fileName.EndsWith(".tmp") || fileName.Contains("~"))
                return;

            try
            {
                File.Copy(e.FullPath, Path.Combine(BackupDirectory, fileName), true);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Unable to copy {fileName} as it no longer exists.");
            }
        }
    }
}
