using System;
using System.Diagnostics;
using System.Net;
using SystemMonitor.Models.UtilityModels;

namespace SystemMonitor.Services
{
    public class HttpService
    {
        /// <summary>
        /// Send Http Request to the specific url
        /// </summary>
        /// <param name="jobId">Id of executing job</param>
        /// <param name="clientId">Id of executor client</param>
        /// <param name="url">url which will be used for http request</param>
        /// <returns>ServiceOutput object</returns>
        public static ServiceOutput SendHttpRequest(long jobId, long clientId, string url)
        {
            var httpRequest = (HttpWebRequest) WebRequest.Create("http://" + url);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            using(var httpResponse = (HttpWebResponse) httpRequest.GetResponse()){
                stopWatch.Stop();
                return new ServiceOutput()
                {
                    JobId = jobId,
                    ClientId = clientId,
                    Result = (long)httpResponse.StatusCode,
                    Duration = (httpResponse.StatusCode == HttpStatusCode.Accepted) ? stopWatch.ElapsedMilliseconds : -1,
                    Date = DateTime.Now
                };
            }
        }
    }
}
