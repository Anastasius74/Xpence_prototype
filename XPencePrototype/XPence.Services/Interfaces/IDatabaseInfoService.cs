using System.Collections.ObjectModel;
using System.Windows.Input;

namespace XPence.Services.Interfaces
{
    public interface IDatabaseInfoService
    {
        ObservableCollection<T> GetRecentItems<T>();
    }
}