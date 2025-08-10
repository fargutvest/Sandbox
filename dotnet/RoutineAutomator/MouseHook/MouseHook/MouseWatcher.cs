using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MouseHook.Helpers;
using MouseHook.Hooks;

namespace MouseHook
{
    /// <summary>
    /// Wraps low level mouse hook.
    /// Uses a producer-consumer pattern to improve performance and to avoid operating system forcing unhook on delayed
    /// user callbacks.
    /// </summary>
    public class MouseWatcher
    {
        private readonly object _syncRoot = new object();
        private readonly SyncFactory _factory;
        private Hooks.MouseHook _mouseHook;
        private ConcurrentQueue<object> _mouseQueue;
        private CancellationTokenSource _cts;
        private bool _isRunning;

        internal MouseWatcher(SyncFactory factory)
        {
            this._factory = factory;
        }

        public event EventHandler<MouseEventArgs> OnMouseInput;

        /// <summary>
        /// Start watching mouse events
        /// </summary>
        public void Start(params MouseMessages[] mouseMessagesToSuppress)
        {
            lock (_syncRoot)
            {
                if (!_isRunning)
                {
                    _cts = new CancellationTokenSource();
                    _mouseQueue = new ConcurrentQueue<object>();
                    //This needs to run on UI thread context
                    //So use task factory with the shared UI message pump thread
                    Task.Factory.StartNew(() =>
                        {
                            _mouseHook = new Hooks.MouseHook();
                            _mouseHook.MouseAction += MListener;
                            _mouseHook.Start(mouseMessagesToSuppress);
                        },
                        CancellationToken.None,
                        TaskCreationOptions.None,
                        _factory.GetTaskScheduler()).Wait();

                    Task.Factory.StartNew(ConsumeMouseEvents);

                    _isRunning = true;
                }
            }
        }

        /// <summary>
        /// Stop watching mouse events
        /// </summary>
        public void Stop()
        {
            lock (_syncRoot)
            {
                if (_isRunning)
                {
                    if (_mouseHook != null)
                    {
                        //This needs to run on UI thread context
                        //So use task factory with the shared UI message pump thread
                        Task.Factory.StartNew(() =>
                            {
                                _mouseHook.MouseAction -= MListener;
                                _mouseHook.Stop();
                                _mouseHook = null;
                            },
                            CancellationToken.None,
                            TaskCreationOptions.None,
                            _factory.GetTaskScheduler());
                    }

                    _mouseQueue.Enqueue(false);
                    _isRunning = false;
                    _cts.Cancel();
                }
            }
        }

        /// <summary>
        /// Add mouse event to our producer queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MListener(object sender, RawMouseEventArgs e)
        {
            _mouseQueue.Enqueue(e);
        }

        /// <summary>
        /// Consume mouse events in our producer queue asynchronously
        /// </summary>
        private void ConsumeMouseEvents()
        {
            while (_isRunning)
            {
                //blocking here until a key is added to the queue
                _mouseQueue.TryDequeue(out object item);

                if (item is null)
                {
                    continue;
                }

                if (item is bool)
                {
                    break;
                }

                if (item is RawMouseEventArgs kd)
                {
                    OnMouseInput?.Invoke(null, new MouseEventArgs
                    {
                        Message = kd.Message, Point = kd.Point, MouseData = kd.MouseData
                    });
                }
            }
        }
    }
}
