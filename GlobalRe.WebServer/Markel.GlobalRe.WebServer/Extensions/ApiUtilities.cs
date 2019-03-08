using System;
using System.Configuration;
using System.Reflection;

namespace Markel.GlobalRe.WebServer.Extensions
{
    public static class ApiUtilities
    {
        public static string GetConfigSetting(string name, bool raiseErrors = true)
        {
            string value = ConfigurationManager.AppSettings.Get(name);
            if (raiseErrors && string.IsNullOrEmpty(value))
                throw new Exception($"{name} Key is not set in web.config!");

            return value;
        }

        public static string GetUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return userName;
            string[] parts = userName.Split('\\');
            return parts[parts.Length - 1];
        }

        public static string GetVersion()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;

            if (version.ToString().Equals("1.0.0.0"))
            {
                return "[DEVELOPMENT]";
            }
            else if (version.Major == 0 && version.Minor == 0)
            {
                return $"[DEVELOPMENT].{version.Build}.{version.Revision}";
            }
            else
            {
                return version.ToString();
            }
        }
    }
}