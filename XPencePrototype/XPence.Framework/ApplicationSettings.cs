using System;
using System.IO;

namespace XPence.Framework
{
    /// <summary>
    ///     Convenience class for accessing application settings without Magic Strings.
    /// </summary>
    public static class ApplicationSettings
    {
        private const string CompanyName = "ABB";
        private const string ProjectName = "AC800PEC";
        private const string DbFolder = "Data";
        private const string DbInfoFolder = "DbInfo";
        private const string DbInfoName = "UserDatabaseInfo.xml";
        private const string DbDefaultName = "SimulinkDb.db";
        private const string DbFileExtension = ".db";
        private const string DbTemporaryFolder = "Temporary";
        private const string DbTemplateFolder = "Template";
        private const string DbTemporaryName = "NewFile";

        public static string DatabaseInfoStorageDirectory
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), CompanyName, ProjectName, DbFolder, DbInfoFolder);
            }
        }

        public static string DatabaseInfoAbsolutePath
        {
            get
            {
                return Path.Combine(DatabaseInfoStorageDirectory, DbInfoName);
            }
        }

        public static string DatabaseTemporaryDirectory
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), CompanyName, ProjectName, DbFolder, DbTemporaryFolder); 
            }
        }

        public static string DatabaseTemporaryRelativePath
        {
            get
            {
                return Path.Combine(DbFolder, DbDefaultName); 
            }
        }

        public static string DatabaseTemplateDirectory
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), CompanyName, ProjectName, DbFolder, DbTemplateFolder);
            }
        }

        public static string DatabaseDefaultName
        {
            get
            {
                return DbDefaultName;
            }
        }

        public static string DatabaseTemporaryFileName
        {
            get
            {
                return string.Format("{0}{1}", DbTemporaryName, DbFileExtension);
                
            }
        }

        public static string DatabaseExtension
        {
            get
            {
                return DbFileExtension;
            }
        }
    }
}
