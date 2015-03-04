using System.Linq;
using XPence.Framework;
using XPence.Infrastructure.BaseClasses;
using XPence.Models;

namespace XPence.ViewModels
{
    public class RoleViewModel : ViewModelBase
    {
        private static readonly string[] PropertyNames = { "Name" };
        private bool isMarked;

        /// <summary>
        ///     Gets or sets the wrapped up Role model.
        /// </summary>
        public Role RoleEntity { get; private set; }

        /// <summary>
        ///     Intializes an instance of <see cref="RoleViewModel" />.
        /// </summary>
        /// <param name="roleEntity"></param>
        public RoleViewModel(Role roleEntity)
        {
            Guard.ArgumentNotNull("roleEntity", "roleEntity");
            RoleEntity = roleEntity;
        }

        /// <summary>
        ///     Gets the Role id.
        /// </summary>
        public long RoleId
        {
            get { return RoleEntity.Id; }
        }

        /// <summary>
        ///     Gets or sets the Role network name.
        /// </summary>
        public string Name
        {
            get { return RoleEntity.Name; }
            set
            {
                RoleEntity.Name = value;
                OnPropertyChanged(GetPropertyName(() => Name));
            }
        }

        /// <summary>
        ///     Gets or sets if the instance of <see cref="RoleViewModel" /> is marked in the UI.
        /// </summary>
        public bool IsMarked
        {
            get { return isMarked; }
            set
            {
                if (value == isMarked)
                    return;
                isMarked = value;
                OnPropertyChanged(GetPropertyName(() => IsMarked));
            }
        }

        /// <summary>
        ///     Returns if this instance of <see cref="RoleViewModel" /> is valid for saving.
        /// </summary>
        public bool IsValid
        {
            get { return PropertyNames.All(p => GetErrorForProperty(p) == null); }
        }

        /// <summary>
        ///     This method explicitly raises the property changed notification event for the <see cref="RoleId" />
        ///     property for this instance.
        /// </summary>
        public void Refresh()
        {
            OnPropertyChanged(GetPropertyName(() => RoleId));
        }
    }
}
