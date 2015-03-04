using System;
using System.Globalization;
using System.Windows.Data;

namespace XPence.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> to provide the header text for a role.
    /// </summary>
    public class RoleHeaderConverter : IMultiValueConverter
    {
        /// <summary>
        /// returns a header text for transaction.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //Expected parameters are role id, name
            if (values.Length != 2)
                return string.Empty;

            long roleId;
            long roleName;

            if (!long.TryParse(System.Convert.ToString(values[0]), out roleId))
                return string.Empty;
            if (!long.TryParse(System.Convert.ToString(values[1]), out roleName))
                return string.Empty;
            if (roleId == 0)
                return "New role";
            return string.Format("{0}", roleName);
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
