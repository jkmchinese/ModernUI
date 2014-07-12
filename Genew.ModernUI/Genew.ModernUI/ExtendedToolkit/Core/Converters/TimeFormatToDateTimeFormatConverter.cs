/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： TimeFormatToDateTimeFormatConverter.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-18 10:01
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows.Data;

namespace ModernUI.ExtendedToolkit.Converters
{
    public class TimeFormatToDateTimeFormatConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TimeFormat timeFormat = (TimeFormat)value;
            switch (timeFormat)
            {
                case TimeFormat.Custom:
                    return DateTimeFormat.Custom;
                case TimeFormat.ShortTime:
                    return DateTimeFormat.ShortTime;
                case TimeFormat.LongTime:
                    return DateTimeFormat.LongTime;
                default:
                    return DateTimeFormat.ShortTime;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}