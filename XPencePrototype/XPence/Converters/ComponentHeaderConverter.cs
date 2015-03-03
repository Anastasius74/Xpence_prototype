using System;
using System.Globalization;
using System.Windows.Data;

namespace XPence.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> to provide the header text for a component.
    /// </summary>
    public class ComponentHeaderConverter : IMultiValueConverter
    {
        /// <summary>
        /// returns a header text for component.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //Expected parameters are component id, IsStorageOwner, Layer, Core
            if (values.Length != 4)
                return string.Empty;

            long componentId;
            long componentIsStorageOwner;
            long componentLayer;
            long componentCore;

            if (!long.TryParse(System.Convert.ToString(values[0]), out componentId))
                return string.Empty;
            if (!long.TryParse(System.Convert.ToString(values[1]), out componentIsStorageOwner))
                return string.Empty;
            if (!long.TryParse(System.Convert.ToString(values[2]), out componentLayer))
                return string.Empty;
            if (!long.TryParse(System.Convert.ToString(values[3]), out componentCore))
                return string.Empty;
            if (componentId == 0)
                return "New component";//Should get this from a resource file
            return string.Format("{0} - {1} - {2}", componentIsStorageOwner, componentLayer, componentCore);
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
