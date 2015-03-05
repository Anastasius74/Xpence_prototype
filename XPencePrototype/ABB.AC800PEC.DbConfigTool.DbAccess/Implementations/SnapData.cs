using System.Data.Entity;

namespace ABB.AC800PEC.DbConfigTool.DbAccess.Implementations
{
    /// <summary>
    /// Represents the properties, which have to be kept in the Undo/Redo/Revert feature 
    /// by adding, updating and deleting of the Entities in the context.
    /// </summary>
    internal class SnapData
    {
        internal object Entity { get; set; }

        internal object OrginalValue { get; set; }

        internal EntityState State { get; set; }

        internal object Value { get; set; }
    }
}