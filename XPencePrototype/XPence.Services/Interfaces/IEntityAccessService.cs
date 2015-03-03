using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XPence.Services.Interfaces
{
    /// <summary>
    /// The Interface defining CRUD operations for the entitiesToDelete.  
    /// </summary>
    public interface IEntityAccessService<TEntity> 
    {
        ObservableCollection<TEntity> SelectAll();
        TEntity SelectById(long entityId);
        long Create(TEntity entityToCreate);
        void Update(TEntity entityToUpdate);
        void Save(TEntity entityToSave);
        void Delete(TEntity entityToDelete);
        void DeleteEntities(IEnumerable<TEntity> entitiesToDelete);
        void Commit();
        bool Initialized { get; }
        void Initialize();
        bool HasWorkToCommit { get; }
        void EnsureStartTransaction();
        void EnsureEndTransaction();
    }
}
