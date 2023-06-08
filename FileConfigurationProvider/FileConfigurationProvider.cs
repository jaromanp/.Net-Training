using ConfigurationProvider.Abstractions;
using Newtonsoft.Json;

namespace FileConfigurationProvider
{
    public class FileConfigurationProvider : IConfigurationProvider
    {
        private const string SettingsFilePath = "settings.json";

        public FileConfigurationProvider() { }
        public void SaveSetting(string settingName, object value)
        {
            // Load existing settings
            var settings = LoadSettings();

            // Update setting value
            settings[settingName] = value;

            // Save settings to file
            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(SettingsFilePath, json);
        }

        public T LoadSetting<T>(string settingName)
        {
            // Load existing settings
            var settings = LoadSettings();

            // Get setting value
            if (settings.TryGetValue(settingName, out var value))
            {
                return (T)value;
            }

            return default(T);
        }

        private static Dictionary<string, object> LoadSettings()
        {
            if (File.Exists(SettingsFilePath))
            {
                var json = File.ReadAllText(SettingsFilePath);
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }

            return new Dictionary<string, object>();
        }
    }
}
