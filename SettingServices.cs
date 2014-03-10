using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepadder
{
    public class SettingServices
    {
        private static Settings settings = Settings.Default;

        public static bool WordWrap
        {
            get
            {
                return settings.WordWrap;
            }
            set
            {
                settings.WordWrap = value;
                settings.Save();
            }
        }

        public static System.Drawing.Font Font
        {
            get
            {
                return settings.Font;
            }
            set
            {
                settings.Font = value;
                settings.Save();
            }
        }

        public static bool ShowStatusBar
        {
            get
            {
                return settings.ShowStatusBar;
            }
            set
            {
                settings.ShowStatusBar = value;
                settings.Save();
            }
        }
    }
}
