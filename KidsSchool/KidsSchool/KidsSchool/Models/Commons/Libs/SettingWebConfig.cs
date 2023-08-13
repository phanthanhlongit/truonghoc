﻿using System;
using System.Configuration;
using System.Web.Configuration;


    public static class SettingWebConfig
{
        public static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = WebConfigurationManager.OpenWebConfiguration("~");
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("AppSettings");
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
