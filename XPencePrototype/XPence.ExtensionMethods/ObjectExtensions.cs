using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;

namespace System
{
    public static class ObjectExtensions
    {
        public static string AsStringResource(this string resourceName)
        {
            return (string)Application.Current.FindResource(resourceName);
        }

        public static long? GetDatabaseId(this object persistableObject)
        {
            var t = persistableObject.GetType();
            var pinfos = t.GetProperties();
            var keyPi = pinfos.FirstOrDefault(pi => pi.GetCustomAttributes(typeof (KeyAttribute), true).Any());
            if (keyPi != null)
            {
                return (long) keyPi.GetValue(persistableObject);
            }
            return null;
        }
    }
}
