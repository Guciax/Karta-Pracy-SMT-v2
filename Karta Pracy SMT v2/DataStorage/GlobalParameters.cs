using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2.DataStorage
{
    public class GlobalParameters
    {
        static System.Configuration.Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        static System.Configuration.KeyValueConfigurationCollection settings = configFile.AppSettings.Settings;

        public static bool Debug = false;

        private static string _SmtLine = "";
        public static string SmtLine
        {
            get
            {
                if (_SmtLine=="")
                {
                    RefreshKeys();
                }
                return _SmtLine;
            }
            set
            {
                AddOrUpdateAppSettings("Linia", value);
                _SmtLine = value;
            }
        }

        public static string CardReaderName
        {
            get
            {
                if(_CardReaderName == "")
                {
                    RefreshKeys();
                }
                return _CardReaderName;
            }
            set
            {
                AddOrUpdateAppSettings("CzytnikKart", value);
                _CardReaderName = value;
            }
        }

        private static string _QrReaderName = "";
        private static string _CardReaderName = @"\\?\HID#VID_FFFF&PID_0035&MI_00#8&8b12fef&0&0000#{884b96c3-56ef-11d1-bc8c-00a0c91405dd}";

        public static string QrReaderName
        {
            get
            {
                if (_QrReaderName == "")
                {
                    RefreshKeys();
                }
                return _QrReaderName;
            }
            set
            {
                AddOrUpdateAppSettings("CzytnikQr", value);
                _QrReaderName = value;
            }
        }

        private static void RefreshKeys()
        {
            _QrReaderName = ReadKey("CzytnikQr");
            _CardReaderName = ReadKey("CzytnikKart");
            _SmtLine = ReadKey("Linia");
        }

        private static string ReadKey(string key)
        {
            if (settings[key] != null)
            {
                return settings[key].Value;
            }
            return "";
        }

        private static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
