/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ByteUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:19
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace ModernUI.ExtendedToolkit
{
    public class ByteUpDown : CommonNumericUpDown<byte>
    {
        #region Constructors

        static ByteUpDown()
        {
            UpdateMetadata(typeof (ByteUpDown), 1, byte.MinValue, byte.MaxValue);
        }

        public ByteUpDown()
            : base(Byte.Parse, Decimal.ToByte, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override byte IncrementValue(byte value, byte increment)
        {
            return (byte) (value + increment);
        }

        protected override byte DecrementValue(byte value, byte increment)
        {
            return (byte) (value - increment);
        }

        #endregion //Base Class Overrides
    }
}