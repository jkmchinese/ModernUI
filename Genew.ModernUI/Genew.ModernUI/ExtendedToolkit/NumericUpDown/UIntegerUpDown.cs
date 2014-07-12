/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： UIntegerUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:19
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace ModernUI.ExtendedToolkit
{
    internal class UIntegerUpDown : CommonNumericUpDown<uint>
    {
        #region Constructors

        static UIntegerUpDown()
        {
            UpdateMetadataInternal(typeof(UIntegerUpDown), 1, uint.MinValue, uint.MaxValue);
        }

        public UIntegerUpDown()
            : base(uint.Parse, Decimal.ToUInt32, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override uint IncrementValue(uint value, uint increment)
        {
            return value + increment;
        }

        protected override uint DecrementValue(uint value, uint increment)
        {
            return value - increment;
        }

        #endregion //Base Class Overrides
    }
}