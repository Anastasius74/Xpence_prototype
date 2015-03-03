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
    public class AllNodeViewModel : WorkspaceViewModelBase
    {
        private readonly IEntityAccessService<Node> nodeService;
        private readonly IMessagingService messagingService;
        private ExtendedObservableCollection<NodeViewModel> nodes;
        private NodeViewModel selectedNode;

        public AllNodeViewModel(string registeredName, IMessagingService messagingService)
            : base(registeredName)
        {
            if (messagingService == null)
                throw new ArgumentNullException("messagingService");
            this.messagingService = messagingService;

            nodeService = new EntityAccessService<Node>();
            nodes = new ExtendedObservableCollection<NodeViewModel>();

            Refresh();

            //Initialize commands
            AddNewNodeCommand = new RelayCommand(CreateNode, CanCreateNode);
            UpdateNodeCommand = new RelayCommand(UpdateNode, CanUpdateNode);
            SaveNodeCommand = new RelayCommand(SaveNode, CanSaveNode);
            DeleteNodesCommand = new RelayCommand(DeleteNodes, CanDeleteNodes);
        }

        /// <summary>
        ///     Gets or sets the Nodes viewable by the users.
        /// </summary>
        public ExtendedObservableCollection<NodeViewModel> Nodes
        {
            get { return nodes; }
            set
            {
                if (value == nodes)
                    return;
                nodes = value;
                OnPropertyChanged(GetPropertyName(() => Nodes));
            }
        }

        /// <summary>
        ///     Gets or sets the Selected Node.
        /// </summary>
        public NodeViewModel SelectedNode
        {
            get { return selectedNode; }
            set
            {
                selectedNode = value;
                OnPropertyChanged(GetPropertyName(() => SelectedNode));
            }
        }

        private void Refresh()
        {
            nodes.Clear();
            var list = nodeService.SelectAll().Select(t => new NodeViewModel(t));
            nodes.AddRange(list);
        }

        private void CreateNode()
        {
            nodeService.EnsureStartTransaction();
            nodeService.Create(SelectedNode.NodeEntity);
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Create);
        }

        private bool CanCreateNode()
        {
            if (Nodes.Count == 1)
            {
                return false;
            }
            return true;
        }

        private void UpdateNode()
        {
            nodeService.EnsureStartTransaction();
            nodeService.Update(SelectedNode.NodeEntity);
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Update);
        }

        private bool CanUpdateNode()
        {
            if (null == SelectedNode)
            {
                return false;
            }

            return SelectedNode.IsValid;
        }

        /// <summary>
        ///     Save the selected transction.
        /// </summary>
        private void SaveNode()
        {
            nodeService.EnsureStartTransaction();
            nodeService.Commit();
            Refresh();
            nodeService.EnsureEndTransaction();
        }

        private bool CanSaveNode()
        {
            if (null == SelectedNode)
            {
                return false;
            }

            return SelectedNode.IsValid;
        }

        /// <summary>
        ///     Deletes the Node marked.
        ///     If no Node id marked, the selected Node is deleted.
        /// </summary>
        private void DeleteNodes()
        {
            var markedEntity = Nodes.Where(t => t.IsMarked);
            var nodeViewModels = markedEntity as IList<NodeViewModel> ?? markedEntity.ToList();
            if (nodeViewModels.Any())
            {
                var markedArray = nodeViewModels.Select(t => t.NodeEntity);
                nodeService.EnsureStartTransaction();
                nodeService.DeleteEntities(markedArray);
                nodeViewModels.ForEach(t => t.Refresh());
            }
            messagingService.ShowMessage(InfoMessages.Inf_Mark_For_Del);
        }

        private bool CanDeleteNodes()
        {
            return Nodes != null && Nodes.Any();
        }

        #region Commands

        /// <summary>
        ///     Gets the command to save a Node.
        /// </summary>
        public ICommand SaveNodeCommand { get; private set; }

        /// <summary>
        ///     Gets the command to create a new Node.
        /// </summary>
        public ICommand AddNewNodeCommand { get; private set; }

        /// <summary>
        /// Gets the command to update a node.
        /// </summary>
        public ICommand UpdateNodeCommand { get; private set; }

        /// <summary>
        ///     Gets the command to delete Nodes.
        /// </summary>
        public ICommand DeleteNodesCommand { get; private set; }

        #endregion
    }
}