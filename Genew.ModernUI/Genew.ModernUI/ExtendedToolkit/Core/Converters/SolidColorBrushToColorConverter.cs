/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： SolidColorBrushToColorConverter.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-18 10:01
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows.Data;
using System.Windows.Media;

namespace ModernUI.ExtendedToolkit.Converters
{
    public class SolidColorBrushToColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush brush = value as SolidColorBrush;
            if (brush != null)
                return brush.Color;

            return default(Color);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Color color = (Color) value;
                return new SolidColorBrush(color);
            }

            return default(SolidColorBrush);
        }

        #endregion
    }
}