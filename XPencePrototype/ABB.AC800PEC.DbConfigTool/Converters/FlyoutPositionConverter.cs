using System;
using System.Globalization;
using System.Windows.Data;
using ABB.AC800PEC.DbConfigTool.Infrastructure.BaseClasses;
using MahApps.Metro.Controls;

namespace ABB.AC800PEC.DbConfigTool.Converters
{
    /// <summary>
    /// A converter to convert a <see cref="VisibilityPosition"/> enum to <see cref="Position"/> enum.
    /// </summary>
    public class FlyoutPositionConverter : IValueConverter
    {
        /// <summary>
        /// Conmverts a View model recognized enum to view ewcogniuzed enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = (VisibilityPosition)Enum.Parse(typeof(VisibilityPosition), System.Convert.ToString(value));
            switch (position)
            {
                case VisibilityPosition.Bottom:
                    return Position.Bottom;
                case VisibilityPosition.Right:
                    return Position.Right;
                case VisibilityPosition.Left:
                    return Position.Right;
                case VisibilityPosition.Top:
                    return Position.Top;
            }
            return null;
        }

        /// <summary>
        /// Not implemented!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
