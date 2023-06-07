using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttributeDemo
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItemAttribute : Attribute
    {
        public string SettingName { get; set; }
        public string ProviderType { get; set; }

        public ConfigurationItemAttribute(string settingName, string providerType)
        {
            SettingName = settingName;
            ProviderType = providerType;
        }
    }
}
