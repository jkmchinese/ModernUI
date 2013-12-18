/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： WindowContentBorderMarginConverter.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-18 10:01
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Genew.ModernUI.ExtendedToolkit.Converters
{
    /// <summary>
    ///     Sets the margin for the thumb grip, the top buttons, or for the content border in the WindowControl.
    /// </summary>
    public class WindowContentBorderMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double horizontalContentBorderOffset = (double) values[0];
            double verticalContentBorderOffset = (double) values[1];

            switch ((string) parameter)
            {
                    // Content Border Margin in the WindowControl
                case "0":
                    return new Thickness(horizontalContentBorderOffset
                        , 0d
                        , horizontalContentBorderOffset
                        , verticalContentBorderOffset);
                    // Thumb Grip Margin in the WindowControl
                case "1":
                    return new Thickness(0d
                        , 0d
                        , horizontalContentBorderOffset
                        , verticalContentBorderOffset);
                    // Header Buttons Margin in the WindowControl
                case "2":
                    return new Thickness(0d
                        , 0d
                        , horizontalContentBorderOffset
                        , 0d);
                default:
                    throw new NotSupportedException("'parameter' for WindowContentBorderMarginConverter is not valid.");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}