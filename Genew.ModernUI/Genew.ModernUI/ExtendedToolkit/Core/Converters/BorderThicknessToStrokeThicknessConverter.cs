/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： BorderThicknessToStrokeThicknessConverter.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-18 10:01
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;
using System.Windows.Data;

namespace ModernUI.ExtendedToolkit.Converters
{
    public class BorderThicknessToStrokeThicknessConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Thickness thickness = (Thickness) value;
            return (thickness.Bottom + thickness.Left + thickness.Right + thickness.Top)/4;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            int? thick = (int?) value;
            int thickValue = thick.HasValue ? thick.Value : 0;

            return new Thickness(thickValue, thickValue, thickValue, thickValue);
        }

        #endregion
    }
}