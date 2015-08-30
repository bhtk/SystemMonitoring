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
            var cpuCounter = new PerformanceCounter()
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };
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
}
