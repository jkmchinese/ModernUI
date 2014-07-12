/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： PointHelper.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-11 23:52
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;

namespace ModernUI.ExtendedToolkit.Utilities
{
    internal static class PointHelper
    {
        public static Point Empty
        {
            get { return new Point(double.NaN, double.NaN); }
        }

        public static double DistanceBetween(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static bool IsEmpty(Point point)
        {
            return DoubleHelper.IsNaN(point.X) && DoubleHelper.IsNaN(point.Y);
        }
    }
}