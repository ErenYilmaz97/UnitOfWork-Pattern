using System;
using BackgroundJob.Managers.RecurringJobs;
using Hangfire;

namespace BackgroundJob.Schedules
{
    public static class RecurringJobs
    {
        

        
        public static void DatabaseBackupOperation()
        {
            //SAAT HER 00.00 OLDUĞUNDA TETİKLEN
            //RecurringJob.RemoveIfExists(nameof(DatabaseBackupJobManager));
            RecurringJob.AddOrUpdate<DatabaseBackupJobManager>(nameof(DatabaseBackupJobManager),
                job => job.Process(), "31 14 * * *", TimeZoneInfo.Local);
        }
    }
}