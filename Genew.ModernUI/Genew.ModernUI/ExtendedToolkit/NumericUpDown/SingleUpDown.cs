/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： SingleUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:19
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;

namespace ModernUI.ExtendedToolkit
{
    public class SingleUpDown : CommonNumericUpDown<float>
    {
        #region Constructors

        static SingleUpDown()
        {
            UpdateMetadata(typeof (SingleUpDown), 1f, float.NegativeInfinity, float.PositiveInfinity);
        }

        public SingleUpDown()
            : base(Single.Parse, Decimal.ToSingle, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Properties

        #region AllowInputSpecialValues

        public static readonly DependencyProperty AllowInputSpecialValuesProperty =
            DependencyProperty.Register("AllowInputSpecialValues", typeof (AllowedSpecialValues), typeof (SingleUpDown),
                new UIPropertyMetadata(AllowedSpecialValues.None));

        public AllowedSpecialValues AllowInputSpecialValues
        {
            get { return (AllowedSpecialValues) GetValue(AllowInputSpecialValuesProperty); }
            set { SetValue(AllowInputSpecialValuesProperty, value); }
        }

        #endregion //AllowInputSpecialValues

        #endregion

        #region Base Class Overrides

        protected override float? OnCoerceIncrement(float? baseValue)
        {
            if (baseValue.HasValue && float.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Increment.");

            return base.OnCoerceIncrement(baseValue);
        }

        protected override float? OnCoerceMaximum(float? baseValue)
        {
            if (baseValue.HasValue && float.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Maximum.");

            return base.OnCoerceMaximum(baseValue);
        }

        protected override float? OnCoerceMinimum(float? baseValue)
        {
            if (baseValue.HasValue && float.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Minimum.");

            return base.OnCoerceMinimum(baseValue);
        }

        protected override float IncrementValue(float value, float increment)
        {
            return value + increment;
        }

        protected override float DecrementValue(float value, float increment)
        {
            return value - increment;
        }

        protected override void SetValidSpinDirection()
        {
            if (Value.HasValue && float.IsInfinity(Value.Value) && (Spinner != null))
            {
                Spinner.ValidSpinDirection = ValidSpinDirections.None;
            }
            else
            {
                base.SetValidSpinDirection();
            }
        }

        protected override float? ConvertTextToValue(string text)
        {
            float? result = base.ConvertTextToValue(text);

            if (result != null)
            {
                if (float.IsNaN(result.Value))
                    TestInputSpecialValue(AllowInputSpecialValues, AllowedSpecialValues.NaN);
                else if (float.IsPositiveInfinity(result.Value))
                    TestInputSpecialValue(AllowInputSpecialValues, AllowedSpecialValues.PositiveInfinity);
                else if (float.IsNegativeInfinity(result.Value))
                    TestInputSpecialValue(AllowInputSpecialValues, AllowedSpecialValues.NegativeInfinity);
            }

            return result;
        }

        #endregion
    }
}