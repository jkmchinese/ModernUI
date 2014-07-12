/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ULongUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:19
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace ModernUI.ExtendedToolkit
{
    internal class ULongUpDown : CommonNumericUpDown<ulong>
    {
        #region Constructors

        static ULongUpDown()
        {
            UpdateMetadataInternal(typeof(ULongUpDown), 1, ulong.MinValue, ulong.MaxValue);
        }

        public ULongUpDown()
            : base(ulong.Parse, Decimal.ToUInt64, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override ulong IncrementValue(ulong value, ulong increment)
        {
            return value + increment;
        }

        protected override ulong DecrementValue(ulong value, ulong increment)
        {
            return value - increment;
        }

        #endregion //Base Class Overrides
    }
}