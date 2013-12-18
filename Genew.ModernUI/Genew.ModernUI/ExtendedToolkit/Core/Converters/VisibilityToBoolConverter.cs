/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： VisibilityToBoolConverter.cs
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
    public class VisibilityToBoolConverter : IValueConverter
    {
        #region Inverted Property

        public bool Inverted { get; set; }

        #endregion

        #region Not Property

        public bool Not { get; set; }

        #endregion

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Inverted ? BoolToVisibility(value) : VisibilityToBool(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Inverted ? VisibilityToBool(value) : BoolToVisibility(value);
        }

        private object VisibilityToBool(object value)
        {
            if (!(value is Visibility))
                throw new InvalidOperationException(ErrorMessages.GetMessage("SuppliedValueWasNotVisibility"));

            return (((Visibility)value) == Visibility.Visible) ^ Not;
        }

        private object BoolToVisibility(object value)
        {
            if (!(value is bool))
                throw new InvalidOperationException(ErrorMessages.GetMessage("SuppliedValueWasNotBool"));

            return ((bool)value ^ Not) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}