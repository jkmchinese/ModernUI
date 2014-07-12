/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： DecimalUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:19
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace ModernUI.ExtendedToolkit
{
    public class DecimalUpDown : CommonNumericUpDown<decimal>
    {
        #region Constructors

        static DecimalUpDown()
        {
            UpdateMetadata(typeof (DecimalUpDown), 1m, decimal.MinValue, decimal.MaxValue);
        }

        public DecimalUpDown()
            : base(Decimal.Parse, d => d, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override decimal IncrementValue(decimal value, decimal increment)
        {
            return value + increment;
        }

        protected override decimal DecrementValue(decimal value, decimal increment)
        {
            return value - increment;
        }

        #endregion //Base Class Overrides
    }
}