using System;
using System.IO;
using System.Linq;
using ABB.AC800PEC.DbConfigTool.Framework.XmlSerialization;
using ABB.AC800PEC.DbConfigTool.Shared;

namespace ABB.AC800PEC.DbConfigTool.Framework.DatabaseInfo
{
    /// <summary>
    ///     Represents a list of the database infos.
    /// </summary>
    public class DatabaseInfoManager
    {
        public DatabaseInfoManager()
        {
            this.Children = new ChildItemCollection<DatabaseInfoManager, DatabaseInfoItem>(this);
        }
        
        /// <summary>
        ///     Represents all database info items.
        /// </summary>
        public ChildItemCollection<DatabaseInfoManager, DatabaseInfoItem> Children { get; private set; }

        /// <summary>
        ///     Get all the database infos from the Xml document.
        /// </summary>
        /// <returns>Get database info</returns>
        public static DatabaseInfoManager GetDatabaseInfoList()
        {
            return DataSerializer.Deserialize<DatabaseInfoManager>(ApplicationSettings.DatabaseInfoAbsolutePath);
        }
        
        /// <summary>
        ///     Get the default database info from the xml document.
        /// </summary>
        /// <returns>Returns the default database from the xml document.</returns>
        public static DatabaseInfoItem GetDefaultDatabaseInfo()
        {
            return GetDatabaseInfoList().Children.FirstOrDefault(item => item.IsDefault);
        }

        /// <summary>
        ///     Get the not default database info from the xml document.
        /// </summary>
        /// <returns>Returns the not default database from the xml document.</returns>
        public static DatabaseInfoItem GetNotDefaultDatabaseInfo()
        {
            return GetDatabaseInfoList().Children.FirstOrDefault(item => !item.IsDefault);
        }

        /// <summary>
        ///     Get the current database info from the xml document.
        /// </summary>
        /// <returns>Returns database info.</returns>
        public static DatabaseInfoItem GetDatabaseInfoWithMaxDate()
        {
            // Retrieve maximum datetime.
            var maxDate = (from d in GetDatabaseInfoList().Children select d.CreationTime).Max();

            return GetDatabaseInfoList().Children.FirstOrDefault(x => x.CreationTime == maxDate);
        }
      
        /// <summary>
        /// Get the databaseInfoItem with given database filename and location.
        /// </summary>
        /// <param name="databaseFileFullPath">The full path of the physical database file</param>
        /// <returns>A single DatabseInfoItem for the given file name
        /// </returns>
        public static DatabaseInfoItem GetDatabaseInfoItem(string databaseFileFullPath)
        {
            return GetDatabaseInfoList().Children.FirstOrDefault(fi => fi.FileName == Path.GetFileName(databaseFileFullPath) && fi.Location == Path.GetDirectoryName(databaseFileFullPath));
        }

        /// <summary>
        /// Remove all Elements from the database base info xml file, which have IsDefault element equal false.
        /// </summary>
        public void RemoveNewDatabaseInfo()
        {
            if (GetDatabaseInfoList().Children.Count > 0)
            {
                foreach (var item in GetDatabaseInfoList().Children.ToList())
                {
                    if (!item.IsDefault)
                    {
                        DataSerializer.RemoveNewElement(ApplicationSettings.DatabaseInfoAbsolutePath, item, ApplicationConstants.DatabaseInfoXmlXPath);
                    }
                } 
            }
        }

        private void RemoveSameDefaultDatabaseInfo(string fileName, string location)
        {
            if (GetDatabaseInfoList().Children.Count > 0)
            {
                foreach (var item in GetDatabaseInfoList().Children.ToList())
                {
                    if (item.FileName == fileName && item.Location == location)
                    {
                        DataSerializer.RemoveNewElement(ApplicationSettings.DatabaseInfoAbsolutePath, item, ApplicationConstants.DatabaseInfoXmlXPath);
                    }
                }
            }
        }

        /// <summary>
        /// Check if the databaseInfoItem already exists in the DatabaseInfo file.
        /// </summary>
        /// <param name="item">Database info item.</param>
        /// <returns>If the databaseInfoItem exists in the xml file return true, else false.</returns>
        public static bool CheckDatabaseInfoItem(DatabaseInfoItem item)
        {
            return GetDatabaseInfoList().Children.Any(databaseInfoItem => databaseInfoItem.FileName == item.FileName && databaseInfoItem.Location == item.Location);
        }

        public static void Refresh()
        {
            if (File.Exists(ApplicationSettings.DatabaseInfoAbsolutePath) && GetDatabaseInfoList().Children.Count > 0)
            {
                int recentFilesMax = Convert.ToInt32(ConfigurationFileSettings.ReadAppSettingValue(ApplicationConstants.RecentFilesMax));
                IOrderedEnumerable<DatabaseInfoItem> lastItems = from l in GetDatabaseInfoList().Children orderby l.CreationTime descending select l;

                if (lastItems.Count() > recentFilesMax)
                {
                    var databaseInfoItems = lastItems.ToList().GetRange(0, recentFilesMax);

                    DataSerializer.RemoveAll(ApplicationSettings.DatabaseInfoAbsolutePath, ApplicationConstants.DatabaseInfoXmlXPath);
                    DataSerializer.AddElements(databaseInfoItems, ApplicationSettings.DatabaseInfoAbsolutePath);
                }
            }
        }

        /// <summary>
        ///     Add a new database info item in the list items.
        /// </summary>
        /// <param name="databaseInfoItem">Struture of the database info.</param>
        private void AddDatabaseInfo(DatabaseInfoItem databaseInfoItem)
        {
            Children.Add(databaseInfoItem);
        }

        /// <summary>
        ///     Create database info xml file.
        /// </summary>
        /// <param name="item">Database info item.</param>
        public void CreateDatabaseInfoFile(DatabaseInfoItem item)
        {
            try
            {
                var items = new DatabaseInfoManager();

                items.AddDatabaseInfo(item);
                DataSerializer.Serialize(items, ApplicationSettings.DatabaseInfoAbsolutePath);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Add default database info item to the xml file.
        /// </summary>
        /// <param name="item">database info item.</param>
        public void AddDefaultDatabaseInfoItem(DatabaseInfoItem item)
        {
            try
            {
                var items = new DatabaseInfoManager();

                items.AddDatabaseInfo(item);

                if (File.Exists(ApplicationSettings.DatabaseInfoAbsolutePath))
                {
                    RemoveNewDatabaseInfo();
                    RemoveSameDefaultDatabaseInfo(item.FileName, item.Location);
                    DataSerializer.AddNewElement(item, ApplicationSettings.DatabaseInfoAbsolutePath);
                }
                else
                {
                    DataSerializer.Serialize(items, ApplicationSettings.DatabaseInfoAbsolutePath);
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
