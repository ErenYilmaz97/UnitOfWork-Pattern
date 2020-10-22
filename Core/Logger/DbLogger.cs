using Serilog;
using Serilog.Core;

namespace Core
{
    public class DbLogger : ILogManager
    {
        public Logger GetLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.MSSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LoggerProject;Trusted_Connection=True;MultipleActiveResultSets=True", tableName: "Serilog", autoCreateSqlTable: true)
                .CreateLogger();
        }

        public void Information(string message) => GetLogger().Information(message);
        public void Warning(string message) => GetLogger().Warning(message);
        public void Debug(string message) => GetLogger().Debug(message);
        public void Error(string message) => GetLogger().Error(message);

    }
}