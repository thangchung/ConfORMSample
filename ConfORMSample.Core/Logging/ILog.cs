using System;

namespace ConfORMSample.Core.Logging
{
    public interface ILog
    {
        // TODO: Should wrap all when have free-time

        void Debug(string message);

        void Error(string message);

        void ErrorException(string message, Exception ex);

        void Fatal(string message);

        void Info(string message);

        void Trace(string message);

        void Warn(string message);
    }
}