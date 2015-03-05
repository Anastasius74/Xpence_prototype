using System;
using System.Linq;
using System.Windows.Input;
using ABB.AC800PEC.DbConfigTool.Infrastructure.BaseClasses;
using ABB.AC800PEC.DbConfigTool.Models;

namespace ABB.AC800PEC.DbConfigTool.ViewModels
{
    /// <summary>
    /// A UI friendly wrapper around the <see cref="User"/> odel.
    /// </summary>
    public class UserViewModel : ViewModelBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name
        {
            get { return Entity.Name; }
            set
            {
                if (value == Entity.Name)
                    return;
                Entity.Name = value;
                OnPropertyChanged(GetPropertyName(() => Name));
            }
        }

        /// <summary>
        /// Gets or sets the theme selcted by the user.
        /// </summary>
        public string SelectedTheme
        {
            get { return Entity.SelectedTheme; }
            set
            {
                if (value == Entity.SelectedTheme)
                    return;
                Entity.SelectedTheme = value;
                OnPropertyChanged(GetPropertyName(() => SelectedTheme));
            }
        }

        /// <summary>
        /// Gets or sets the accent selected by the user.
        /// </summary>
        public string SelectedAccent
        {
            get { return Entity.SelectedAccent; }
            set
            {
                if (value == Entity.SelectedAccent)
                    return;
                Entity.SelectedAccent = value;
                OnPropertyChanged(GetPropertyName(() => SelectedTheme));
            }
        }

        /// <summary>
        /// Gets the wrapped up model instance.
        /// </summary>
        public User Entity { get; private set; }
        
        public bool IsValid
        {
            get { return PropertyNames.All(p => GetErrorForProperty(p) == null); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Intializes an instance of <see cref="UserViewModel"/>.
        /// </summary>
        /// <param name="user"></param>
        public UserViewModel(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            Entity = user;
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Gets the error string for a property value against a property's name.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override string GetErrorForProperty(string propertyName)
        {
            CommandManager.InvalidateRequerySuggested();
            return Entity[propertyName];
        }

        #endregion

        #region Member Variables

        private static readonly string[] PropertyNames = { "Name"};
        #endregion
    }
}
