/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： DoubleHelper.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-11 23:36
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Genew.ModernUI.ExtendedToolkit.Utilities
{
    internal static class DoubleHelper
    {
        public static bool AreVirtuallyEqual(double d1, double d2)
        {
            if (double.IsPositiveInfinity(d1))
                return double.IsPositiveInfinity(d2);

            if (double.IsNegativeInfinity(d1))
                return double.IsNegativeInfinity(d2);

            if (IsNaN(d1))
                return IsNaN(d2);

            double n = d1 - d2;
            double d = (Math.Abs(d1) + Math.Abs(d2) + 10) * 1.0e-15;
            return (-d < n) && (d > n);
        }

        public static bool AreVirtuallyEqual(Size s1, Size s2)
        {
            return (AreVirtuallyEqual(s1.Width, s2.Width)
                    && AreVirtuallyEqual(s1.Height, s2.Height));
        }

        public static bool AreVirtuallyEqual(Point p1, Point p2)
        {
            return (AreVirtuallyEqual(p1.X, p2.X)
                    && AreVirtuallyEqual(p1.Y, p2.Y));
        }

        public static bool AreVirtuallyEqual(Rect r1, Rect r2)
        {
            return (AreVirtuallyEqual(r1.TopLeft, r2.TopLeft)
                    && AreVirtuallyEqual(r1.BottomRight, r2.BottomRight));
        }

        public static bool AreVirtuallyEqual(Vector v1, Vector v2)
        {
            return (AreVirtuallyEqual(v1.X, v2.X)
                    && AreVirtuallyEqual(v1.Y, v2.Y));
        }

        public static bool AreVirtuallyEqual(Segment s1, Segment s2)
        {
            // note: Segment struct already uses "virtually equal" approach
            return (s1 == s2);
        }

        public static bool IsNaN(double value)
        {
            // used reflector to borrow the high performance IsNan function 
            // from the WPF MS.Internal namespace
            NanUnion t = new NanUnion();
            t.DoubleValue = value;

            UInt64 exp = t.UintValue & 0xfff0000000000000;
            UInt64 man = t.UintValue & 0x000fffffffffffff;

            return (exp == 0x7ff0000000000000 || exp == 0xfff0000000000000) && (man != 0);
        }

        #region NanUnion Nested Types

        [StructLayout(LayoutKind.Explicit)]
        private struct NanUnion
        {
            [FieldOffset(0)]
            internal double DoubleValue;
            [FieldOffset(0)]
            internal readonly UInt64 UintValue;
        }

        #endregion
    }
}