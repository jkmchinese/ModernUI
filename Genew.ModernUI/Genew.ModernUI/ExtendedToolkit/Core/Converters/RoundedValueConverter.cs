/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： RoundedValueConverter.cs
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
    public class RoundedValueConverter : IValueConverter
    {
        #region Precision Property

        private int _precision;

        public int Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }

        #endregion

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return Math.Round((double) value, _precision);
            }
            if (value is Point)
            {
                return new Point(Math.Round(((Point) value).X, _precision), Math.Round(((Point) value).Y, _precision));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}