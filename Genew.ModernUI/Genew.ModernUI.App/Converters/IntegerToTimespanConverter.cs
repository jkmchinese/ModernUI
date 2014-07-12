/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： IntegerToTimespanConverter.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-30 15:16
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows.Data;

namespace ModernUI.App.Converters
{
    public class IntegerToTimespanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return TimeSpan.FromMilliseconds((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}