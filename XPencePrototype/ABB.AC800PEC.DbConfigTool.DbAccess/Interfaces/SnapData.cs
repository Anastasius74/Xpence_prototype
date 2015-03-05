using System.Data.Entity;

namespace ABB.AC800PEC.DbConfigTool.DbAccess.Interfaces
{
    class SnapData
    {
        public EntityState State;
        public object Value;
        public object OrginalValue;
        public object Entity;
    }
}
