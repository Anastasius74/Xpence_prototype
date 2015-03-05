using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ABB.AC800PEC.DbConfigTool.DbAccess.Interfaces;

namespace ABB.AC800PEC.DbConfigTool.DbAccess.Implementations
{
    /// <summary>
    /// Represents an abstraction layer between the data access layer and the business logic layer. 
    /// Contains data manipulation methods which communicate with the data access layer to serve data 
    /// depending on the entity object.
    /// </summary>
    /// <typeparam name="TEntity">Entity model object.</typeparam>
    public sealed class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SimulinkContext context;
       
        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericRepository{TEntity}" /> class.
        ///     It is needed to initiates the application SimulinkContext.
        ///     Prepare the generic DbSet instance for access 
        ///     to entities of the given type in the context and the underlying store.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GenericRepository(SimulinkContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets or sets DbSet for access to entities of the given type 
        /// in the context and the underlying store.
        /// </summary>
        private DbSet<TEntity> dbSet;

        public DbSet<TEntity> DbSet
        {
            get
            {
                if(dbSet == null)dbSet = context.Set<TEntity>();
                return dbSet;
            }
            set { dbSet = value; }
        }

        /// <summary>
        /// Represents the input sequence of the DbSet instance.
        /// </summary>
        /// <returns>Returns the input sequence of the DbSet instance.</returns>
        public IQueryable<TEntity> AsQueryable()
        {
            return this.DbSet.AsQueryable();
        }

        /// <summary>
        /// Get all entries in the entity object.
        /// </summary>
        /// <returns>Returns the Dbset instance</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return this.DbSet;
        }

        /// <summary>
        /// Finds an entity with the given primary key values.
        /// </summary>
        /// <param name="id">primary key</param>
        /// <returns>An entity</returns>
        public TEntity GetById(long id)
        {
            return this.DbSet.Find(id);
        }

        /// <summary>
        /// Insert the given entity to the context underlying the set in the Added state.
        /// </summary>
        /// <param name="entity">The entity model object.</param>
        public void Insert(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        /// <summary>
        /// Delete the entity with the given primary key values.
        /// </summary>
        /// <param name="id">The primary key.</param>
        public void Delete(long id)
        {
            var entityToDelete = this.DbSet.Find(id);
            this.Delete(entityToDelete);
        }

        /// <summary>
        /// Delete the entity from the context with the given the entity object.
        /// </summary>
        /// <param name="entityToDelete">the entity which has to be deleted from the context.</param>
        public void Delete(TEntity entityToDelete)
        {
            if (this.context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.context.Set<TEntity>().Attach(entityToDelete);
            }

            this.context.Set<TEntity>().Remove(entityToDelete);
        }

        /// <summary>
        /// Delete the selected entities.
        /// </summary>
        /// <param name="entities">The selected entities to be deleted.</param>
        public void DeleteAll(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// Update the entity in the context with the given the entity model object.
        /// </summary>
        /// <param name="entityToUpdate">The entity which has to be updated in the context.</param>
        public void Update(TEntity entityToUpdate)
        {
            this.context.Set<TEntity>().Attach(entityToUpdate);
            this.context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}