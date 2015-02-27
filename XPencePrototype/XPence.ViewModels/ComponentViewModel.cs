
using XPence.Framework;
using XPence.Infrastructure.BaseClasses;
using XPence.Models;

namespace XPence.ViewModels
{
    public class ComponentViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the wrapped up component model.
        /// </summary>
        public Component ComponentEntity { get; private set; }

        /// <summary>
        /// Intializes an instance of <see cref="ComponentViewModel"/>.
        /// </summary>
        /// <param name="entity"></param>
        public ComponentViewModel(Component entity)
        {
            Guard.ArgumentNotNull("entity", "entity");
            ComponentEntity = entity;
        }

        /// <summary>
        /// Gets the component id.
        /// </summary>
        public long ComponentId
        {
            get { return ComponentEntity.Id; }
        }

        /// <summary>
        /// Gets or sets the component name.
        /// </summary>
        public string ComponentName
        {
            get { return ComponentEntity.Name; }
            set { ComponentEntity.Name = value; }
        }

        /// <summary>
        /// Gets or sets the component isStorageOwner.
        /// </summary>
        public long ComponentIsStorageOwner
        {
            get { return ComponentEntity.IsStorageOwner; }
            set { ComponentEntity.IsStorageOwner = value; }
        }

        /// <summary>
        /// Gets or sets the component layer.
        /// </summary>
        public long ComponentLayer
        {
            get { return ComponentEntity.Layer; }
            set { ComponentEntity.Layer = value; }
        }

        /// <summary>
        /// Gets or sets the component core.
        /// </summary>
        public long? ComponentCore
        {
            get { return ComponentEntity.Core; }
            set { ComponentEntity.Core = value; }
        }

        /// <summary>
        /// This method explicitly raises the property changed notification event for the <see cref="ComponentId"/>
        /// property for this instance.
        /// </summary>
        public void Refresh()
        {
            OnPropertyChanged(GetPropertyName(() => ComponentId));
        }
    }
}
