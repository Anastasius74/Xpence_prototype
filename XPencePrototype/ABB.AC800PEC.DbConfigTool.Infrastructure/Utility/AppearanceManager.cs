using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MahApps.Metro;

namespace ABB.AC800PEC.DbConfigTool.Infrastructure.Utility
{
    /// <summary>
    /// A static class that manages the appearance of the application.
    /// </summary>
    public class AppearanceManager
    {
        private static readonly string LightThemeText;
        private static readonly string DarkThemeText;

        #region Constructors

        /// <summary>
        /// Static constructor to initialize static variables.
        /// </summary>
        static AppearanceManager()
        {
            LightThemeText = "BaseLight";
            DarkThemeText = "BaseDark";
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets the accent names.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetAccentNames()
        {
            return ThemeManager.Accents.Select(a => a.Name).ToList();
        }

        /// <summary>
        /// Gets the theme names.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetThemeNames()
        {
            var themes = new[] { LightThemeText, DarkThemeText };
            return themes;
        }

        /// <summary>
        /// Gets the accent name that the application is displaying presently.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationAccent()
        {
            var accentName = AppConfigSettings.ReadAppSettingValue("AccentName") ?? "Blue";
            var theme = ThemeManager.Accents.First(x => x.Name == accentName);

            return theme.Name;
        }

        /// <summary>
        /// Gets the theme name that the app is displaying presently.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationTheme()
        {
            var themeName = AppConfigSettings.ReadAppSettingValue("AppThemeName") ?? "BaseLight";

            return themeName;
        }

        /// <summary>
        /// Changes the accent of the application.
        /// </summary>
        /// <param name="accentName">The name of the accent color.</param>
        public static void ChangeAccent(string accentName)
        {
            var theme = ThemeManager.GetAppTheme(ConfigurationManager.AppSettings["AppThemeName"]);
            var accent = ThemeManager.Accents.First(x => x.Name == accentName);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
        }

        /// <summary>
        /// Changes the theme of the application.
        /// </summary>
        /// <param name="themeName">The name of the theme.</param>
        public static void ChangeTheme(string themeName)
        {
            if (string.CompareOrdinal(LightThemeText, themeName) == 0)
            {
                ThemeManager.ChangeAppTheme(Application.Current, LightThemeText);
            }
            else if (string.CompareOrdinal(DarkThemeText, themeName) == 0)
            {
                ThemeManager.ChangeAppTheme(Application.Current, DarkThemeText);
            }
        }

        #endregion
    }
}
