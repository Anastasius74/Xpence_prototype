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
        private readonly IEntityAccessService<Component> componentService;
        private readonly IMessagingService messagingService;
        private ExtendedObservableCollection<ComponentViewModel> components;
        private ComponentViewModel selectedComponent;

        public AllComponentViewModel(string registeredName, IMessagingService messagingService) : base(registeredName)
        {
            if (messagingService == null)
                throw new ArgumentNullException("messagingService");
            this.messagingService = messagingService;

            componentService = new EntityAccessService<Component>();
            Components = new ExtendedObservableCollection<ComponentViewModel>();

            Refresh();

            //Initialize commands
            AddNewComponentCommand = new RelayCommand(CreateComponent, CanCreateComponent);
            UpdateComponentCommand = new RelayCommand(UpdateComponent, CanUpdateComponent);
            SaveComponentCommand = new RelayCommand(SaveComponent, CanSaveComponent);
            DeleteComponentsCommand = new RelayCommand(DeleteComponents, CanDeleteComponents);
        }

        /// <summary>
        ///     Gets or sets the components viewable by the users.
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
        ///     Gets or sets the Selected component.
        /// </summary>
        public ComponentViewModel SelectedComponent
        {
            get { return selectedComponent; }
            set
            {
                selectedComponent = value;
                OnPropertyChanged(GetPropertyName(() => SelectedComponent));
            }
        }

        private void Refresh()
        {
            Components.Clear();
            var list = componentService.SelectAll().Select(t => new ComponentViewModel(t));
            Components.AddRange(list);
        }

        private void CreateComponent()
        {
            componentService.EnsureStartTransaction();
            componentService.Create(SelectedComponent.ComponentEntity);
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Create);
        }

        private bool CanCreateComponent()
        {
            if (null == SelectedComponent)
            {
                return false;
            }

            return SelectedComponent.IsValid;
        }

        private void UpdateComponent()
        {
            componentService.EnsureStartTransaction();
            componentService.Update(SelectedComponent.ComponentEntity);
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Update);
        }

        private bool CanUpdateComponent()
        {
            if (null == SelectedComponent)
            {
                return false;
            }

            return SelectedComponent.IsValid;
        }

        /// <summary>
        ///     Save the selected transction.
        /// </summary>
        private void SaveComponent()
        {
            componentService.EnsureStartTransaction();
            componentService.Commit();
            Refresh();
            componentService.EnsureEndTransaction();
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
        ///     Deletes the component marked.
        ///     If no component id marked, the selected component is deleted.
        /// </summary>
        private void DeleteComponents()
        {
            var markedEntity = Components.Where(t => t.IsMarked);
            var componentViewModels = markedEntity as IList<ComponentViewModel> ?? markedEntity.ToList();
            if (componentViewModels.Any())
            {
                var markedArray = componentViewModels.Select(t => t.ComponentEntity);
                componentService.EnsureStartTransaction();
                componentService.DeleteEntities(markedArray);
                componentViewModels.ForEach(t => t.Refresh());
            }
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Del);
        }

        private bool CanDeleteComponents()
        {
            return Components != null && Components.Any();
        }

        #region Commands

        /// <summary>
        ///     Gets the command to save a Component.
        /// </summary>
        public ICommand SaveComponentCommand { get; private set; }

        /// <summary>
        ///     Gets the command to create a new Component.
        /// </summary>
        public ICommand AddNewComponentCommand { get; private set; }

        /// <summary>
        /// Gets the command to update a component.
        /// </summary>
        public ICommand UpdateComponentCommand { get; private set; }

        /// <summary>
        ///     Gets the command to delete Components.
        /// </summary>
        public ICommand DeleteComponentsCommand { get; private set; }

        #endregion
    }
}