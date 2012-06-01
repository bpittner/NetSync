using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NetSyncObserver
{
    static class FileFactory
    {
        public static FileInfo getFileInfo(String path)
        {
            if (File.Exists(path))
            {
                return new FileInfo(path);
            }
            else
            {
                return null;
            }
        }

        public static DirectoryInfo getDirectoryInfo(String path)
        {
            if(Directory.Exists(path))
            {
                return new DirectoryInfo(path);
            }
            else
            {
                return null;
            }
        }
    }
}
