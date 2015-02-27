using System.Configuration;
using System.Text;
using XPence.Framework.DatabaseInfo;
using XPence.Shared;

namespace XPence.Framework
{

    public static class ConfigurationFileSettings
    {
        public static void UpdateConnectionString(DatabaseInfoItem databaseInfoItem)
        {
            //Constructing connection string from the DatabaseInfoItem.
            StringBuilder stringBuilder = new StringBuilder(ApplicationConstants.DataSourceString);
            stringBuilder.Append(databaseInfoItem.Location);
            stringBuilder.Append(@"\");
            stringBuilder.Append(databaseInfoItem.FileName);

            string connectionString = stringBuilder.ToString();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings[ApplicationConstants.ConnectionStringName].ConnectionString = connectionString;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection(ApplicationConstants.ConnectionStringSection);
        }

        public static string ReadAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}