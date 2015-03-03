using System.Linq;
using XPence.Framework;
using XPence.Infrastructure.BaseClasses;
using XPence.Models;

namespace XPence.ViewModels
{
    public class NodeViewModel : ViewModelBase
    {
        private static readonly string[] PropertyNames = { "NetworkName" };
        private bool isMarked;

        /// <summary>
        ///     Gets or sets the wrapped up Node model.
        /// </summary>
        public Node NodeEntity { get; private set; }

        /// <summary>
        ///     Intializes an instance of <see cref="NodeViewModel" />.
        /// </summary>
        /// <param name="nodeEntity"></param>
        public NodeViewModel(Node nodeEntity)
        {
            Guard.ArgumentNotNull("nodeEntity", "nodeEntity");
            NodeEntity = nodeEntity;
        }

        /// <summary>
        ///     Gets the Node id.
        /// </summary>
        public long NodeId
        {
            get { return NodeEntity.Id; }
        }

        /// <summary>
        ///     Gets or sets the Node network name.
        /// </summary>
        public string NetworkName
        {
            get { return NodeEntity.NetworkName; }
            set
            {
                NodeEntity.NetworkName = value;
                OnPropertyChanged(GetPropertyName(() => NetworkName));
            }
        }

        /// <summary>
        ///     Gets or sets if the instance of <see cref="NodeViewModel" /> is marked in the UI.
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
        ///     Returns if this instance of <see cref="NodeViewModel" /> is valid for saving.
        /// </summary>
        public bool IsValid
        {
            get { return PropertyNames.All(p => GetErrorForProperty(p) == null); }
        }

        /// <summary>
        ///     This method explicitly raises the property changed notification event for the <see cref="NodeId" />
        ///     property for this instance.
        /// </summary>
        public void Refresh()
        {
            OnPropertyChanged(GetPropertyName(() => NodeId));
        }
    }
}
