using System;
using System.IO;
using System.Threading.Tasks;
using Core.Results;
using Microsoft.SqlServer.Management.Smo;

namespace Core
{
    public class DatabaseOperations :IDatabaseManager
    {
        public void DatabaseBackupOperation()
        {

            Backup databaseBackup = new Backup();

            databaseBackup.Action = BackupActionType.Database;

            databaseBackup.Database = "LoggerProject";

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LoggerProject.bak");
            

            databaseBackup.Devices.AddDevice(path, DeviceType.File);
            databaseBackup.BackupSetName = "LoggerProject Database Backup";
            databaseBackup.BackupSetDescription = "LoggerProject Database Description";

            databaseBackup.Initialize = false;

            Server server = new Server("(localdb)\\MSSQLLocalDB");
            databaseBackup.SqlBackup(server);
        }
    }
}