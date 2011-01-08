using System;
using ConfORMSample.Core.Logging;

namespace ConfORMSample.Core
{
    public abstract class RootObject : IRootObject, IDisposable
    {
        protected RootObject()
            : this(new MyLogger())
        {
        }

        protected RootObject(ILog logger)
        {
            Logger = logger;
        }

        protected ILog Logger { get; private set; }

        public virtual void Dispose()
        {
        }
    }
}