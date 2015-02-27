using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MahApps.Metro;

namespace XPence.Infrastructure.Utility
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
            LightThemeText = "Light";
            DarkThemeText = "Dark";
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets the accent names.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetAccentNames()
        {
            return ThemeManager.DefaultAccents.Select(a => a.Name).ToList();
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
            var theme = ThemeManager.DetectTheme(Application.Current);
            return theme.Item2.Name;
        }

        /// <summary>
        /// Gets the theme name that the app is displaying presently.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationTheme()
        {
            var theme = ThemeManager.DetectTheme(Application.Current);
            if (theme.Item1 == Theme.Dark)
                return DarkThemeText;
            if (theme.Item1 == Theme.Light)
                return LightThemeText;
            throw new Exception("Undetected theme.");
        }

        /// <summary>
        /// Changes the accent of the application.
        /// </summary>
        /// <param name="accentName">The name of the accent color.</param>
        public static void ChangeAccent(string accentName)
        {
            var theme = ThemeManager.DetectTheme(Application.Current);
            var accent = ThemeManager.DefaultAccents.First(x => x.Name == accentName);
            ThemeManager.ChangeTheme(Application.Current, accent, theme.Item1);
        }

        /// <summary>
        /// Changes the theme of the application.
        /// </summary>
        /// <param name="themeName">The name of the theme.</param>
        public static void ChangeTheme(string themeName)
        {
            if (string.CompareOrdinal(LightThemeText, themeName) == 0)
            {
                var theme = ThemeManager.DetectTheme(Application.Current);
                ThemeManager.ChangeTheme(Application.Current, theme.Item2, Theme.Light);
            }
            else if (string.CompareOrdinal(DarkThemeText, themeName) == 0)
            {
                var theme = ThemeManager.DetectTheme(Application.Current);
                ThemeManager.ChangeTheme(Application.Current, theme.Item2, Theme.Dark);
            }
            else
            {
                throw new ValueUnavailableException("Theme name not known.");
            }
        }
        

        #endregion
    }
}
