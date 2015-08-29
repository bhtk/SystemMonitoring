using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using SystemMonitor.Models.UtilityModels;

namespace SystemMonitor.Services
{
    public class PingService
    {
        /// <summary>
        /// Send Ping to the specific url synchronously
        /// </summary>
        /// <param name="jobId">Id of executing job</param>
        /// <param name="clientId">Id of executor client</param>
        /// <param name="url">url which will be pinged</param>
        /// <returns>ServiceOutput object</returns>
        public static ServiceOutput SendPingRequest(long jobId, long clientId, string url)
        {
            using (var pingSender = new Ping())
            {
                var reply = pingSender.Send(url);
                return new ServiceOutput()
                {
                    JobId = jobId,
                    ClientId = clientId,
                    Result = (long)reply.Status,
                    Duration = (reply.Status == IPStatus.Success) ? reply.RoundtripTime : -1,
                    Date = DateTime.Now
                };
            }
        }
    }
}
