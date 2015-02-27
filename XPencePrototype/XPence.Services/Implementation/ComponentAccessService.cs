using System.Collections.ObjectModel;
using System.ComponentModel;
using XPence.DbAccess.Implementations;
using XPence.Services.Interfaces;

namespace XPence.Services.Implementation
{
    public class ComponentAccessService : IComponentAccessService
    {
        private readonly UnitOfWork unitOfWorkOfComponent;

        public ComponentAccessService()
        {
            unitOfWorkOfComponent = new UnitOfWork();
        }

        public ObservableCollection<Models.Component> GetComponents()
        {
            ObservableCollection<Component> components = new ObservableCollection<Component>();

            foreach (var item in unitOfWorkOfComponent.Repository<Component>().GetAll())
            {
                components.Add(item);
            }

            return components;
            
        }

        public long CreateComponent(Component component)
        {
            unitOfWorkOfComponent.Repository<Component>().Insert(component);

            return component.Id;
        }

        public void SaveComponent()
        {
            unitOfWorkOfComponent.Commit();
        }
    }
}
