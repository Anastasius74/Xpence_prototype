using System.Configuration;
using System.Reflection;
using XPence.Shared;

namespace XPence.Infrastructure.Utility
{
    public static class AppConfigSettings
    {
      
        public static void UpdateConnectionString(string section, string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection(ApplicationConstants.ConnectionStringSection);
        }

        public static void UpdateAppSetting(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration((Assembly.GetExecutingAssembly().Location));
            var settings = config.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);

        }

        public static string ReadAppSettingValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            if (config.AppSettings.Settings.Count > 0)
            {
                return config.AppSettings.Settings[key].Value;
            }
            return null;
        }
    }
}
