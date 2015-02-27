using System.Collections.ObjectModel;
using XPence.DbAccess.Interfaces;
using XPence.Models;

namespace XPence.Services.Interfaces
{
    /// <summary>
    /// The Interface defining methods for Create Component and Read All Components  
    /// </summary>
    public interface IEntityAccessService<TEntity> 
    {
        ObservableCollection<TEntity> GetAll();
        TEntity GetById(long entityId);
        long Create(TEntity entity);
        void Save(TEntity entity);
        void Delete(TEntity entity);
        void Commit();
        bool Initialized { get; }
        void Initialize();
        bool HasWorkToCommit { get; }
    }
}
