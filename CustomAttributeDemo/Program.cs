using System;
using System.Reflection;
using System.IO;

namespace CustomAttributeDemo
{
    public class MySettings : ConfigurationComponentBase
    {
        [ConfigurationItem("MyStringSetting", "ConfigurationManager")]
        public string MyStringSetting { get; set; }

        [ConfigurationItem("MyIntSetting", "ConfigurationManager")]
        public int MyIntSetting { get; set; }

        [ConfigurationItem("MyTimeSpanSetting", "ConfigurationManager")]
        public TimeSpan MyTimeSpanSetting { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var mySettings = new MySettings();
            Console.WriteLine("Loading settings from app.config...");
            mySettings.LoadSettings();

            Console.WriteLine("The settings were loaded");
            Console.WriteLine("My string setting is: {0}", mySettings.MyStringSetting);
            Console.WriteLine("My int setting is: {0}", mySettings.MyIntSetting);
            Console.WriteLine("My TimeSpanSetting setting is: {0}", mySettings.MyTimeSpanSetting);

            //// Modify settings
            Console.WriteLine("Now lets modify the settings and save it to the app.config");
            mySettings.MyStringSetting = "New value";
            mySettings.MyIntSetting = 123;
            mySettings.MyTimeSpanSetting = TimeSpan.FromMinutes(30);

            mySettings.SaveSettings();

            string appConfigFilePath = "../../../app.config";
            string appConfigContent = File.ReadAllText(appConfigFilePath);

            Console.WriteLine("New app.config content:");
            Console.WriteLine(appConfigContent);
        }
    }

}

