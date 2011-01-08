using System.Configuration;
using System.Diagnostics.Contracts;
using ConfORMSample.Core.Helpers;

namespace ConfORMSample.Core.Configuration
{
    public class Configurator : RootObject, IConfigurator
    {
        public string GetAppSettingString(string key)
        {
            Contract.Requires(!string.IsNullOrEmpty(key), string.Format("Key with named {0} is null or empty", key));

            return CatchExceptionHelper.TryCatchFunction(
                () => ConfigurationManager.AppSettings[key],
                Logger
            );
        }

        public string GetConnectionString(string key)
        {
            Contract.Requires(!string.IsNullOrEmpty(key), string.Format("Key with named {0} is null or empty", key));

            return CatchExceptionHelper.TryCatchFunction(
                () => ConfigurationManager.ConnectionStrings[key].ConnectionString,
                Logger
            );
        }

        public T GetSection<T>(string sectionName)
        {
            Contract.Requires(!string.IsNullOrEmpty(sectionName), "SectionName [" + sectionName + "] is null or empty");

            return CatchExceptionHelper.TryCatchFunction(
                () => (T)ConfigurationManager.GetSection(sectionName),
                Logger
            );
        }
    }
}