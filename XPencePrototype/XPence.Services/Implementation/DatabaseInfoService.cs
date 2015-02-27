using System.Collections.ObjectModel;
using System.IO;
using XPence.Framework;
using XPence.Framework.DatabaseInfo;
using XPence.Services.Interfaces;
using AutoMapper;

namespace XPence.Services.Implementation
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
