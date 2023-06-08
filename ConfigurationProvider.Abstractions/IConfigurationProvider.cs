namespace ConfigurationProvider.Abstractions
{
    public interface IConfigurationProvider
    {
        void SaveSetting(string settingName, object value);
        T LoadSetting<T>(string settingName);
    }
}