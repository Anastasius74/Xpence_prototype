﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using ABB.AC800PEC.DbConfigTool.Infrastructure.BaseClasses;
using ABB.AC800PEC.DbConfigTool.Infrastructure.CoreClasses;
using ABB.AC800PEC.DbConfigTool.Infrastructure.MessagingService;
using ABB.AC800PEC.DbConfigTool.Infrastructure.Navigation;
using ABB.AC800PEC.DbConfigTool.Shared;

namespace ABB.AC800PEC.DbConfigTool.ViewModels
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
        public ApplicationViewModel(IMessagingService messagingService) : base("ApplicationViewModel")
        {
            //Configure the navigator
            Navigator = NavigatorFactory.GetNavigator();
            
            var viewList = new List<WorkspaceViewModelBase>
            {
                new AllComponentViewModel(ApplicationConstants.AllComponentViewRegesteredName, messagingService),
                new AllNodeViewModel(ApplicationConstants.AllNodeViewRegesteredName, messagingService),
                new AllRoleViewModel(ApplicationConstants.AllRoleViewRegesteredName, messagingService)
            };

            viewList.ForEach(wvm => Navigator.AddView(wvm));
            Navigator.AddHomeView(new HomeViewModel(viewList, ApplicationConstants.HomeViewRegeredName, messagingService));
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
