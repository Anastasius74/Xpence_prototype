using System;
using System.Collections.Generic;
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
            Initialize();
        }

        public ObservableCollection<TModel> SelectAll()
        {
            var models = new ObservableCollection<TModel>();

            foreach (TModel item in unitOfWork.Repository<TModel>().GetAll())
            {
                models.Add(item);
            }

            return models;
        }

        public TModel SelectById(long entityId)
        {
            return unitOfWork.Repository<TModel>().GetById(entityId);
        }

        public long Create(TModel entityToCreate)
        {
            unitOfWork.Repository<TModel>().Insert(entityToCreate);
            return entityToCreate.GetDatabaseId().Value;
        }

        public void Update(TModel entityToUpdate)
        {
            unitOfWork.Repository<TModel>().Update(entityToUpdate);
        }

        public void Save(TModel entityToSave)
        {
            unitOfWork.Repository<TModel>().Insert(entityToSave);
        }

        public void Delete(TModel entityToDelete)
        {
            unitOfWork.Repository<TModel>().Delete(entityToDelete);
        }

        public void DeleteEntities(IEnumerable<TModel> entitiesToDelete)
        {
            unitOfWork.Repository<TModel>().DeleteAll(entitiesToDelete);
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

        public void EnsureStartTransaction()
        {
            unitOfWork.EnsureStartTransaction();
        }

        public void EnsureEndTransaction()
        {
            unitOfWork.EnsureEndTransaction();
        }
    }
}
