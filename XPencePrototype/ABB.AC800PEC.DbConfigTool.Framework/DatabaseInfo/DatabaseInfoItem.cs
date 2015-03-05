using System;
using System.Xml.Serialization;
using ABB.AC800PEC.DbConfigTool.Framework.XmlSerialization;

namespace ABB.AC800PEC.DbConfigTool.Framework.DatabaseInfo
{
    /// <summary>
    ///     Represents the database information.
    /// </summary>
    public class DatabaseInfoItem : IChildItem<DatabaseInfoManager>
    {
        public DatabaseInfoItem()
        {
            
        }
        public DatabaseInfoItem(string fileName, bool isDefault, string location, string creationBy, DateTime creationTime)
        {
            this.FileName = fileName;
            this.IsDefault = isDefault;
            this.Location = location;
            this.CreatedBy = creationBy;
            this.CreationTime = creationTime;
        }

        [XmlIgnore]
        public DatabaseInfoManager ParentObject { get; internal set; }
        /// <summary>
        ///     Gets or sets the database name.
        /// </summary>
        /// [XPathKey]
        public string FileName { get; set; }

        /// <summary>
        ///     Gets or sets the value, if the database is the default one.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        ///     Gets or sets the database location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///     Database creation time.
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///     User who created this DB
        /// </summary>
        public string CreatedBy { get; set; }

        DatabaseInfoManager IChildItem<DatabaseInfoManager>.Parent
        {
            get
            {
                return this.ParentObject;
            }
            set
            {
                this.ParentObject = value;
            }
        }
    }
}
