using System;
using System.IO;
using XPence.Framework;
using XPence.Framework.DatabaseInfo;
using XPence.Framework.XmlSerialization;

namespace XPence.DbAccess.Implementations
{
    /// <summary>
    ///     Represents the copy operation the new sqlite database file from the base directory to the application folder.
    /// </summary>
    public static class DatabaseInstanceManager
    {
        /// <summary>
        ///   Represents the creation of a new database structure, which contains the following features:
        /// - Create the DbInfoItem.
        /// - Copy the default database to the temp folder.
        /// - Copy the default Database to the default template folder, 
        ///   when the default Database file does not exist in the default template folder.  
        /// </summary>
        public static void CreateNewDatabaseStructure()
        {
            try
            {
                var tempDatabaseName = ApplicationSettings.DatabaseTemporaryFileName;
                var newDatabaseTemporaryDirectory = ApplicationSettings.DatabaseTemporaryDirectory;
                var defaultDatabasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ApplicationSettings.DatabaseTemporaryRelativePath);
                if (newDatabaseTemporaryDirectory != null)
                {
                    if (!Directory.Exists(newDatabaseTemporaryDirectory))
                    {
                        Directory.CreateDirectory(newDatabaseTemporaryDirectory); 
                    }
                    
                    var databaseDestinationFile = Path.Combine(newDatabaseTemporaryDirectory, tempDatabaseName);
                    var manager = new DatabaseInfoManager();

                    File.Copy(defaultDatabasePath, databaseDestinationFile, true);
                    DatabaseInfoItem databaseInfoItem = new DatabaseInfoItem(tempDatabaseName, false, newDatabaseTemporaryDirectory, Environment.UserName, DateTime.Now);
                    if (File.Exists(ApplicationSettings.DatabaseInfoAbsolutePath))
                    {
                        manager.RemoveNewDatabaseInfo();
                        DataSerializer.AddNewElement(databaseInfoItem, ApplicationSettings.DatabaseInfoAbsolutePath);
                    }
                    else
                    {
                        manager.CreateDatabaseInfoFile(databaseInfoItem);
                    }
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}