using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NetSyncObserver
{
    /// <summary>
    /// After initialized with a path to a directory,
    /// DirectoryObserver contains events that fire when
    /// file modifications occur within the directory.
    /// </summary>
    class DirectoryObserver
    {
        // Directory watched by the observer
        public String Directory { get; private set; }

        // Set of actions performed
        private List<IObserverActions> observationBehaviors;

        // FileSystemWatcher object
        private FileSystemWatcher observer;

        /// <summary>
        /// Allows the constructor with one behavior set to call the one taking multiple.
        /// </summary>
        /// <param name="ioa"></param>
        /// <returns></returns>
        private static List<IObserverActions> initList(IObserverActions ioa)
        {
            List<IObserverActions> l = new List<IObserverActions>();
            l.Add(ioa);
            return l;
        }

        /// <summary>
        /// Constructs the Observer with the given directory and set of
        /// actions to perform when activity occurs in the directory.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="behavior"></param>
        public DirectoryObserver(String directoryPath, IObserverActions behavior) : this(directoryPath, initList(behavior))
        { }

        /// <summary>
        /// Constructs the Observer with the given director and multiple sets
        /// of actions to perform when activity occurs in the directory.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="behaviors"></param>
        public DirectoryObserver(String directoryPath, List<IObserverActions> behaviors)
        {
            Directory = directoryPath;
            observationBehaviors = behaviors;

            // Initialize the observer
            observer = new FileSystemWatcher(Directory);
            observer.NotifyFilter = NotifyFilters.LastAccess
                | NotifyFilters.LastWrite
                | NotifyFilters.FileName
                | NotifyFilters.DirectoryName;

            foreach (IObserverActions a in observationBehaviors)
            {
                // Set event handling logic
                observer.Created += new FileSystemEventHandler(a.OnFileCreated);
                observer.Changed += new FileSystemEventHandler(a.OnFileChanged);
                observer.Deleted += new FileSystemEventHandler(a.OnFileDeleted);
                observer.Renamed += new RenamedEventHandler(a.OnFileRenamed);
            }

            // Enable events
            observer.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Enables observer events. (Does nothing if already enabled)
        /// </summary>
        public void enable()
        {
            observer.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Disables observer events. (Does nothing if already disabled)
        /// </summary>
        public void disable()
        {
            observer.EnableRaisingEvents = false;
        }

        /// <summary>
        /// Adds a behavior set to the observer.
        /// </summary>
        /// <param name="ioa"></param>
        public void addBehavior(IObserverActions ioa)
        {
            observer.Created += ioa.OnFileCreated;
            observer.Changed += ioa.OnFileChanged;
            observer.Deleted += ioa.OnFileDeleted;
            observer.Renamed += ioa.OnFileRenamed;
        }

        /// <summary>
        /// Removes a behavior set from the observer. (NOTE: Simply throwing in a new IObserverActions() object probably won't work)
        /// </summary>
        /// <param name="ioa"></param>
        public void removeBehavior(IObserverActions ioa)
        {
            observer.Created -= ioa.OnFileCreated;
            observer.Changed -= ioa.OnFileChanged;
            observer.Deleted -= ioa.OnFileDeleted;
            observer.Renamed -= ioa.OnFileRenamed;
        }
    }
}
