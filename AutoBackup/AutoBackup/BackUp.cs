using System;
using System.IO;

namespace AutoBackup
{
    internal sealed class BackUp
    {
        private string _targetFolder;
        private string _backupDirectory;
        private FileSystemWatcher _watcher;
        
        internal BackUp(IFileWatcher fileWatcher, IAppSettingsAccess appSettingsAccess)
        {
            _targetFolder = appSettingsAccess.AppSetting("FolderToBackup");
            _backupDirectory = appSettingsAccess.AppSetting("FolderToBackupTo");

            if (_targetFolder == "NoneSpecified")
                throw new TargetFolderNotSpecifiedException();

            this._watcher = fileWatcher.GetWatcher;
        }
        
        internal void Configure()
        {
            _watcher.Path = _targetFolder;
            _watcher.Changed += CopyFile;
            _watcher.Created += CopyFile;
            _watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
        }
        
        internal void Begin()
        {
            _watcher.EnableRaisingEvents = true;
            Console.WriteLine($"Monitoring and backing up {_targetFolder}\n");
        }

        private void CopyFile(object sender, FileSystemEventArgs e)
        {            
            string fileName = e.FullPath.Substring(_targetFolder.Length + 1);
            if (fileName.EndsWith(".tmp") || fileName.Contains("~"))
                return;

            try
            {
                File.Copy(e.FullPath, Path.Combine(_backupDirectory, fileName), true);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Unable to copy {fileName} as it no longer exists.");
            }
        }
    }
}
