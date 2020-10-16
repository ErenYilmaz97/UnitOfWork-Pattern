using System.IO;
using Serilog;
using Serilog.Core;

namespace Core.Serilog
{
    public class SerilogFileLogger : ILogManager
    {

        public Logger GetLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.File(string.Format("{0}{1}", Directory.GetCurrentDirectory() + "deneme.txt"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null,
                    fileSizeLimitBytes: 5000000,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
        }


        public void Information(string message) => GetLogger().Information(message);
        public void Warning(string message) => GetLogger().Warning(message);
        public void Debug(string message) => GetLogger().Debug(message);
        public void Error(string message) => GetLogger().Error(message);

       
    }
}