using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using XPence.Infrastructure.BaseClasses;
using XPence.Infrastructure.CoreClasses;
using XPence.Infrastructure.MessagingService;
using XPence.Models;
using XPence.Services.Implementation;
using XPence.Services.Interfaces;
using XPence.Shared;

namespace XPence.ViewModels
{
    public class AllComponentViewModel : WorkspaceViewModelBase
    {
        private ExtendedObservableCollection<ComponentViewModel> components;
        private ComponentViewModel selectedComponent;
        private readonly IEntityAccessService<Component> componentService;
        private readonly IMessagingService messagingService;
        
        public AllComponentViewModel(string registeredName, IMessagingService messagingService): base(registeredName)
        {
            if (messagingService == null)
                throw new ArgumentNullException("messagingService");
            this.messagingService = messagingService;

            componentService = new EntityAccessService<Component>();
            Components = new ExtendedObservableCollection<ComponentViewModel>();
            
            var list = componentService.SelectAll().Select(t => new ComponentViewModel(t));
            Components.AddRange(list);

            //Initialize commands
            AddNewComponentCommand = new RelayCommand(CreateComponent);
            SaveComponentCommand = new RelayCommand(SaveComponent, CanSaveComponent);
            DeleteComponentsCommand = new RelayCommand(DeleteComponents);
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

        private void CreateComponent()
        {
            componentService.Create(SelectedComponent.ComponentEntity);
            componentService.Commit();
        }

        /// <summary>
        /// Save the selected transction.
        /// </summary>
        private void SaveComponent()
        {
            //messagingService.ShowProgressMessage(UIText.WAIT_SCREEN_HEADER, UIText.SAVING_Entity_WAIT_MSG);
            componentService.Save(SelectedComponent.ComponentEntity);
            componentService.Commit();
        }

        private bool CanSaveComponent()
        {
            if (null == SelectedComponent)
            {
                return false;
            }

            return SelectedComponent.IsValid;
        }

        /// <summary>
        /// Deletes the component marked.
        /// If no component id marked, the selected component is deleted.
        /// </summary>
        private void DeleteComponents()
        {
            if (Components.Any())
            {
                var markedTrans = Components.Where(t => t.IsMarked);
                IList<ComponentViewModel> componentViewModels = markedTrans as IList<ComponentViewModel> ?? markedTrans.ToList();
                if (componentViewModels.Any())
                {
                    IEnumerable<Component> markedArray = componentViewModels.Select(t => t.ComponentEntity);
                    messagingService.ShowProgressMessage(UIText.WAIT_SCREEN_HEADER, UIText.DELETING_Components_WAIT_MSG);
                    componentService.DeleteEntities(markedArray);
                    return;
                }
            }
            messagingService.ShowMessage(InfoMessages.INF_MARK_FOR_DEL);
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
