/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ColorSorter.cs
* 作   者： chenzhifen
* 创建日期： 2014-07-08 17:00
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Genew.ModernUI.ExtendedToolkit
{
    internal class ColorSorter : IComparer
    {
        public int Compare(object firstItem, object secondItem)
        {
            if (firstItem == null || secondItem == null)
                return -1;

            ColorItem colorItem1 = (ColorItem)firstItem;
            ColorItem colorItem2 = (ColorItem)secondItem;
            System.Drawing.Color drawingColor1 = System.Drawing.Color.FromArgb(colorItem1.Color.A, colorItem1.Color.R,
                colorItem1.Color.G, colorItem1.Color.B);
            System.Drawing.Color drawingColor2 = System.Drawing.Color.FromArgb(colorItem2.Color.A, colorItem2.Color.R,
                colorItem2.Color.G, colorItem2.Color.B);

            // Compare Hue
            double hueColor1 = Math.Round((double)drawingColor1.GetHue(), 3);
            double hueColor2 = Math.Round((double)drawingColor2.GetHue(), 3);

            if (hueColor1 > hueColor2)
                return 1;
            if (hueColor1 < hueColor2)
                return -1;
            // Hue is equal, compare Saturation
            double satColor1 = Math.Round((double)drawingColor1.GetSaturation(), 3);
            double satColor2 = Math.Round((double)drawingColor2.GetSaturation(), 3);

            if (satColor1 > satColor2)
                return 1;
            if (satColor1 < satColor2)
                return -1;
            // Saturation is equal, compare Brightness
            double brightColor1 = Math.Round((double)drawingColor1.GetBrightness(), 3);
            double brightColor2 = Math.Round((double)drawingColor2.GetBrightness(), 3);

            if (brightColor1 > brightColor2)
                return 1;
            if (brightColor1 < brightColor2)
                return -1;

            return 0;
        }
    }
}