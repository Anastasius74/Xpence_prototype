using System.Collections.ObjectModel;
using XPence.DbAccess.Implementations;
using XPence.Models;
using XPence.Services.Interfaces;

namespace XPence.Services.Implementation
{
    public class ComponentAccessService : IComponentAccessService
    {
        private readonly UnitOfWork unitOfWorkOfComponent;

        public ComponentAccessService()
        {
            unitOfWorkOfComponent = new UnitOfWork();
            unitOfWorkOfComponent.Initialize();
        }

        public ObservableCollection<Component> SelectComponents()
        {
            ObservableCollection<Component> components = new ObservableCollection<Component>();

            foreach (var item in unitOfWorkOfComponent.Repository<Component>().GetAll())
            {
                components.Add(item);
            }

            return components;
            
        }

        public Component SelectComponent(long id)
        {
            return unitOfWorkOfComponent.Repository<Component>().GetById(id);
        }

        /// <summary>
        /// create a new Component.
        /// </summary>
        public void AddNewComponent(Component component)
        {
            unitOfWorkOfComponent.Repository<Component>().Insert(component);
        }

        /// <summary>
        /// Save a component
        /// </summary>
        public void SaveComponent()
        {
            unitOfWorkOfComponent.Commit();
        }

        /// <summary>
        /// delete Components.
        /// </summary>
        public void DeleteComponent(Component component)
        {
            unitOfWorkOfComponent.Repository<Component>().Delete(component);   
        }
    }
}
