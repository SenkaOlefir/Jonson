using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Johnson
{
    public class SomeClass
    {
        private Thread _executingThread;
        private readonly ManualResetEvent _manualResetEvent = new ManualResetEvent(true);

        public void Execute()
        {
            _manualResetEvent.Reset();
            _executingThread = Thread.CurrentThread;
            
            Thread.Sleep(10000);     //wait 10 second
            Console.WriteLine("Not aborted");
            _manualResetEvent.Set();
        }

        public void Interrupt()
        {
            if (!_manualResetEvent.WaitOne(TimeSpan.FromSeconds(3)))
            {
                Console.WriteLine("Is aborted");
                _executingThread.Abort();
            }
        }
    }
}
