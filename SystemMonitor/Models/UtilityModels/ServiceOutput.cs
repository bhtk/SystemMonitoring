using System;

namespace SystemMonitor.Models.UtilityModels
{
    public class ServiceOutput
    {
        public long JobId { get; set; }
        public long ClientId { get; set; }
        public long Result { get; set; }
        public long Duration { get; set; }
        public DateTime Date { get; set; }
    }
}
