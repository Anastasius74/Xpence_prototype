using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using XPence.Infrastructure.BaseClasses;
using XPence.Infrastructure.CoreClasses;
using XPence.Infrastructure.MessagingService;
using XPence.Models;
using XPence.Services.Implementation;
using XPence.Shared;

namespace XPence.ViewModels
{
    public class AllComponentViewModel : WorkspaceViewModelBase
    {
        private readonly IMessagingService messagingService;
        private ExtendedObservableCollection<ComponentViewModel> components;
        private ComponentViewModel selectedComponent;
        private ObservableCollection<ComponentViewModel> selectedComponents;
        private ObservableCollection<ComponentViewModel> copiedComponents;
        private EntityAccessService<Component> entityService; 

        public AllComponentViewModel(string registeredName, IMessagingService messagingService) : base(registeredName)
        {
            if (messagingService == null)
                throw new ArgumentNullException("messagingService");
            this.messagingService = messagingService;
            Initialize();
        }

        private new void Initialize()
        {
            Components = new ExtendedObservableCollection<ComponentViewModel>();
            copiedComponents = new ObservableCollection<ComponentViewModel>();
            SelectedComponents = new ObservableCollection<ComponentViewModel>();
            entityService = new EntityAccessService<Component>();

            //Initialize commands
            AddNewComponentCommand = new RelayCommand(CreateComponent, CanCreateComponent);
            UpdateComponentCommand = new RelayCommand(UpdateComponent, CanUpdateComponent);
            SaveComponentCommand = new RelayCommand(SaveComponent, CanSaveComponent);
            DeleteComponentsCommand = new RelayCommand(DeleteComponents, CanDeleteComponents);
            SelectionChangedCommand = new RelayCommand<IList>(GetSelectedComponents);
            CopyCommand = new RelayCommand(CopyComponent, CanCopyComponent);
            PasteCommand = new RelayCommand(PasteComponent, CanPasteComponent);

            GetComponents();
        }

        private bool CanPasteComponent()
        {
            return copiedComponents != null && copiedComponents.Count > 0;
        }

        private void PasteComponent()
        {
            entityService.EnsureStartTransaction();
            foreach (var componentViewModel in SelectedComponents)
            {
                entityService.Create(componentViewModel.ComponentEntity);
            }
            
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Create);
        }

        private bool CanCopyComponent()
        {
            return SelectedComponents != null && SelectedComponents.Count > 0;
        }

        private void CopyComponent()
        {
            copiedComponents.Clear();

            foreach (var componentViewModel in SelectedComponents)
            {
                copiedComponents.Add(componentViewModel);
            }

        }

        private void GetSelectedComponents(IList componentList)
        {
            SelectedComponents.Clear();

            var collection = componentList.Cast<ComponentViewModel>();
            var componentViewModels = collection as IList<ComponentViewModel> ?? collection.ToList();

            if (componentViewModels.Count > 0)
            {
                SelectedComponent = componentViewModels.Select(x => x).Last();
                foreach (ComponentViewModel item in componentViewModels)
                {
                    SelectedComponents.Add(item);
                }
            }
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

        public ObservableCollection<ComponentViewModel> SelectedComponents
        {
            get
            {
                return selectedComponents;
            }
            set
            {
                selectedComponents = value;
                OnPropertyChanged(GetPropertyName(() => SelectedComponents));
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

        private void GetComponents()
        {
            Components.Clear();
            var list = entityService.SelectAll().Select(t => new ComponentViewModel(t));
            Components.AddRange(list);
        }

        private void CreateComponent()
        {
            entityService.EnsureStartTransaction();
            entityService.Create(SelectedComponent.ComponentEntity);
            GetComponents();
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
            entityService.EnsureStartTransaction();
            entityService.Update(SelectedComponent.ComponentEntity);
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
            entityService.EnsureStartTransaction();
            entityService.Commit();
            GetComponents();
            entityService.EnsureEndTransaction();
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
            List<Component> items = SelectedComponents.Select(componentViewModel => componentViewModel.ComponentEntity).ToList();
            entityService.EnsureStartTransaction();
            entityService.DeleteEntities(items);
            SelectedComponents.ForEach(t => t.Refresh());
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Del);
        }

        private bool CanDeleteComponents()
        {
            return SelectedComponents != null && SelectedComponents.Count > 0;
        }

        #region Commands

        /// <summary>
        ///     Gets the command to save a Component.
        /// </summary>
        public ICommand SaveComponentCommand { get; set; }

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

        /// <summary>
        /// Gets or sets the selected componentes.
        /// </summary>
        public RelayCommand<IList> SelectionChangedCommand { get; set; }

        /// <summary>
        /// Gets or sets the copied components.
        /// </summary>
        public ICommand CopyCommand { get; set; }

        /// <summary>
        /// Gets or sets the pasted components.
        /// </summary>
        public ICommand PasteCommand { get; set; }

        #endregion
    }
}