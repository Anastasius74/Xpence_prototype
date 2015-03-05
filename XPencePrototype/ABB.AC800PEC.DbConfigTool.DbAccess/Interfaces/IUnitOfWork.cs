using System;

namespace ABB.AC800PEC.DbConfigTool.DbAccess.Interfaces
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
