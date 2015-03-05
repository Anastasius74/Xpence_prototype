using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Security.AccessControl;
using System.Windows.Input;
using ABB.AC800PEC.DbConfigTool.Shared;
using ABB.AC800PEC.DbConfigTool.Infrastructure.BaseClasses;
using ABB.AC800PEC.DbConfigTool.Infrastructure.CoreClasses;
using ABB.AC800PEC.DbConfigTool.Infrastructure.MessagingService;
using ABB.AC800PEC.DbConfigTool.Infrastructure.Utility;
using ABB.AC800PEC.DbConfigTool.Logging;
using ABB.AC800PEC.DbConfigTool.Models;

namespace ABB.AC800PEC.DbConfigTool.ViewModels
{
    /// <summary>
    /// The ViewModel class that caters to the main window of the application.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase, IFlyoutContainer
    {
        #region Public Members

        /// <summary>
        /// Gets or sets an instance of <see cref="WorkspaceViewModelBase"/> That is shown in the MainWindow.
        /// </summary>
        public WorkspaceViewModelBase SelectedView
        {
            get { return selectedView; }
            set
            {
                if (selectedView == value)
                    return;
                selectedView = value;
                OnPropertyChanged(GetPropertyName(() => SelectedView));
            }
        }

        /// <summary>
        /// Gets an instance of <see cref="SettingsViewModel"/> that caters to the SettingsView.
        /// </summary>
        public SettingsViewModel SettingsView { get; private set; }

        /// <summary>
        /// Gets or sets the list of <see cref="FlyoutViewModelBase"/>
        /// </summary>
        public ObservableCollection<FlyoutViewModelBase> Flyouts
        {
            get { return flyouts; }
            set
            {
                if (value == flyouts)
                    return;
                flyouts = value;
                OnPropertyChanged(GetPropertyName(() => Flyouts));
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the command to toggle the visibility of the settings view.
        /// </summary>
        public ICommand ToggleSettingsVisibilityCommand { get; private set; }

        /// <summary>
        /// Gets the command to show the help.
        /// </summary>
        public ICommand ShowHelpCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="MainWindowViewModel"/>.
        /// </summary>
        /// <param name="messagingService"></param>
        public MainWindowViewModel(IMessagingService messagingService)
        {
            if (null == messagingService)
                throw new ArgumentNullException("messagingService");
            this.messagingService = messagingService;
            applicationView = new ApplicationViewModel(messagingService);
            SelectedView = applicationView;
            SettingsView = new SettingsViewModel(this.messagingService)
            {
                Position = VisibilityPosition.Right,
                Theme = FlyoutTheme.AccentedTheme,
                Header = UIText.SETTINGS_VIEW_HEADER
            };

            //Initialize commands
            ToggleSettingsVisibilityCommand = new RelayCommand(() => SettingsView.IsOpen = !SettingsView.IsOpen, () => true);
            ShowHelpCommand = new RelayCommand(ShowHelp);
        }

        #endregion

        #region Overriden methods

        /// <summary>
        /// A place holder for self initialization.
        /// </summary>
        protected override void OnInitialize()
        {
            Flyouts = new ObservableCollection<FlyoutViewModelBase>();
            SetUserPreference();
            SelectedView.Initialize();
            messagingService.RegisterFlyout(SettingsView);
        }

        /// <summary>
        /// Clean-up!
        /// </summary>
        protected override void OnDispose()
        {
            //Clear the flyouts
            if (Flyouts != null)
            {
                Flyouts.Clear();
            }
            // Dispose the application view
            if (null != applicationView)
            {
                applicationView.Dispose();
            }
            //Dispose the application view.
            if (null != SelectedView)
            {
                SelectedView.Dispose();
            }
        }

        #endregion
       
        #region Private Methods

        /// <summary>
        /// Sets the appearance of the application as per user prefernce.
        /// </summary>
        private void SetUserPreference()
        {
            
            string accent = AppConfigSettings.ReadAppSettingValue("AccentName") ?? AppearanceManager.GetApplicationAccent();
            string theme = AppConfigSettings.ReadAppSettingValue("AppThemeName") ?? AppearanceManager.GetApplicationTheme();
            AppearanceManager.ChangeAccent(accent);
            AppearanceManager.ChangeTheme(theme);
            //Set the defaults in settings view too!
            SettingsView.SetDefaultSettings(AppearanceManager.GetApplicationTheme(),AppearanceManager.GetApplicationAccent());
        }

        private void ShowHelp()
        {
            try
            {
                messagingService.ShowCustomMessageDialog(ApplicationConstants.HelpView, new HelpViewModel { TitleText = UIText.ABOUT_WINDOW_HEADER });
            }
            catch (Exception ex)
            {
                LogUtil.LogError("LoginViewModel", "TryLoginIn", ex);
                messagingService.ShowMessage(ErrorMessages.ERR_APP_ERROR, DialogType.Error);
            }
        }

        #endregion

        #region Members variables
        
        private readonly ApplicationViewModel applicationView;
        private WorkspaceViewModelBase selectedView;
        private readonly IMessagingService messagingService;
        private ObservableCollection<FlyoutViewModelBase> flyouts;

        #endregion
    }
}
