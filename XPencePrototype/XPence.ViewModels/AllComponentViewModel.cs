using System.Windows.Input;
using XPence.Infrastructure.BaseClasses;
using XPence.Infrastructure.CoreClasses;

namespace XPence.ViewModels
{
    public class AllComponentViewModel : WorkspaceViewModelBase
    {
        private ExtendedObservableCollection<ComponentViewModel> components;
        private ComponentViewModel selectedComponent;
        
        public AllComponentViewModel()
        {
            
        }

        /// <summary>
        /// Gets or sets the components viewable by the users.
        /// </summary>
        public ExtendedObservableCollection<ComponentViewModel> Components
        {
            get { return components; }
            set
            {
                if (value == components)
                    return;
                components = value;
                OnPropertyChanged(GetPropertyName(() => Components));
            }
        }

        /// <summary>
        /// Gets or sets the Selected component.
        /// </summary>
        public ComponentViewModel SelectedComponent
        {
            get
            {
                return selectedComponent;
            }
            set
            {
                selectedComponent = value;
                OnPropertyChanged(GetPropertyName(() => SelectedComponent));
            }
        }

        #region Commands

        /// <summary>
        /// Gets the command to save a Component.
        /// </summary>
        public ICommand SaveComponentCommand { get; private set; }

        /// <summary>
        /// Gets the command to create a new Component.
        /// </summary>
        public ICommand AddNewComponentCommand { get; private set; }

        /// <summary>
        /// Gets the command to delete Components.
        /// </summary>
        public ICommand DeleteComponentsCommand { get; private set; }

        #endregion

    }
}
