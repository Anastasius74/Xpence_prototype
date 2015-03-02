using System;
using System.Collections.Generic;
using System.Configuration;
using XPence.Infrastructure.BaseClasses;
using XPence.Infrastructure.MessagingService;
using XPence.Shared;
using XPence.Infrastructure.Utility;

namespace XPence.ViewModels
{
    /// <summary>
    /// This view model class caters to the settings view.
    /// </summary>
    public class SettingsViewModel : FlyoutViewModelBase
    {
        #region Public properties

        /// <summary>
        /// Gets the list of the accent color names.
        /// </summary>
        public IList<string> AccentColorlist { get; private set; }

        /// <summary>
        /// Gets the name of the base themes.
        /// </summary>
        public IList<string> ThemeColorlist { get; private set; }

        /// <summary>
        /// Gets or sets the selected accent color.
        /// </summary>
        public string SelectedAccent
        {
            get { return selectedAccent; }
            set
            {
                if (value == selectedAccent)
                    return;
                selectedAccent = value;
                OnPropertyChanged(GetPropertyName(() => SelectedAccent));
                AccentChangeRequested();
            }
        }

        /// <summary>
        /// Gets or sets the selected theme.
        /// </summary>
        public string SelectedTheme
        {
            get { return selectedTheme; }
            set
            {
                if (value == selectedTheme)
                    return;
                selectedTheme = value;
                OnPropertyChanged(GetPropertyName(() => SelectedTheme));
                ThemeChangeRequested();
            }
        }

        #endregion
       
        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="SettingsViewModel"/>.
        /// </summary>
        /// <param name="messagingService">An implementation of <see cref="IMessagingService"/>.</param>
        public SettingsViewModel(IMessagingService messagingService)
        {
            if (null == messagingService)
                throw new ArgumentNullException("messagingService");
            this.messagingService = messagingService;
            AccentColorlist = new List<string>(AppearanceManager.GetAccentNames());
            ThemeColorlist = new List<string>(AppearanceManager.GetThemeNames());
            selectedAccent = AppearanceManager.GetApplicationAccent();
            selectedTheme = AppearanceManager.GetApplicationTheme();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the default theme for this instance of <see cref="SettingsViewModel"/>
        /// </summary>
        /// <param name="themeName"></param>
        /// <param name="accentName"></param>
        public void SetDefaultSettings(string themeName, string accentName)
        {
            if (themeName == null)
                throw new ArgumentNullException("themeName");
            if (accentName == null)
                throw new ArgumentNullException("accentName");
            selectedTheme = themeName;
            OnPropertyChanged(GetPropertyName(() => SelectedTheme));
            selectedAccent = accentName;
            OnPropertyChanged(GetPropertyName(() => SelectedAccent));
        }

        #endregion


        #region Private Helpers

        /// <summary>
        /// Sets the requested theme to the application's appearance.
        /// </summary>
        private void ThemeChangeRequested()
        {
            AppearanceManager.ChangeTheme(SelectedTheme);
            PromptUserToSaveAppearance();
        }

        /// <summary>
        /// Sets the requested accent to the application's appearance.
        /// </summary>
        private void AccentChangeRequested()
        {
            AppearanceManager.ChangeAccent(SelectedAccent);
            PromptUserToSaveAppearance();
        }

        /// <summary>
        /// Prompt the user if he wishes to persist the appearnce for future too.
        /// </summary>
        private void PromptUserToSaveAppearance()
        {
            if (messagingService.ShowMessage(UIText.APPEAR_CHANGE_PROMPT, DialogType.Question) == DialogResponse.Yes)
            {
                LogUtil.LogInfo("SettingsViewModel", "PromptUserToSaveAppearance", "User changed apperance.");
                try
                {
                    AppData.ApplicationUser.SelectedAccent = SelectedAccent;
                    AppData.ApplicationUser.SelectedTheme = SelectedTheme;
                    AppConfigSettings.UpdateAppSetting("AppThemeName", SelectedTheme);
                    AppConfigSettings.UpdateAppSetting("AccentName", SelectedAccent);
                }
                catch (Exception ex)
                {
                    LogUtil.LogError("SettingsViewModel", "PromptUserToSaveAppearance",ex);
                    messagingService.ShowMessage(ErrorMessages.ERR_APP_ERROR, DialogType.Error);
                }
            }
        }

        #endregion

        #region Member Variables

        private string selectedAccent;
        private string selectedTheme;
        private readonly IMessagingService messagingService;

        #endregion
    }
}
