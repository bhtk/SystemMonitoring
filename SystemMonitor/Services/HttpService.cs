using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SystemMonitor.Models.UtilityModels;

namespace SystemMonitor.Services
{
    public class HttpService
    {
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
