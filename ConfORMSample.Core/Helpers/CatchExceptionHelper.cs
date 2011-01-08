using System;
using ConfORMSample.Core.Logging;

namespace ConfORMSample.Core.Helpers
{
    public static class CatchExceptionHelper
    {
        public static void TryCatchAction(Action action, ILog logger)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public static T TryCatchFunction<T>(Func<T> function, ILog logger)
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                logger.ErrorException(ex.Message, ex);
                throw;
            }
        }
    }
}