using ConfigurationProvider.Abstractions;
using System.Configuration;

namespace ConfigurationManagerConfigurationProvider
{
    public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationManagerConfigurationProvider() { }
        public void SaveSetting(string settingName, object value)
        {
            // Open the configuration file
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Update setting value
            config.AppSettings.Settings[settingName].Value = value.ToString();

            // Save changes to the configuration file
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public T LoadSetting<T>(string settingName)
        {
            try
            {
                // Get setting value
                var value = ConfigurationManager.AppSettings[settingName];

                // Handle TimeSpan values
                if (typeof(T) == typeof(TimeSpan))
                {
                    return (T)(object)TimeSpan.Parse(value);
                }

                // Convert value to the specified type
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (InvalidCastException ex)
            {
                // Handle conversion errors
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}