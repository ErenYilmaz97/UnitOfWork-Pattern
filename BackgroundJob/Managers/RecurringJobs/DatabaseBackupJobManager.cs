
using System;
using System.Threading.Tasks;
using Core;

namespace BackgroundJob.Managers.RecurringJobs
{
    public class DatabaseBackupJobManager
    {
        private readonly IDatabaseManager _databaseManager;
        private readonly ILogManager _logManager;


        //DI
        public DatabaseBackupJobManager(IDatabaseManager databaseManager, ILogManager logManager)
        {
            _databaseManager = databaseManager;
            _logManager = logManager;
        }


        public void Process()
        {

            try
                {
                    //BELİRTİLEN DOSYA YOLUNA VERİTABANI YEDEĞİ AL.
                    _databaseManager.DatabaseBackupOperation();
                    _logManager.Information("Veritabanı Yedeği Alındı.");
                }
                catch
                {
                    _logManager.Error("Veritabanı Yedeği Alınamadı.");
                }

        }
    }
}