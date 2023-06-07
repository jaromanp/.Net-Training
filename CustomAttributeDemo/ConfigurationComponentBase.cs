using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttributeDemo
{
    public abstract class ConfigurationComponentBase
    {
        public void SaveSettings()
        {
            // Get all properties with the ConfigurationItem attribute
            var properties = GetType().GetProperties()
                .Where(p => p.GetCustomAttribute<ConfigurationItemAttribute>() != null);

            foreach (var property in properties)
            {
                // Get the attribute and its values
                var attribute = property.GetCustomAttribute<ConfigurationItemAttribute>();
                var settingName = attribute.SettingName;
                var providerType = attribute.ProviderType;

                // Get the value of the property
                var value = property.GetValue(this);

                // Save the value using the specified configuration provider
                if (providerType == "File")
                {
                    FileConfigurationProvider.SaveSetting(settingName, value);
                }
                else if (providerType == "ConfigurationManager")
                {
                    ConfigurationManagerConfigurationProvider.SaveSetting(settingName, value);
                }
            }
        }

        public void LoadSettings()
        {
            // Get all properties with the ConfigurationItem attribute
            var properties = GetType().GetProperties()
                .Where(p => p.GetCustomAttribute<ConfigurationItemAttribute>() != null);

            foreach (var property in properties)
            {
                // Get the attribute and its values
                var attribute = property.GetCustomAttribute<ConfigurationItemAttribute>();
                var settingName = attribute.SettingName;
                var providerType = attribute.ProviderType;

                // Load the value using the specified configuration provider
                object value = null;
                if (providerType == "File")
                {
                    value = typeof(FileConfigurationProvider)
                        .GetMethod("LoadSetting")
                        .MakeGenericMethod(property.PropertyType)
                        .Invoke(null, new object[] { settingName });
                }
                else if (providerType == "ConfigurationManager")
                {
                    value = typeof(ConfigurationManagerConfigurationProvider)
                        .GetMethod("LoadSetting")
                        .MakeGenericMethod(property.PropertyType)
                        .Invoke(null, new object[] { settingName });
                }

                // Set the value of the property
                property.SetValue(this, value);
            }
        }

    }

}
