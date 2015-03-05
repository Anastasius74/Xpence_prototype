using System;
using System.Collections.Generic;

namespace ABB.AC800PEC.DbConfigTool.DbAccess.Implementations
{
    /// <summary>
    /// Represents a collection of the UnitOfWork for the entity models.
    /// </summary>
    public class UnitOfWorkCollection
    {
        /// <summary>
        /// Create instance of the generic List for UnitOfWork instances.
        /// </summary>
        public readonly List<UnitOfWork> UnitOfWorks = new List<UnitOfWork>();
      
        /// <summary>
        /// Prepare the a list for UnitOfWork instances.
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork instance.</param>
        public void Add(UnitOfWork unitOfWork)
        {
            this.UnitOfWorks.Add(unitOfWork);
        }

        /// <summary>
        /// Commit all the changes in the defined UnitOfWork list 
        /// using the transaction scope feature.
        /// </summary>
        public void Commit()
        {
            foreach (var unitOfWork in this.UnitOfWorks)
            {
                try
                {
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}