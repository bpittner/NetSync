using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NetSyncObserver
{
    class ConsoleActions : IObserverActions
    {
        public void OnFileCreated(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File Created");
            Console.WriteLine("=>Path: {0}\n", e.FullPath);
        }

        public void OnFileChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File Changed");
            Console.WriteLine("=>Path: {0}\n", e.FullPath);
        }

        public void OnFileDeleted(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File Deleted");
            Console.WriteLine("=>Path: {0}\n", e.FullPath);
        }

        public void OnFileRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine("File Renamed");
            Console.WriteLine("=>OldPath: {0}\n=>NewPath: {1}\n", e.OldFullPath, e.FullPath);
        }
    }
}
