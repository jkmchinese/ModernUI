/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： WizardPageButtonVisibilityConverter.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-29 23:57
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;
using System.Windows.Data;

namespace ModernUI.ExtendedToolkit.Converters
{
    /// <summary>
    /// Wizard PageButton Visibility Converter
    /// </summary>
    public class WizardPageButtonVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (values == null || values.Length != 2)
                throw new ArgumentException("Wrong number of arguments for WizardPageButtonVisibilityConverter.");

            Visibility wizardVisibility = ((values[0] == null) || (values[0] == DependencyProperty.UnsetValue))
                ? Visibility.Hidden
                : (Visibility)values[0];

            WizardPageButtonVisibility wizardPageVisibility = ((values[1] == null) ||
                                                               (values[1] == DependencyProperty.UnsetValue))
                ? WizardPageButtonVisibility.Hidden
                : (WizardPageButtonVisibility)values[1];

            Visibility visibility = Visibility.Visible;

            switch (wizardPageVisibility)
            {
                case WizardPageButtonVisibility.Inherit:
                    visibility = wizardVisibility;
                    break;
                case WizardPageButtonVisibility.Collapsed:
                    visibility = Visibility.Collapsed;
                    break;
                case WizardPageButtonVisibility.Hidden:
                    visibility = Visibility.Hidden;
                    break;
                case WizardPageButtonVisibility.Visible:
                    visibility = Visibility.Visible;
                    break;
            }

            return visibility;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}