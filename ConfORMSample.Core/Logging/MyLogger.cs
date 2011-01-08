using System;
using NLog;

namespace ConfORMSample.Core.Logging
{
    public class MyLogger : ILog
    {
        private static Logger _logger;

        public MyLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public static ILog GetInstance()
        {
            return new MyLogger();
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void ErrorException(string message, Exception ex)
        {
            _logger.ErrorException(message, ex);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }
    }
}