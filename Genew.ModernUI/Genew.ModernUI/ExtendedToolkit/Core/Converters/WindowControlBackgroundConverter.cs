/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： WindowControlBackgroundConverter.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-18 10:01
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ModernUI.ExtendedToolkit.Converters
{
    public class WindowControlBackgroundConverter : IMultiValueConverter
    {
        /// <summary>
        ///     Used in the WindowContainer Template to calculate the resulting background brush
        ///     from the WindowBackground (values[0]) and WindowOpacity (values[1]) propreties.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Brush backgroundColor = (Brush) values[0];
            double opacity = (double) values[1];

            if (backgroundColor != null)
            {
                // Do not override any possible opacity value specifically set by the user.
                // Only use WindowOpacity value if the user did not set an opacity first.
                if (backgroundColor.ReadLocalValue(Brush.OpacityProperty) ==
                    System.Windows.DependencyProperty.UnsetValue)
                {
                    backgroundColor = backgroundColor.Clone();
                    backgroundColor.Opacity = opacity;
                }
            }
            return backgroundColor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}