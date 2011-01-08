using System;

namespace ConfORMSample.Core.Exceptions
{
    public class ConvertDataException : Exception
    {
        public ConvertDataException() : base() { }

        public ConvertDataException(string message) : base(message) { }

        public ConvertDataException(string message, Exception inner) : base(message, inner) { }
    }
}