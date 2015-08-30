using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitor.Models.UtilityModels;

namespace SystemMonitor.Services
{
    public class DiskService
    {
        /// <summary>
        /// Calculates Total Size of Disk - Free Space of Disk
        /// </summary>
        /// <param name="jobId">Id of executing job</param>
        /// <param name="clientId">Id of executor client</param>
        /// <returns>ServiceOutput object</returns>
        public static ServiceOutput GetOccupiedDiskSpace(long jobId, long clientId)
        {
            var drives = DriveInfo.GetDrives();
            long totalSpace = 0, freeSpace = 0;
            foreach (var drive in drives)
            {
                freeSpace += drive.AvailableFreeSpace;
                totalSpace += drive.TotalSize;
            }
            return new ServiceOutput()
            {
                JobId = jobId,
                ClientId = clientId,
                Result = totalSpace - freeSpace,
                Duration = -1,
                Date = DateTime.Now
            };
        }

        /// <summary>
        /// Calculates Total Size of Disk - Free Space of Disk for specific Drive
        /// </summary>
        /// <param name="jobId">Id of executing job</param>
        /// <param name="clientId">Id of executor client</param>
        /// <param name="driveName">drive name</param>
        /// <returns>ServiceOutput object</returns>
        public static ServiceOutput GetOccupiedDriveSpace(long jobId, long clientId, string driveName)
        {
            var drive = DriveInfo.GetDrives().FirstOrDefault(d => d.Name == driveName);
            if (drive == null)
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
            return new ServiceOutput()
            {
                JobId = jobId,
                ClientId = clientId,
                Result = drive.TotalSize - drive.AvailableFreeSpace,
                Duration = -1,
                Date = DateTime.Now
            };
        }
    }
}
