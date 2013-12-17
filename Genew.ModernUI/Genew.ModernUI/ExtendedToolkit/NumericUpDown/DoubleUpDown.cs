/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： DoubleUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:19
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;

namespace Genew.ModernUI.ExtendedToolkit
{
    public class DoubleUpDown : CommonNumericUpDown<double>
    {
        #region Constructors

        static DoubleUpDown()
        {
            UpdateMetadata(typeof (DoubleUpDown), 1d, double.NegativeInfinity, double.PositiveInfinity);
        }

        public DoubleUpDown()
            : base(Double.Parse, Decimal.ToDouble, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Properties

        #region AllowInputSpecialValues

        public static readonly DependencyProperty AllowInputSpecialValuesProperty =
            DependencyProperty.Register("AllowInputSpecialValues", typeof (AllowedSpecialValues), typeof (DoubleUpDown),
                new UIPropertyMetadata(AllowedSpecialValues.None));

        public AllowedSpecialValues AllowInputSpecialValues
        {
            get { return (AllowedSpecialValues) GetValue(AllowInputSpecialValuesProperty); }
            set { SetValue(AllowInputSpecialValuesProperty, value); }
        }

        #endregion //AllowInputSpecialValues

        #endregion

        #region Base Class Overrides

        protected override double? OnCoerceIncrement(double? baseValue)
        {
            if (baseValue.HasValue && double.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Increment.");

            return base.OnCoerceIncrement(baseValue);
        }

        protected override double? OnCoerceMaximum(double? baseValue)
        {
            if (baseValue.HasValue && double.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Maximum.");

            return base.OnCoerceMaximum(baseValue);
        }

        protected override double? OnCoerceMinimum(double? baseValue)
        {
            if (baseValue.HasValue && double.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is for Minimum.");

            return base.OnCoerceMinimum(baseValue);
        }

        protected override double IncrementValue(double value, double increment)
        {
            return value + increment;
        }

        protected override double DecrementValue(double value, double increment)
        {
            return value - increment;
        }

        protected override void SetValidSpinDirection()
        {
            if (Value.HasValue && double.IsInfinity(Value.Value) && (Spinner != null))
            {
                Spinner.ValidSpinDirection = ValidSpinDirections.None;
            }
            else
            {
                base.SetValidSpinDirection();
            }
        }

        protected override double? ConvertTextToValue(string text)
        {
            double? result = base.ConvertTextToValue(text);
            if (result != null)
            {
                if (double.IsNaN(result.Value))
                    TestInputSpecialValue(AllowInputSpecialValues, AllowedSpecialValues.NaN);
                else if (double.IsPositiveInfinity(result.Value))
                    TestInputSpecialValue(AllowInputSpecialValues, AllowedSpecialValues.PositiveInfinity);
                else if (double.IsNegativeInfinity(result.Value))
                    TestInputSpecialValue(AllowInputSpecialValues, AllowedSpecialValues.NegativeInfinity);
            }

            return result;
        }

        #endregion
    }
}