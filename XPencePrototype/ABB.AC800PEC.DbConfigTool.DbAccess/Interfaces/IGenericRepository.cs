using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ABB.AC800PEC.DbConfigTool.DbAccess.Interfaces
{
    /// <summary>
    /// Represents an abstract layer, which contains methods 
    /// to server data from data layer to business layer.
    /// </summary>
    /// <typeparam name="TEntity">The entity object.</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets or sets a DbSet with given the entity.
        /// </summary>
        DbSet<TEntity> DbSet { get; set; }

        /// <summary>
        /// Represents the input sequence of the DbSet instance.
        /// </summary>
        /// <returns>Returns the input sequence of the DbSet instance.</returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Get all entities from the context.
        /// </summary>
        /// <returns>All entities from the context.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get a single entity by given the primary key.
        /// </summary>
        /// <param name="id">the primary key.</param>
        /// <returns>A single entity.</returns>
        TEntity GetById(long id);

        /// <summary>
        /// Insert an entity to the context.
        /// </summary>
        /// <param name="entity">The entity object</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Delete an entity from the context with the given primary key.
        /// </summary>
        /// <param name="id">The primary key.</param>
        void Delete(long id);

        /// <summary>
        /// Delete an entity from the context with the given entity object.
        /// </summary>
        /// <param name="entityToDelete">The entity model object, which has to be deleted.</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Delete the selected entities.
        /// </summary>
        /// <param name="entities">The selected entities to be deleted.</param>
        void DeleteAll(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update an entity in the context with the given entity object.
        /// </summary>
        /// <param name="entityToUpdate">the entity model object, which has to be updated.</param>
        void Update(TEntity entityToUpdate);
    }
}