namespace XPence.DbAccess.Interfaces
{
    /// <summary>
    ///     Provide a Snapshot manager for the Undo/Redo/Revert feature
    ///     in the business layer.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        ///     Undo the last action, which is performed.
        /// </summary>
        void Undo();

        /// <summary>
        ///     Redo undoes the last Undo action.
        /// </summary>
        void Redo();

        /// <summary>
        ///     Undo the last saved changes from the database.
        /// </summary>
        void Revert();

        /// <summary>
        ///     Can execute the undo feature.
        /// </summary>
        /// <returns>True or false</returns>
        bool CanUndo();

        /// <summary>
        ///     Can execute the redo feature.
        /// </summary>
        /// <returns>True or false</returns>
        bool CanRedo();

        /// <summary>
        ///     Can execute the revert feature.
        /// </summary>
        /// <returns>True or false.</returns>
        bool CanRevert();

        /// <summary>
        ///     Take snapshot of each changes by storing entries.
        ///     Allows to backward and forward any changes with the DBContext.
        /// </summary>
        void TakeSnapshot();

        /// <summary>
        ///     Commit all changes
        /// </summary>
        /// <returns>
        ///     1 when the changes could be saved successfully or
        ///     0 when the changes could not be saved successfully
        /// </returns>
        int Commit();
    }
}