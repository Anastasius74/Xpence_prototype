using System.Linq;
using XPence.Framework;
using XPence.Infrastructure.BaseClasses;
using XPence.Models;

namespace XPence.ViewModels
{
    public class ComponentViewModel : ViewModelBase
    {
        private static readonly string[] PropertyNames =
        {
            "ComponentName", "ComponentIsStorageOwner", "ComponentLayer", "ComponentCore, ComponentModule"
        };

        /// <summary>
        ///     Gets or sets the wrapped up component model.
        /// </summary>
        public Component ComponentEntity { get; private set; }

        /// <summary>
        ///     Intializes an instance of <see cref="ComponentViewModel" />.
        /// </summary>
        /// <param name="componentEntity"></param>
        public ComponentViewModel(Component componentEntity)
        {
            Guard.ArgumentNotNull("componentEntity", "componentEntity");
            ComponentEntity = componentEntity;
        }

        /// <summary>
        ///     Gets the component id.
        /// </summary>
        public long ComponentId
        {
            get { return ComponentEntity.Id; }
        }

        /// <summary>
        ///     Gets or sets the component name.
        /// </summary>
        public string ComponentName
        {
            get { return ComponentEntity.Name; }
            set
            {
                ComponentEntity.Name = value;
                OnPropertyChanged(GetPropertyName(() => ComponentName));
            }
        }

        /// <summary>
        ///     Gets or sets the component isStorageOwner.
        /// </summary>
        public string ComponentIsStorageOwner
        {
            get
            {
                return ComponentEntity.ComponentFunction == null ? string.Empty : ComponentEntity.ComponentFunction.Name;
            }
            set
            {
                ComponentEntity.ComponentFunction.Name = value;
                OnPropertyChanged(GetPropertyName(() => ComponentIsStorageOwner));
            }
        }

        /// <summary>
        ///     Gets or sets the component layer.
        /// </summary>
        public string ComponentLayer
        {
            get { return ComponentEntity.Layer.Label; }
            set
            {
                ComponentEntity.Layer.Label = value;
                OnPropertyChanged(GetPropertyName(() => ComponentLayer));
            }
        }

        public Layer Layer { get; set; }

        /// <summary>
        ///     Gets or sets the component core.
        /// </summary>
        public long? ComponentCore
        {
            get { return ComponentEntity.Core; }
            set
            {
                ComponentEntity.Core = value;
                OnPropertyChanged(GetPropertyName(() => ComponentCore));
            }
        }

        /// <summary>
        ///     Gets or sets the component module.
        /// </summary>
        public string ComponentModule
        {
            get { return ComponentEntity.Module; }
            set
            {
                ComponentEntity.Module = value;
                OnPropertyChanged(GetPropertyName(() => ComponentModule));
            }
        }

        /// <summary>
        ///     Returns if this instance of <see cref="ComponentViewModel" /> is valid for saving.
        /// </summary>
        public bool IsValid
        {
            get { return PropertyNames.All(p => GetErrorForProperty(p) == null); }
        }

        /// <summary>
        ///     This method explicitly raises the property changed notification event for the <see cref="ComponentId" />
        ///     property for this instance.
        /// </summary>
        public void Refresh()
        {
            OnPropertyChanged(GetPropertyName(() => ComponentId));
        }
    }
}
