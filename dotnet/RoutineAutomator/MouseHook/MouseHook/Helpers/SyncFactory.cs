using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MouseHook.Helpers
{
    /// <summary>
    /// A class to create a dummy message pump if we don't have one
    /// A message pump is required for most of our hooks to succeed
    /// </summary>
    public class SyncFactory : IDisposable
    {
        private readonly Lazy<MessageHandler> _messageHandler;
        private readonly Lazy<TaskScheduler> _scheduler;

        public SyncFactory()
        {
            _scheduler = new Lazy<TaskScheduler>(() =>
            {
                TaskScheduler current = null;

                // create a message pump 
                // http://stackoverflow.com/questions/2443867/message-pump-in-net-windows-service
                // use async for performance gain!
                new Task(() =>
                {
                    Dispatcher.CurrentDispatcher.BeginInvoke(
                        new Action(() =>
                        {
                            Volatile.Write(ref current, TaskScheduler.FromCurrentSynchronizationContext());
                        }), DispatcherPriority.Normal);
                    Dispatcher.Run();
                }).Start();

                // we called dispatcher begin invoke to get the Message Pump Sync Context
                // we check every 10ms until synchronization context is copied
                while (Volatile.Read(ref current) == null)
                {
                    Thread.Sleep(10);
                }

                return Volatile.Read(ref current);
            });

            _messageHandler = new Lazy<MessageHandler>(() =>
            {
                MessageHandler msgHandler = null;
                // get the message handler dummy window created using the UI sync context
                new Task(e => { Volatile.Write(ref msgHandler, new MessageHandler()); }, 
                    GetUITaskScheduler()).Start();

                // wait here until the window is created on UI thread
                while (Volatile.Read(ref msgHandler) == null)
                {
                    Thread.Sleep(10);
                };

                return Volatile.Read(ref msgHandler);
            });

            Initialize();
        }

        public void Dispose()
        {
            if (_messageHandler?.Value != null)
            {
                _messageHandler.Value.DestroyHandle();
            }
        }

        /// <summary>
        /// Initialize the required message pump for all the hooks
        /// </summary>
        private void Initialize()
        {
            GetUITaskScheduler();
            GetHandle();
        }

        /// <summary>
        /// Get the UI task scheduler
        /// </summary>
        /// <returns></returns>
        public TaskScheduler GetUITaskScheduler()
        {
            return _scheduler.Value;
        }

        /// <summary>
        /// Get the handle of the window we created on the UI thread
        /// </summary>
        /// <returns></returns>
        internal IntPtr GetHandle()
        {
            return _messageHandler.Value.Handle;
        }
    }
}
