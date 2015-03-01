using System;
using System.Linq;
using System.Windows.Input;
using XPence.Infrastructure.BaseClasses;
using XPence.Infrastructure.CoreClasses;
using XPence.Infrastructure.MessagingService;
using XPence.Services.Implementation;
using XPence.Services.Interfaces;
using XPence.Shared;

namespace XPence.ViewModels
{
    public class AllComponentViewModel : WorkspaceViewModelBase
    {
        private ExtendedObservableCollection<ComponentViewModel> components;
        private ComponentViewModel selectedComponent;
        private IComponentAccessService componentService;
        private readonly IMessagingService messagingService;
        
        public AllComponentViewModel(string registeredName, IMessagingService messagingService): base(registeredName)
        {
            if (messagingService == null)
                throw new ArgumentNullException("messagingService");
            this.messagingService = messagingService;

            componentService = new ComponentAccessService();
            Components = new ExtendedObservableCollection<ComponentViewModel>();
            
            var list = componentService.SelectComponents().Select(t => new ComponentViewModel(t));
            Components.AddRange(list);
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

        /// <summary>
        /// Save the selected transction.
        /// </summary>
        private void SaveTransaction()
        {
            messagingService.ShowProgressMessage(UIText.WAIT_SCREEN_HEADER, UIText.SAVING_TRANS_WAIT_MSG);
           //_transactionRepository.SaveTransactionAsync(SelectedTransaction.Entity);
        }

        private bool CanSaveTransaction()
        {
            //if (null == SelectedTransaction)
            //    return false;
            //return SelectedTransaction.IsValid;
            return true;
        }

        /// <summary>
        /// Deletes the transaction marked.
        /// If no transaction id marked, the selected transaction is deleted.
        /// </summary>
        private void DeleteTransactions()
        {
            if (Components.Any())
            {
                var markedTrans = Components.Where(t => t.IsMarked);
                if (markedTrans.Any())
                {
                    //var markedArray = markedTrans.Select(t => t.Entity).ToArray();
                    //messagingService.ShowProgressMessage(UIText.WAIT_SCREEN_HEADER, UIText.DELETING_TRANS_WAIT_MSG);
                    //_transactionRepository.DeleteTransactionsAsync(markedArray);
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
