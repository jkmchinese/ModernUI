/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ColorBlendConverter.cs
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
    /// <summary>
    ///     This converter allow to blend two colors into one based on a specified ratio
    /// </summary>
    public class ColorBlendConverter : IValueConverter
    {
        private double _blendedColorRatio;

        /// <summary>
        ///     The ratio of the blended color. Must be between 0 and 1.
        /// </summary>
        public double BlendedColorRatio
        {
            get { return _blendedColorRatio; }

            set
            {
                if (value < 0d || value > 1d)
                    throw new ArgumentException(
                        "BlendedColorRatio must greater than or equal to 0 and lower than or equal to 1 ");

                _blendedColorRatio = value;
            }
        }

        /// <summary>
        ///     The color to blend with the source color
        /// </summary>
        public Color BlendedColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(Color))
                return null;

            Color color = (Color)value;
            return new Color
            {
                A = BlendValue(color.A, BlendedColor.A),
                R = BlendValue(color.R, BlendedColor.R),
                G = BlendValue(color.G, BlendedColor.G),
                B = BlendValue(color.B, BlendedColor.B)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private byte BlendValue(byte original, byte blend)
        {
            double blendRatio = BlendedColorRatio;
            double sourceRatio = 1 - blendRatio;

            double result = (original * sourceRatio) + (blend * blendRatio);
            result = Math.Round(result);
            result = Math.Min(255d, Math.Max(0d, result));
            return System.Convert.ToByte(result);
        }
    }
}