using System.Collections.ObjectModel;
using System.IO;
using ABB.AC800PEC.DbConfigTool.Framework;
using ABB.AC800PEC.DbConfigTool.Framework.DatabaseInfo;
using ABB.AC800PEC.DbConfigTool.Services.Interfaces;
using AutoMapper;

namespace ABB.AC800PEC.DbConfigTool.Services.Implementation
{
    public class DatabaseInfoService : IDatabaseInfoService
    {
        public ObservableCollection<T> GetRecentItems<T>()
        {
            var items = new ObservableCollection<T>();
            
            if (File.Exists(ApplicationSettings.DatabaseInfoAbsolutePath))
            {
                var recentItems = DatabaseInfoManager.GetDatabaseInfoList();

                foreach (DatabaseInfoItem dbInfoItem in recentItems.Children)
                {
                    var vm = Mapper.Map<T>(dbInfoItem);
                    items.Add(vm);
                } 
            }

            return items;
        }
    }
}
