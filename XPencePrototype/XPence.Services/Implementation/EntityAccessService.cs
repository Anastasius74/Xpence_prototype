using System;
using System.Collections.ObjectModel;
using XPence.DbAccess.Implementations;
using XPence.Services.Interfaces;

namespace XPence.Services.Implementation
{
    public class EntityAccessService<TModel> : IEntityAccessService<TModel> where TModel : class 
    {
        private readonly UnitOfWork unitOfWork;

        public EntityAccessService()
        {
            unitOfWork = new UnitOfWork();
        }

        public ObservableCollection<TModel> GetAll()
        {
            ObservableCollection<TModel> models = new ObservableCollection<TModel>();

            foreach (var item in unitOfWork.Repository<TModel>().GetAll())
            {
                models.Add(item);
            }

            return models;
        }

        public TModel GetById(long entityId)
        {
            return unitOfWork.Repository<TModel>().GetById(entityId);
        }

        public long Create(TModel entity)
        {
            unitOfWork.Repository<TModel>().Insert(entity);
            return entity.GetDatabaseId().Value;
        }

        public void Save(TModel entity)
        {
            unitOfWork.Repository<TModel>().Insert(entity);
        }

        public void Delete(TModel entity)
        {
            unitOfWork.Repository<TModel>().Delete(entity);
        }

        public void Commit()
        {
            if (HasWorkToCommit)
            {
                unitOfWork.Commit();
            }
        }

        public bool Initialized { get; private set; }

        public void Initialize()
        {
            if (!Initialized)
            {
                unitOfWork.Initialize();
                Initialized = true;
            }
        }

        public bool HasWorkToCommit
        {
            get { return unitOfWork.HasWork; }
        }
    }
}
