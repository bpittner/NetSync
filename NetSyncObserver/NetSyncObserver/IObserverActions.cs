using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NetSyncObserver
{
    interface IObserverActions
    {
        void OnFileCreated(object source, FileSystemEventArgs e);
        void OnFileChanged(object source, FileSystemEventArgs e);
        void OnFileDeleted(object source, FileSystemEventArgs e);

        void OnFileRenamed(object source, RenamedEventArgs e);
    }
}
