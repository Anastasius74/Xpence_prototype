using System;

namespace XPence.DbAccess.Interfaces
{
    /// <summary>
    /// Interface <see cref="IUnitOfWork"/> declare unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit changes
        /// </summary>
        void Commit();
    }
}
