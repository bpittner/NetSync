using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NetSyncObserver
{
    class MirrorActions : IObserverActions
    {
        public DirectoryInfo MirrorDirectory { get; private set; }

        public MirrorActions(String directory)
        {
            MirrorDirectory = new DirectoryInfo(directory);
            if (!MirrorDirectory.Exists)
            {
                throw new DirectoryNotFoundException(string.Format("Unable to locate directory: {0}", directory));
            }
        }

        public void OnFileCreated(object source, FileSystemEventArgs e)
        {
            // WORK ON CREATING FILES IN SUBDIRECTORIES
            FileInfo fi;
            DirectoryInfo di;

            // Handle file
            fi = FileFactory.getFileInfo(e.FullPath);
            if(fi != null) 
            {
                Console.WriteLine("File Created! Mirroring {0} to {1}", fi.FullName, MirrorDirectory.FullName + fi.Name);
                fi.CopyTo(MirrorDirectory + fi.Name);
            }
            // Handle directory
            else
            {
                di = FileFactory.getDirectoryInfo(e.FullPath);
                Console.WriteLine("Directory Created! Mirroring {0} to {1}", di.FullName, MirrorDirectory.FullName + di.Name);

                // Create the directory
                DirectoryInfo diCpy = Directory.CreateDirectory(MirrorDirectory.FullName + di.Name);
                // Recursively copy the contents of di to to diCpy
                CopyDirectory(di, diCpy);
            }
        }

        public void OnFileChanged(object source, FileSystemEventArgs e)
        {
            FileInfo fi;
            DirectoryInfo di;

            // Handle file
            fi = FileFactory.getFileInfo(e.FullPath);
            if (fi != null)
            {
                Console.WriteLine("File Created! Mirroring {0} to {1}", fi.FullName, MirrorDirectory.FullName + fi.Name);
                fi.CopyTo(MirrorDirectory + fi.Name);
            }
        }

        public void OnFileDeleted(object source, FileSystemEventArgs e)
        {
        }

        public void OnFileRenamed(object source, RenamedEventArgs e)
        {
        }


        /// <summary>
        /// Recursively copies all contents from one directory to another.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        private void CopyDirectory(DirectoryInfo src, DirectoryInfo dest)
        {
            // Copy all files
            foreach (FileInfo file in src.GetFiles())
        	{
                file.CopyTo(dest.FullName + "\\" + file.Name);
	        }

            // Create each directory and do a recursive copy
            foreach (DirectoryInfo directory in src.GetDirectories())
            {
                CopyDirectory(directory, Directory.CreateDirectory(dest.FullName + "\\" + directory.Name));
            }
        }
    }
}
