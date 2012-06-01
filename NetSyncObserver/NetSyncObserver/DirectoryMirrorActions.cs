using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NetSyncObserver
{
    class DirectoryMirrorActions : IObserverActions
    {
        public String MirrorDirectory { get; private set; }

        public DirectoryMirrorActions(String directory)
        {
            MirrorDirectory = directory;
        }

        public void OnFileCreated(object source, FileSystemEventArgs e)
        {
            FileInfo fi;
            DirectoryInfo di;

            // Handle file
            fi = FileFactory.getFileInfo(e.FullPath);
            if(fi != null) 
            {
                Console.WriteLine("File Created!");    
            }
            // Handle directory
            else
            {
                di = FileFactory.getDirectoryInfo(e.FullPath);
                Console.WriteLine("Directory Created!");

            }
        }

        public void OnFileChanged(object source, FileSystemEventArgs e)
        {
        }

        public void OnFileDeleted(object source, FileSystemEventArgs e)
        {
        }

        public void OnFileRenamed(object source, RenamedEventArgs e)
        {
        }
    }
}
