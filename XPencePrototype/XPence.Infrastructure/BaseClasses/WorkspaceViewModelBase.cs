
namespace XPence.Infrastructure.BaseClasses
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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes an instance of WorkspaceViewModelBase.
        /// </summary>
        /// <param name="canUserNavigate">Optional parameter if the present logged in user can navigate to this instance of <see cref="WorkspaceViewModelBase"/> </param>
        protected WorkspaceViewModelBase(bool canUserNavigate=true)
        {
            CanUserNavigate = canUserNavigate;
        }

        #endregion

        #region Private Variables

        private bool canGoBack;

        #endregion
    }
}
