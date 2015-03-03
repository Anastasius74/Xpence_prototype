using System;
using System.Data.Entity;
using XPence.DbAccess.Interfaces;

namespace XPence.DbAccess.Implementations
{
    /// <summary>
    ///     Maintains a list of objects affected by a business transaction and coordinates
    ///     the writing out of changes and the resolution of concurrency problem.
    ///     Inherits from an IDisposable interface so that its instance will be disposed.
    ///     Initiates the application DataContext.
    ///     Returns a repository for the entity.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed;
        private DbContextTransaction transaction;
        private SimulinkContext context;
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Reflects whether a commit is required to close the object
        /// </summary>
        public bool HasWork { get; private set; }

        /// <summary>
        ///     Gets or sets the audit log attribute to determines the auditing.
        /// </summary>
        public bool EnableAuditlog { get; set; }

        public void Initialize()
        {
            if (!IsInitialized)
            {
                context = new SimulinkContext();
                IsInitialized = true;
            }
        }

        public void EnsureStartTransaction()
        {
            Initialize();
            if (!HasWork)
            {
                transaction = context.Database.BeginTransaction();
                HasWork = true;
            }
        }

        public void EnsureEndTransaction()
        {
            HasWork = false;
            Dispose();
            IsInitialized = false;
        }

        public static UnitOfWork GetInitialized()
        {
            var returnValue = new UnitOfWork();
            returnValue.Initialize();
            return returnValue;
        }


        /// <summary>
        ///     Dispose the context.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Create a generic reposistory instance for the entity object.
        ///     to perform all CRUD operation methods depending on the entity object.
        /// </summary>
        /// <typeparam name="TEntity">The entity model object.</typeparam>
        /// <returns>GenericRepository instance with the given entity and initiating of the context.</returns>
        public GenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(this.context);
        }

        /// <summary>
        ///     Take snapshot of each changes by storing entries.
        ///     Allows to backward and forward any changes with the DBContext.
        /// </summary>
        public void TakeSnapshot()
        {
            EnsureStartTransaction();
            this.context.TakeSnapshot();
        }

        /// <summary>
        ///     Commit all changes in the context.
        /// </summary>
        public void Commit()
        {
            try
            {
                this.context.Commit();
                this.transaction.Commit();
            }
            catch (Exception exception)
            {
                this.transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        ///     Undo the last action, which is performed.
        /// </summary>
        public void Undo()
        {
            EnsureStartTransaction();
            this.context.Undo();
        }

        /// <summary>
        ///     Redo undoes the last Undo action.
        /// </summary>
        public void Redo()
        {
            EnsureStartTransaction();
            this.context.Redo();
        }

        /// <summary>
        ///     Undo the last saved changes from the database.
        /// </summary>
        public void Revert()
        {
            EnsureStartTransaction();
            this.context.Revert();
        }

        /// <summary>
        ///     Can execute the undo feature.
        /// </summary>
        /// <returns>True or false</returns>
        public bool CanUndo()
        {
            return this.context.CanUndo();
        }

        /// <summary>
        ///     Can execute the redo feature.
        /// </summary>
        /// <returns>True or false</returns>
        public bool CanRedo()
        {
            return this.context.CanRedo();
        }

        /// <summary>
        ///     Can execute the revert feature.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool CanRevert()
        {
            return this.context.CanRevert();
        }

        /// <summary>
        /// Dispose the transaction and the context.
        /// </summary>
        /// <param name="disposing">True or False.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.transaction.Dispose();
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}