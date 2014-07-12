/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： SByteUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:19
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace ModernUI.ExtendedToolkit
{
    internal class SByteUpDown : CommonNumericUpDown<sbyte>
    {
        #region Constructors

        static SByteUpDown()
        {
            UpdateMetadataInternal(typeof (SByteUpDown), 1, sbyte.MinValue, sbyte.MaxValue);
        }

        public SByteUpDown()
            : base(sbyte.Parse, Decimal.ToSByte, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override sbyte IncrementValue(sbyte value, sbyte increment)
        {
            return (sbyte) (value + increment);
        }

        protected override sbyte DecrementValue(sbyte value, sbyte increment)
        {
            return (sbyte) (value - increment);
        }

        #endregion //Base Class Overrides
    }
}