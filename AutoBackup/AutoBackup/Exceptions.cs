using System;

namespace AutoBackup
{
    class TargetFolderNotSpecifiedException : Exception
    {
        public TargetFolderNotSpecifiedException()
            : base("No target folder has been specified in the App.config file") { }
    }
}
