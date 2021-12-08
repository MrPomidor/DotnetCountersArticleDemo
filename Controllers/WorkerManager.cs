using System.Diagnostics;
using TestApplication.Monitoring;

namespace TestApplication.Controllers
{
    public class WorkerManager
    {
        private static WorkerEventSource Log = WorkerEventSource.Log;

        public void SetupWork()
        {
            Task.Run(() => DoWork());
            //Task.Run(() => DoWorkAsync());
        }

        private void DoWork()
        {
            Log.WorkItemAdded();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                Thread.Sleep(10000); // sleep for 10 seconds
            }
            catch { }
            finally
            {
                stopWatch.Stop();
                Log.WorkItemCompleted(stopWatch.ElapsedMilliseconds);
            }
        }

        private async Task DoWorkAsync()
        {
            Log.WorkItemAdded();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                await Task.Delay(10000); // sleep for 10 seconds
            }
            catch { }
            finally
            {
                stopWatch.Stop();
                Log.WorkItemCompleted(stopWatch.ElapsedMilliseconds);
            }
        }
    }
}
