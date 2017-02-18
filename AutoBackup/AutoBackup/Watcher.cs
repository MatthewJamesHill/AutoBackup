using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        internal void RegisterTargetFolder()
        {
            if (TargetFolder == "NoneSpecified")
                throw new TargetFolderNotSpecifiedException();
            Watcher = new FileSystemWatcher(TargetFolder);
        }

        internal void RegisterEvents()
        {            
            Watcher.Changed += new FileSystemEventHandler(Backup);
            Watcher.Created += new FileSystemEventHandler(Backup);
        }

        internal void RegisterFilters()
        {
            Watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
        }

        internal void BeginWatching()
        {
            Watcher.EnableRaisingEvents = true;
            Console.WriteLine($"Monitoring and backing up {TargetFolder}");
        }

        private void Backup(object sender, FileSystemEventArgs e)
        {
            //TODO: Duplicate file to backup folder
            Console.WriteLine(e.FullPath);
        }
    }
}
