using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ABB.AC800PEC.DbConfigTool.Services.Interfaces
{
    public interface IDatabaseInfoService
    {
        ObservableCollection<T> GetRecentItems<T>();
    }
}