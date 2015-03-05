
using System;

namespace ABB.AC800PEC.DbConfigTool.Infrastructure.BaseClasses
{
    /// <summary>
    /// A base class for all viewmodels that cater to workspace views.
    /// </summary>
    public abstract class WorkspaceViewModelBase : ViewModelBase
    {
        #region Public properties

        /// <summary>
        /// Gets or sets if the viewmodel instance permits navigating back.
        /// </summary>
        public bool CanGoBack
        {
            get { return canGoBack; }
            set
            {
                if (canGoBack == value) 
                    return;
                canGoBack = value;
                OnPropertyChanged(GetPropertyName(() => CanGoBack));
            }
        }

        /// <summary>
        /// Gets or sets if only admin can navigate.
        /// Can be overriden in a child class.
        /// </summary>
        public bool CanUserNavigate { get; protected set; }

        /// <summary>
        /// Gets the registered name of the view.
        /// </summary>
        public string RegisteredName { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes an instance of WorkspaceViewModelBase.
        /// </summary>
        /// <param name="registeredName"></param>
        /// <param name="canUserNavigate">Optional parameter if the present logged in user can navigate to this instance of <see cref="WorkspaceViewModelBase"/> </param>
        protected WorkspaceViewModelBase(string registeredName, bool canUserNavigate = true)
        {
            if (string.IsNullOrEmpty(registeredName))
                throw new ArgumentNullException("registeredName");
            CanUserNavigate = canUserNavigate;
            RegisteredName = registeredName;
        }


        #endregion

        #region Private Variables

        private bool canGoBack;

        #endregion
    }
}
