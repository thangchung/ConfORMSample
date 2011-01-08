using System;
using ConfORMSample.Core.Exceptions;

namespace ConfORMSample.Core.Extensions
{
    public static class ObjectExtension
    {
        public static int ConvertToInt(this object obj)
        {
            var output = 0;

            TryCatchNoLogger(() => int.TryParse(obj.ToString(), out output));

            return output;
        }

        public static string ConvertToString(this object obj)
        {
            var output = string.Empty;

            TryCatchNoLogger(() => output = obj.ToString());

            return output;
        }

        public static DateTime ConvertToDateTime(this object obj)
        {
            var output = DateTime.MinValue;

            TryCatchNoLogger(() => DateTime.TryParse(obj.ToString(), out output));

            return output;
        }

        public static bool ConvertToBoolean(this object obj)
        {
            var output = false;

            TryCatchNoLogger(() => Boolean.TryParse(obj.ToString(), out output));

            return output;
        }

        private static void TryCatchNoLogger(Action action)
        {
            try
            {
                action();
            }
            catch (ConvertDataException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}