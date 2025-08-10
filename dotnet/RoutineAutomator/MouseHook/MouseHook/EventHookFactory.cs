using System;
using MouseHook.Helpers;

namespace MouseHook
{
    /// <summary>
    /// A factory class core to the management of various watchers 
    /// that all shares the same synchronization objects.
    /// Use this class to get instances of different watchers.
    /// This factory instance should be disposed only after all watchers it have been unsubscribed.
    /// </summary>
    public class EventHookFactory : IDisposable
    {
        private readonly SyncFactory _syncFactory = new SyncFactory();

        public void Dispose()
        {
            _syncFactory.Dispose();
        }

        /// <summary>
        /// Get an instance of mouse watcher.
        /// </summary>
        /// <returns></returns>
        public MouseWatcher GetMouseWatcher()
        {
            return new MouseWatcher(_syncFactory);
        }
    }
}
