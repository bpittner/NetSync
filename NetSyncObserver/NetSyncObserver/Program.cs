using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace NetSyncObserver
{
    class Program
    {
        static void Main(string[] args)
        {
            String srcDir = ConfigurationManager.AppSettings["SourceDirectory"];
            String destDir = ConfigurationManager.AppSettings["DestinationDirectory"];
            DirectoryObserver o = new DirectoryObserver(srcDir, new DirectoryMirrorActions(destDir));

            Console.ReadLine();
        }
    }
}
