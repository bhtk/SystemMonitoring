using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SystemMonitor.Models.UtilityModels;

namespace SystemMonitor.Services
{
    public class CpuService
    {
        /// <summary>
        /// Get Total usage of cpu
        /// </summary>
        /// <param name="jobId">Id of executing job</param>
        /// <param name="clientId">Id of executor client</param>
        /// <returns>ServiceOutput object</returns>
        public static ServiceOutput GetTotalCpuUsage(long jobId, long clientId)
        {
            using (var cpuCounter = new PerformanceCounter())
            {
                cpuCounter.CategoryName = "Processor";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "_Total";
                cpuCounter.NextValue();
                Thread.Sleep(1000);
                return new ServiceOutput()
                {
                    JobId = jobId,
                    ClientId = clientId,
                    Result = (long)cpuCounter.NextValue(),
                    Duration = -1,
                    Date = DateTime.Now
                };
            }
        }
        
        /// <summary>
        /// Returnes cpu usage of a specific process
        /// </summary>
        /// <param name="jobId">Id of executing job</param>
        /// <param name="clientId">Id of executor client</param>
        /// <param name="processName">name of the process</param>
        /// <returns>ServiceOutput object</returns>
        public static ServiceOutput GetCpuUsageOfSpecificProcess(long jobId, long clientId, string processName)
        {
            PerformanceCounter cpuCounter;
            var sum = (float)0;
            try
            {
                var processes = Process.GetProcessesByName(processName);
                foreach (var process in processes)
                {
                    cpuCounter = new PerformanceCounter("Processor", "% Processor Time", process.ProcessName, true);
                    cpuCounter.NextValue();
                    Thread.Sleep(250);
                    sum += cpuCounter.NextValue();
                    cpuCounter.Dispose();
                }
                return new ServiceOutput()
                {
                    JobId = jobId,
                    ClientId = clientId,
                    Result = (long) sum,
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
