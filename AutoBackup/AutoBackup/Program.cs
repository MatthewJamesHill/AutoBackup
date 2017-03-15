using System;

namespace AutoBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Auto Backup *****\n");

            BackUp Watcher = new BackUp(new FileWatcher(), new AppSettingsAccess());
            try
            {
                Watcher.Configure();
            }
            catch (Exception ex) when (ex is ArgumentException ||
                                       ex is ArgumentNullException ||
                                       ex is TargetFolderNotSpecifiedException)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            Watcher.Begin();

            Console.WriteLine("Press any key to exit...\n\n");
            Console.ReadKey();
        }
    }
}
