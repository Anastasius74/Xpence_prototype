using System.Data.Entity;

namespace XPence.DbAccess.Interfaces
{
    class SnapData
    {
        public EntityState State;
        public object Value;
        public object OrginalValue;
        public object Entity;
    }
}
