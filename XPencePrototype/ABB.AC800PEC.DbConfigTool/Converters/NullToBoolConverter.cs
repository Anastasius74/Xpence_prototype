﻿using System.Globalization;
using System.Windows.Data;

namespace ABB.AC800PEC.DbConfigTool.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> that returns a bool value against an instance.
    /// </summary>
    public class NullToBoolConverter:IValueConverter
    {
        /// <summary>
        /// Converts an inatance's reference to a <see langword="bool"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns><see langword="true"/> if <see cref="value"/> is not null. Returns<see langword="false"/> otherwise.</returns>
        public object Convert(object value, System.Type targetType, object parameter,CultureInfo culture)
        {
            return null != value;
        }

        /// <summary>
        /// No implemeneted
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
