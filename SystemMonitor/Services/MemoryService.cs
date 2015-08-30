using System;
using System.Diagnostics;
using System.Threading;
using SystemMonitor.Models.UtilityModels;

namespace SystemMonitor.Services
{

    public class MemoryService
    {
        /// <summary>
        /// Get Total memory occupation
        /// </summary>
        /// <param name="jobId">Id of executing job</param>
        /// <param name="clientId">Id of executor client</param>
        /// <returns>ServiceOutput object</returns>
        public static ServiceOutput GetTotalMemoryOccupation(long jobId, long clientId)
        {
            using (var ramCounter = new PerformanceCounter())
            {
                ramCounter.CategoryName = "Memory";
                ramCounter.CounterName = "Available MBytes";
                ramCounter.NextValue();
                Thread.Sleep(500);
                return new ServiceOutput()
                {
                    JobId = jobId,
                    ClientId = clientId,
                    Result = (long)ramCounter.NextValue(),
                    Duration = -1,
                    Date = DateTime.Now
                };
            }
        }

        /// <summary>
        /// Returnes memory occupation of a specific process
        /// </summary>
        /// <param name="jobId">Id of executing job</param>
        /// <param name="clientId">Id of executor client</param>
        /// <param name="processName">name of the process</param>
        /// <returns>ServiceOutput object</returns>
        public static ServiceOutput GetMemoryOccupationOfSpecificProcess(long jobId, long clientId, string processName)
        {
            PerformanceCounter ramCounter;
            var sum = (float)0;
            try
            {
                var processes = Process.GetProcessesByName(processName);
                foreach (var process in processes)
                {
                    ramCounter = new PerformanceCounter("Process", "Private Bytes", process.ProcessName, true);
                    ramCounter.NextValue();
                    Thread.Sleep(250);
                    sum += ramCounter.NextValue();
                    ramCounter.Dispose();
                }
                return new ServiceOutput()
                {
                    JobId = jobId,
                    ClientId = clientId,
                    Result = (long)sum,
                    Duration = -1,
                    Date = DateTime.Now
                };
            }
            catch
            {
                return new ServiceOutput()
                {
                    JobId = jobId,
                    ClientId = clientId,
                    Result = -1,
                    Duration = -1,
                    Date = DateTime.Now
                };
            }
        }
    }
}
