using ConfigurationProvider.Abstractions;
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
        private const string PluginsFolder = "../../../Plugins";

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
                var provider = GetConfigurationProvider(providerType);
                provider.SaveSetting(settingName, value);
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
                var provider = GetConfigurationProvider(providerType);
                var value = provider.GetType()
                    .GetMethod("LoadSetting")
                    .MakeGenericMethod(property.PropertyType)
                    .Invoke(provider, new object[] { settingName });

                // Set the value of the property
                property.SetValue(this, value);
            }
        }

        private IConfigurationProvider GetConfigurationProvider(string providerTypeName)
        {
            // Load the configuration provider assembly
            var assembly = LoadConfigurationProviderAssembly(providerTypeName);
            if (assembly == null)
            {
                throw new Exception($"Could not load configuration provider assembly for '{providerTypeName}'.");
            }

            // Find the configuration provider type
            var providerType = FindConfigurationProviderType(assembly);
            if (providerType == null)
            {
                throw new Exception($"Could not find configuration provider type for '{providerType}'.");
            }

            // Create an instance of the configuration provider
            var provider = (IConfigurationProvider)Activator.CreateInstance(providerType);

            return provider;
        }


        private Assembly LoadConfigurationProviderAssembly(string providerType)
        {
            var assemblyPath = Path.Combine(PluginsFolder, providerType + "ConfigurationProvider.dll");
            return Assembly.LoadFrom(assemblyPath);
        }

        private Type FindConfigurationProviderType(Assembly assembly)
        {
            return assembly.GetTypes()
                .FirstOrDefault(t => t.GetInterfaces().Contains(typeof(IConfigurationProvider)));
        }
    }


}
