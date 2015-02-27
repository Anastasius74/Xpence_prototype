using System.ComponentModel;
using System.Windows.Input;
using XPence.Infrastructure.BaseClasses;
using XPence.Infrastructure.CoreClasses;
using XPence.Infrastructure.MessagingService;
using XPence.Infrastructure.Navigation;
using XPence.Shared;

namespace XPence.ViewModels
{
    /// <summary>
    ///     A view model class that renders the Application after the user has logged in.
    /// </summary>
    public class ApplicationViewModel : WorkspaceViewModelBase
    {
        #region Public Properties

        /// <summary>
        ///     Gets the instance of Navigator that is responsible for all navigation purpose.
        /// </summary>
        public INavigator Navigator { get; private set; }

        /// <summary>
        ///     Gets or sets the selected workspace.
        /// </summary>
        public WorkspaceViewModelBase SelectedWorkspace
        {
            get { return selectedWorkspace; }
            set
            {
                if (selectedWorkspace == value)
                    return;
                selectedWorkspace = value;
                OnPropertyChanged(GetPropertyName(() => SelectedWorkspace));
            }
        }

        #endregion

        #region Commands

        /// <summary>
        ///     Gets the command to Navigat back.
        /// </summary>
        public ICommand NavigateBackCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes an instance of <see cref="ApplicationViewModel" />.
        /// </summary>
        /// <param name="messagingService">An implementation of <see cref="IMessagingService" /> </param>
        public ApplicationViewModel(IMessagingService messagingService)
        {
            //Configure the navigator
            Navigator = NavigatorFactory.GetNavigator();
            //var viewList = new List<WorkspaceViewModelBase>()
            //                   {
            //                       new AllTransactionViewModel(isUserAdmin,ApplicationConstants.ALLEXPENSES_VIEW_REGERED_NAME,userRepository,transactionRepository,messagingService),
            //                       new ManageViewModel(ApplicationConstants.MANAGE_VIEW_REGERED_NAME,userRepository,messagingService,isUserAdmin)
            //                   };
            //viewList.ForEach(wvm => Navigator.AddView(wvm));
            Navigator.AddHomeView(new HomeViewModel(null, messagingService));
            Navigator.PropertyChanged += NavigatorPropertyChanged;
            Navigator.NavigateToHome();
            //Initialie the commands
            NavigateBackCommand = new RelayCommand(Navigator.NavigateBack);
        }

        #endregion

        #region Private event handler

        /// <summary>
        ///     Handles the the event of the Navigator instance property changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigatorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GetPropertyName(() => Navigator.CurrentView))
            {
                SelectedWorkspace = Navigator.CurrentView;
            }
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        ///     Clean Up!
        /// </summary>
        protected override void OnDispose()
        {
            Navigator.PropertyChanged -= NavigatorPropertyChanged;
        }

        #endregion

        #region Member Variables

        private WorkspaceViewModelBase selectedWorkspace;

        #endregion
    }
}
