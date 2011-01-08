namespace ConfORMSample.Core.Configuration
{
    public interface IConfigurator
    {
        string GetAppSettingString(string key);

        string GetConnectionString(string key);

        T GetSection<T>(string sectionName);
    }
}