using System;
using BackgroundJob.Managers.RecurringJobs;

namespace BackgroundJob.Schedules
{
    public  static class DelayedJobs
    {
        public static void DatabaseBackupOperation()
        {
            Hangfire.BackgroundJob.Schedule<DatabaseBackupJobManager>
                (job => job.Process(), TimeSpan.FromMinutes(1));
        }
    }
}