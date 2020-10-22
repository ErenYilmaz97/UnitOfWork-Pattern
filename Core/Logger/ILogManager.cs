using Microsoft.Extensions.Logging;
using Serilog.Core;
namespace Core
{
    public interface ILogManager
    {
        Logger GetLogger();
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
    }
}