using System;
using System.Threading;

namespace RenderingCommon
{
    public class MetricPerSecond
    {
        private int counter;
        private Timer timer;
        private ReaderWriterLockSlim loker;
        public event EventHandler<int> UpdatedFps;
        
        public MetricPerSecond()
        {
            loker = new ReaderWriterLockSlim();
            timer = new Timer(TimerTick, null, 0, 1000);
        }

        private void TimerTick(object state)
        {
            loker.EnterWriteLock();
            var current = counter;
            counter = 0;
            loker.ExitWriteLock();
            UpdatedFps?.Invoke(this, current);
        }

        public void Tick()
        {
            loker.EnterWriteLock();
            counter++;
            loker.ExitWriteLock();
        }
    }
}
