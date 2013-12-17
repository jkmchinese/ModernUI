/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ShortUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:19
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace Genew.ModernUI.ExtendedToolkit
{
    public class ShortUpDown : CommonNumericUpDown<short>
    {
        #region Constructors

        static ShortUpDown()
        {
            UpdateMetadata(typeof (ShortUpDown), 1, short.MinValue, short.MaxValue);
        }

        public ShortUpDown()
            : base(Int16.Parse, Decimal.ToInt16, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override short IncrementValue(short value, short increment)
        {
            return (short) (value + increment);
        }

        protected override short DecrementValue(short value, short increment)
        {
            return (short) (value - increment);
        }

        #endregion //Base Class Overrides
    }
}