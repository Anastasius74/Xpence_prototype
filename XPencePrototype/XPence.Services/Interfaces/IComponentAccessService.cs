using System.Collections.ObjectModel;
using XPence.Models;

namespace XPence.Services.Interfaces
{
    /// <summary>
    /// The Interface defining methods for Create Component and Read All Components  
    /// </summary>
    public interface IComponentAccessService
    {
        ObservableCollection<Component> SelectComponents();
        Component SelectComponent(long id);
        void AddNewComponent(Component component);
        void SaveComponent();
        void DeleteComponent(Component component);
    }
}
