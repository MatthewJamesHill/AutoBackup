using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackup
{
    class TargetFolderNotSpecifiedException : Exception
    {
        public TargetFolderNotSpecifiedException()
            : base("No target folder has been specified in the App.config file") { }
    }
}
