using System.Configuration;

namespace AutoBackup
{
    class AppSettingsAccess : IAppSettingsAccess
    {
        public string AppSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName].ToString();
        }
    }
}
