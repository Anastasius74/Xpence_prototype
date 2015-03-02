using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XPence.Services.Interfaces
{
    /// <summary>
    /// The Interface defining CRUD operations for the entities.  
    /// </summary>
    public interface IEntityAccessService<TEntity> 
    {
        ObservableCollection<TEntity> SelectAll();
        TEntity SelectById(long entityId);
        long Create(TEntity entity);
        void Save(TEntity entity);
        void Delete(TEntity entity);
        void DeleteEntities(IEnumerable<TEntity> entities);
        void Commit();
        bool Initialized { get; }
        void Initialize();
        bool HasWorkToCommit { get; }
    }
}
