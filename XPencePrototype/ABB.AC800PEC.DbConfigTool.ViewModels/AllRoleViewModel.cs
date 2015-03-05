using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ABB.AC800PEC.DbConfigTool.Infrastructure.BaseClasses;
using ABB.AC800PEC.DbConfigTool.Infrastructure.CoreClasses;
using ABB.AC800PEC.DbConfigTool.Infrastructure.MessagingService;
using ABB.AC800PEC.DbConfigTool.Models;
using ABB.AC800PEC.DbConfigTool.Services.Implementation;
using ABB.AC800PEC.DbConfigTool.Shared;

namespace ABB.AC800PEC.DbConfigTool.ViewModels
{
    public class AllRoleViewModel : WorkspaceViewModelBase
    {
        private readonly IMessagingService messagingService;
        private ExtendedObservableCollection<RoleViewModel> roles;
        private RoleViewModel selectedRole;
        private readonly EntityAccessService<Role> entityService;

        public AllRoleViewModel(string registeredName, IMessagingService messagingService)
            : base(registeredName)
        {
            if (messagingService == null)
                throw new ArgumentNullException("messagingService");
            this.messagingService = messagingService;

            entityService = new EntityAccessService<Role>();
            Roles = new ExtendedObservableCollection<RoleViewModel>();

            Refresh();

            //Initialize commands
            AddNewRoleCommand = new RelayCommand(CreateRole, CanCreateRole);
            UpdateRoleCommand = new RelayCommand(UpdateRole, CanUpdateRole);
            SaveRoleCommand = new RelayCommand(SaveRole, CanSaveRole);
            DeleteRolesCommand = new RelayCommand(DeleteRoles, CanDeleteRoles);
        }

        /// <summary>
        ///     Gets or sets the Roles viewable by the users.
        /// </summary>
        public ExtendedObservableCollection<RoleViewModel> Roles
        {
            get { return roles; }
            set
            {
                if (value == roles)
                    return;
                roles = value;
                OnPropertyChanged(GetPropertyName(() => Roles));
            }
        }

        /// <summary>
        ///     Gets or sets the Selected Role.
        /// </summary>
        public RoleViewModel SelectedRole
        {
            get { return selectedRole; }
            set
            {
                selectedRole = value;
                OnPropertyChanged(GetPropertyName(() => SelectedRole));
            }
        }

        private void Refresh()
        {
            Roles.Clear();
            var list = entityService.SelectAll().Select(t => new RoleViewModel(t));
            Roles.AddRange(list);
        }

        private void CreateRole()
        {
            entityService.EnsureStartTransaction();
            entityService.Create(SelectedRole.RoleEntity);
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Create);
        }

        private bool CanCreateRole()
        {
            if (Roles.Count == 1)
            {
                return false;
            }
            return true;
        }

        private void UpdateRole()
        {
            entityService.EnsureStartTransaction();
            entityService.Update(SelectedRole.RoleEntity);
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Update);
        }

        private bool CanUpdateRole()
        {
            if (null == SelectedRole)
            {
                return false;
            }

            return SelectedRole.IsValid;
        }

        /// <summary>
        ///     Save the selected transction.
        /// </summary>
        private void SaveRole()
        {
            entityService.EnsureStartTransaction();
            entityService.Commit();
            Refresh();
            entityService.EnsureEndTransaction();
        }

        private bool CanSaveRole()
        {
            if (null == SelectedRole)
            {
                return false;
            }

            return SelectedRole.IsValid;
        }

        /// <summary>
        ///     Deletes the Role marked.
        ///     If no Role id marked, the selected Role is deleted.
        /// </summary>
        private void DeleteRoles()
        {
            var markedEntity = Roles.Where(t => t.IsMarked);
            var roleViewModels = markedEntity as IList<RoleViewModel> ?? markedEntity.ToList();
            if (roleViewModels.Any())
            {
                var markedArray = roleViewModels.Select(t => t.RoleEntity);
                entityService.EnsureStartTransaction();
                entityService.DeleteEntities(markedArray);
                roleViewModels.ForEach(t => t.Refresh());
            }
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Del);
        }

        private bool CanDeleteRoles()
        {
            return Roles != null && Roles.Any();
        }

        #region Commands

        /// <summary>
        ///     Gets the command to save a Role.
        /// </summary>
        public ICommand SaveRoleCommand { get; private set; }

        /// <summary>
        ///     Gets the command to create a new Role.
        /// </summary>
        public ICommand AddNewRoleCommand { get; private set; }

        /// <summary>
        /// Gets the command to update a Role.
        /// </summary>
        public ICommand UpdateRoleCommand { get; private set; }

        /// <summary>
        ///     Gets the command to delete Roles.
        /// </summary>
        public ICommand DeleteRolesCommand { get; private set; }

        #endregion
    }
}