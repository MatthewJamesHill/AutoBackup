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

            Console.WriteLine("***** Auto Backup *****\n");

            FileWatcher Watcher = new FileWatcher();

            try
            {
                Watcher.RegisterTargetFolder();
                Watcher.RegisterEvents();
                Watcher.RegisterFilters();
            }
            catch (Exception ex) when (ex is ArgumentException ||
                                       ex is ArgumentNullException ||
                                       ex is TargetFolderNotSpecifiedException)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }
                        
            Watcher.BeginWatching();

            Console.WriteLine("Press any key to exit...\n\n");
            Console.ReadKey();
        }
    }
}
