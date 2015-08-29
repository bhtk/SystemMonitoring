using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitor.Models.UtilityModels
{
    public class ServiceOutput
    {
        public long JobId { get; set; }
        public long ClientId { get; set; }
        public long Result { get; set; }
        public DateTime Date { get; set; }
    }
}
