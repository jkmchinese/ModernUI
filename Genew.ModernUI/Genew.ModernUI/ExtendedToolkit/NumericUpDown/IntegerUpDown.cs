/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： IntegerUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:19
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace ModernUI.ExtendedToolkit
{
    public class IntegerUpDown : CommonNumericUpDown<int>
    {
        #region Constructors

        static IntegerUpDown()
        {
            UpdateMetadata(typeof (IntegerUpDown), 1, int.MinValue, int.MaxValue);
        }

        public IntegerUpDown()
            : base(Int32.Parse, Decimal.ToInt32, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override int IncrementValue(int value, int increment)
        {
            return value + increment;
        }

        protected override int DecrementValue(int value, int increment)
        {
            return value - increment;
        }

        #endregion //Base Class Overrides
    }
}