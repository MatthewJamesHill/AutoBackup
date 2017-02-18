using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackup
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("***** Auto Backup *****");

            FileWatcher Watcher = new FileWatcher();

            try
            {
                Watcher.RegisterTargetFolder();
            }
            catch (Exception ex) when (ex is ArgumentException ||
                                       ex is ArgumentNullException ||
                                       ex is TargetFolderNotSpecifiedException)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            Watcher.RegisterEvents();
            Watcher.RegisterFilters();
            Watcher.BeginWatching();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
