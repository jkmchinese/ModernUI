/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ValueRangeTextBox.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-25 17:53
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ModernUI.ExtendedToolkit.Primitives
{
    public class ValueRangeTextBox : AutoSelectTextBox
    {
        private BitVector32 m_flags;
        private CachedTextInfo m_imePreCompositionCachedTextInfo;

        static ValueRangeTextBox()
        {
            TextBox.TextProperty.OverrideMetadata(typeof(ValueRangeTextBox),
                new FrameworkPropertyMetadata(
                    null,
                    ValueRangeTextBox.TextCoerceValueCallback));

            TextBoxBase.AcceptsReturnProperty.OverrideMetadata(typeof(ValueRangeTextBox),
                new FrameworkPropertyMetadata(
                    false, null, ValueRangeTextBox.AcceptsReturnCoerceValueCallback));

            TextBoxBase.AcceptsTabProperty.OverrideMetadata(typeof(ValueRangeTextBox),
                new FrameworkPropertyMetadata(
                    false, null, ValueRangeTextBox.AcceptsTabCoerceValueCallback));

            AutomationProperties.AutomationIdProperty.OverrideMetadata(typeof(ValueRangeTextBox),
                new UIPropertyMetadata("ValueRangeTextBox"));
        }

        #region AcceptsReturn Property

        private static object AcceptsReturnCoerceValueCallback(DependencyObject sender, object value)
        {
            bool acceptsReturn = (bool)value;

            if (acceptsReturn)
                throw new NotSupportedException("The ValueRangeTextBox does not support the AcceptsReturn property.");

            return false;
        }

        #endregion AcceptsReturn Property

        #region AcceptsTab Property

        private static object AcceptsTabCoerceValueCallback(DependencyObject sender, object value)
        {
            bool acceptsTab = (bool)value;

            if (acceptsTab)
                throw new NotSupportedException("The ValueRangeTextBox does not support the AcceptsTab property.");

            return false;
        }

        #endregion AcceptsTab Property

        #region BeepOnError Property

        public static readonly DependencyProperty BeepOnErrorProperty =
            DependencyProperty.Register("BeepOnError", typeof(bool), typeof(ValueRangeTextBox),
                new UIPropertyMetadata(false));

        public bool BeepOnError
        {
            get { return (bool)GetValue(BeepOnErrorProperty); }
            set { SetValue(BeepOnErrorProperty, value); }
        }

        #endregion BeepOnError Property

        #region FormatProvider Property

        public static readonly DependencyProperty FormatProviderProperty =
            DependencyProperty.Register("FormatProvider", typeof(IFormatProvider), typeof(ValueRangeTextBox),
                new UIPropertyMetadata(null,
                    ValueRangeTextBox.FormatProviderPropertyChangedCallback));

        public IFormatProvider FormatProvider
        {
            get { return (IFormatProvider)GetValue(FormatProviderProperty); }
            set { SetValue(FormatProviderProperty, value); }
        }

        private static void FormatProviderPropertyChangedCallback(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            ValueRangeTextBox valueRangeTextBox = (ValueRangeTextBox)sender;

            if (!valueRangeTextBox.IsInitialized)
                return;

            valueRangeTextBox.OnFormatProviderChanged();
        }

        internal virtual void OnFormatProviderChanged()
        {
            RefreshConversionHelpers();
            RefreshCurrentText(false);
            RefreshValue();
        }

        #endregion FormatProvider Property

        #region MinValue Property

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(object), typeof(ValueRangeTextBox),
                new UIPropertyMetadata(
                    null,
                    null,
                    ValueRangeTextBox.MinValueCoerceValueCallback));

        public object MinValue
        {
            get { return GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        private static object MinValueCoerceValueCallback(DependencyObject sender, object value)
        {
            ValueRangeTextBox valueRangeTextBox = sender as ValueRangeTextBox;

            if (!valueRangeTextBox.IsInitialized)
                return DependencyProperty.UnsetValue;

            if (value == null)
                return value;

            Type type = valueRangeTextBox.ValueDataType;

            if (type == null)
                throw new InvalidOperationException(
                    "An attempt was made to set a minimum value when the ValueDataType property is null.");

            if (valueRangeTextBox.IsFinalizingInitialization)
                value = ValueRangeTextBox.ConvertValueToDataType(value, valueRangeTextBox.ValueDataType);

            if (value.GetType() != type)
                throw new ArgumentException("The value is not of type " + type.Name + ".", "MinValue");

            IComparable comparable = value as IComparable;

            if (comparable == null)
                throw new InvalidOperationException("MinValue does not implement the IComparable interface.");

            // ValidateValueInRange will throw if it must.
            object maxValue = valueRangeTextBox.MaxValue;

            valueRangeTextBox.ValidateValueInRange(value, maxValue, valueRangeTextBox.Value);

            return value;
        }

        #endregion MinValue Property

        #region MaxValue Property

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(object), typeof(ValueRangeTextBox),
                new UIPropertyMetadata(
                    null,
                    null,
                    MaxValueCoerceValueCallback));

        public object MaxValue
        {
            get { return GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        private static object MaxValueCoerceValueCallback(DependencyObject sender, object value)
        {
            ValueRangeTextBox valueRangeTextBox = sender as ValueRangeTextBox;

            if (!valueRangeTextBox.IsInitialized)
                return DependencyProperty.UnsetValue;

            if (value == null)
                return value;

            Type type = valueRangeTextBox.ValueDataType;

            if (type == null)
                throw new InvalidOperationException(
                    "An attempt was made to set a maximum value when the ValueDataType property is null.");

            if (valueRangeTextBox.IsFinalizingInitialization)
                value = ValueRangeTextBox.ConvertValueToDataType(value, valueRangeTextBox.ValueDataType);

            if (value.GetType() != type)
                throw new ArgumentException("The value is not of type " + type.Name + ".", "MinValue");

            IComparable comparable = value as IComparable;

            if (comparable == null)
                throw new InvalidOperationException("MaxValue does not implement the IComparable interface.");

            object minValue = valueRangeTextBox.MinValue;

            // ValidateValueInRange will throw if it must.
            valueRangeTextBox.ValidateValueInRange(minValue, value, valueRangeTextBox.Value);

            return value;
        }

        #endregion MaxValue Property

        #region NullValue Property

        public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register("NullValue", typeof(object), typeof(ValueRangeTextBox),
                new UIPropertyMetadata(
                    null,
                    ValueRangeTextBox.NullValuePropertyChangedCallback,
                    NullValueCoerceValueCallback));

        public object NullValue
        {
            get { return GetValue(NullValueProperty); }
            set { SetValue(NullValueProperty, value); }
        }

        private static object NullValueCoerceValueCallback(DependencyObject sender, object value)
        {
            ValueRangeTextBox valueRangeTextBox = sender as ValueRangeTextBox;

            if (!valueRangeTextBox.IsInitialized)
                return DependencyProperty.UnsetValue;

            if ((value == null) || (value == DBNull.Value))
                return value;

            Type type = valueRangeTextBox.ValueDataType;

            if (type == null)
                throw new InvalidOperationException(
                    "An attempt was made to set a null value when the ValueDataType property is null.");

            if (valueRangeTextBox.IsFinalizingInitialization)
                value = ValueRangeTextBox.ConvertValueToDataType(value, valueRangeTextBox.ValueDataType);

            if (value.GetType() != type)
                throw new ArgumentException("The value is not of type " + type.Name + ".", "NullValue");

            return value;
        }

        private static void NullValuePropertyChangedCallback(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            ValueRangeTextBox valueRangeTextBox = sender as ValueRangeTextBox;

            if (e.OldValue == null)
            {
                if (valueRangeTextBox.Value == null)
                    valueRangeTextBox.RefreshValue();
            }
            else
            {
                if (e.OldValue.Equals(valueRangeTextBox.Value))
                    valueRangeTextBox.RefreshValue();
            }
        }

        #endregion NullValue Property

        #region Value Property

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(ValueRangeTextBox),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    ValueRangeTextBox.ValuePropertyChangedCallback,
                    ValueRangeTextBox.ValueCoerceValueCallback));

        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static object ValueCoerceValueCallback(object sender, object value)
        {
            ValueRangeTextBox valueRangeTextBox = sender as ValueRangeTextBox;

            if (!valueRangeTextBox.IsInitialized)
                return DependencyProperty.UnsetValue;

            if (valueRangeTextBox.IsFinalizingInitialization)
                value = ValueRangeTextBox.ConvertValueToDataType(value, valueRangeTextBox.ValueDataType);

            if (!valueRangeTextBox.IsForcingValue)
                valueRangeTextBox.ValidateValue(value);

            return value;
        }

        private static void ValuePropertyChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            ValueRangeTextBox valueRangeTextBox = sender as ValueRangeTextBox;

            if (valueRangeTextBox.IsForcingValue)
                return;

            // The ValueChangedCallback can be raised even though both values are the same since the property
            // datatype is Object.
            if (object.Equals(e.NewValue, e.OldValue))
                return;

            valueRangeTextBox.IsInValueChanged = true;
            try
            {
                valueRangeTextBox.Text = valueRangeTextBox.GetTextFromValue(e.NewValue);
            }
            finally
            {
                valueRangeTextBox.IsInValueChanged = false;
            }
        }

        #endregion Value Property

        #region ValueDataType Property

        public static readonly DependencyProperty ValueDataTypeProperty =
            DependencyProperty.Register("ValueDataType", typeof(Type), typeof(ValueRangeTextBox),
                new UIPropertyMetadata(
                    null,
                    ValueRangeTextBox.ValueDataTypePropertyChangedCallback,
                    ValueRangeTextBox.ValueDataTypeCoerceValueCallback));

        public Type ValueDataType
        {
            get { return (Type)GetValue(ValueDataTypeProperty); }
            set { SetValue(ValueDataTypeProperty, value); }
        }

        private static object ValueDataTypeCoerceValueCallback(DependencyObject sender, object value)
        {
            ValueRangeTextBox valueRangeTextBox = sender as ValueRangeTextBox;

            if (!valueRangeTextBox.IsInitialized)
                return DependencyProperty.UnsetValue;

            Type valueDataType = value as Type;

            try
            {
                valueRangeTextBox.ValidateDataType(valueDataType);
            }
            catch (Exception exception)
            {
                throw new ArgumentException("An error occured while trying to change the ValueDataType.", exception);
            }

            return value;
        }

        private static void ValueDataTypePropertyChangedCallback(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            ValueRangeTextBox valueRangeTextBox = sender as ValueRangeTextBox;

            Type valueDataType = e.NewValue as Type;

            valueRangeTextBox.IsNumericValueDataType = ValueRangeTextBox.IsNumericType(valueDataType);

            valueRangeTextBox.RefreshConversionHelpers();

            valueRangeTextBox.ConvertValuesToDataType(valueDataType);
        }

        internal virtual void ValidateDataType(Type type)
        {
            // Null will always be valid and will reset the MinValue, MaxValue, NullValue and Value to null.
            if (type == null)
                return;

            // We use InvariantCulture instead of the active format provider since the FormatProvider is only
            // used when the source type is String.  When we are converting from a string, we are
            // actually converting a value from XAML.  Therefore, if the string will have a period as a
            // decimal separator.  If we were using the active format provider, we could end up expecting a coma
            // as the decimal separator and the ChangeType method would throw.

            object minValue = MinValue;

            if ((minValue != null) && (minValue.GetType() != type))
                minValue = System.Convert.ChangeType(minValue, type, CultureInfo.InvariantCulture);

            object maxValue = MaxValue;

            if ((maxValue != null) && (maxValue.GetType() != type))
                maxValue = System.Convert.ChangeType(maxValue, type, CultureInfo.InvariantCulture);

            object nullValue = NullValue;

            if (((nullValue != null) && (nullValue != DBNull.Value))
                && (nullValue.GetType() != type))
            {
                nullValue = System.Convert.ChangeType(nullValue, type, CultureInfo.InvariantCulture);
            }

            object value = Value;

            if (((value != null) && (value != DBNull.Value))
                && (value.GetType() != type))
            {
                value = System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
            }

            if ((minValue != null) || (maxValue != null)
                || ((nullValue != null) && (nullValue != DBNull.Value)))
            {
                // Value comparaisons will occur.  Therefore, the aspiring data type must implement IComparable.

                Type iComparable = type.GetInterface("IComparable");

                if (iComparable == null)
                    throw new InvalidOperationException(
                        "MinValue, MaxValue, and NullValue must implement the IComparable interface.");
            }
        }

        private void ConvertValuesToDataType(Type type)
        {
            if (type == null)
            {
                MinValue = null;
                MaxValue = null;
                NullValue = null;

                Value = null;

                return;
            }

            object minValue = MinValue;

            if ((minValue != null) && (minValue.GetType() != type))
                MinValue = ValueRangeTextBox.ConvertValueToDataType(minValue, type);

            object maxValue = MaxValue;

            if ((maxValue != null) && (maxValue.GetType() != type))
                MaxValue = ValueRangeTextBox.ConvertValueToDataType(maxValue, type);

            object nullValue = NullValue;

            if (((nullValue != null) && (nullValue != DBNull.Value))
                && (nullValue.GetType() != type))
            {
                NullValue = ValueRangeTextBox.ConvertValueToDataType(nullValue, type);
            }

            object value = Value;

            if (((value != null) && (value != DBNull.Value))
                && (value.GetType() != type))
            {
                Value = ValueRangeTextBox.ConvertValueToDataType(value, type);
            }
        }

        #endregion ValueDataType Property

        #region Text Property

        private static object TextCoerceValueCallback(object sender, object value)
        {
            ValueRangeTextBox valueRangeTextBox = sender as ValueRangeTextBox;

            if (!valueRangeTextBox.IsInitialized)
                return DependencyProperty.UnsetValue;

            if (value == null)
                return string.Empty;

            return value;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            // If in IME Composition, RefreshValue already returns without doing anything.
            RefreshValue();

            base.OnTextChanged(e);
        }

        #endregion Text Property

        #region HasValidationError Property

        private static readonly DependencyPropertyKey HasValidationErrorPropertyKey =
            DependencyProperty.RegisterReadOnly("HasValidationError", typeof(bool), typeof(ValueRangeTextBox),
                new UIPropertyMetadata(false));

        public static readonly DependencyProperty HasValidationErrorProperty =
            ValueRangeTextBox.HasValidationErrorPropertyKey.DependencyProperty;

        public bool HasValidationError
        {
            get { return (bool)GetValue(ValueRangeTextBox.HasValidationErrorProperty); }
        }

        private void SetHasValidationError(bool value)
        {
            SetValue(ValueRangeTextBox.HasValidationErrorPropertyKey, value);
        }

        #endregion HasValidationError Property

        #region HasParsingError Property

        private static readonly DependencyPropertyKey HasParsingErrorPropertyKey =
            DependencyProperty.RegisterReadOnly("HasParsingError", typeof(bool), typeof(ValueRangeTextBox),
                new UIPropertyMetadata(false));

        public static readonly DependencyProperty HasParsingErrorProperty =
            ValueRangeTextBox.HasParsingErrorPropertyKey.DependencyProperty;

        public bool HasParsingError
        {
            get { return (bool)GetValue(ValueRangeTextBox.HasParsingErrorProperty); }
        }

        internal void SetHasParsingError(bool value)
        {
            SetValue(ValueRangeTextBox.HasParsingErrorPropertyKey, value);
        }

        #endregion HasParsingError Property

        #region IsValueOutOfRange Property

        private static readonly DependencyPropertyKey IsValueOutOfRangePropertyKey =
            DependencyProperty.RegisterReadOnly("IsValueOutOfRange", typeof(bool), typeof(ValueRangeTextBox),
                new UIPropertyMetadata(false));

        public static readonly DependencyProperty IsValueOutOfRangeProperty =
            ValueRangeTextBox.IsValueOutOfRangePropertyKey.DependencyProperty;

        public bool IsValueOutOfRange
        {
            get { return (bool)GetValue(ValueRangeTextBox.IsValueOutOfRangeProperty); }
        }

        private void SetIsValueOutOfRange(bool value)
        {
            SetValue(ValueRangeTextBox.IsValueOutOfRangePropertyKey, value);
        }

        #endregion IsValueOutOfRange Property

        #region IsInValueChanged property

        internal bool IsInValueChanged
        {
            get { return m_flags[(int)ValueRangeTextBoxFlags.IsInValueChanged]; }
            private set { m_flags[(int)ValueRangeTextBoxFlags.IsInValueChanged] = value; }
        }

        #endregion

        #region IsForcingValue property

        internal bool IsForcingValue
        {
            get { return m_flags[(int)ValueRangeTextBoxFlags.IsForcingValue]; }
            private set { m_flags[(int)ValueRangeTextBoxFlags.IsForcingValue] = value; }
        }

        #endregion

        #region IsForcingText property

        internal bool IsForcingText
        {
            get { return m_flags[(int)ValueRangeTextBoxFlags.IsForcingText]; }
            private set { m_flags[(int)ValueRangeTextBoxFlags.IsForcingText] = value; }
        }

        #endregion

        #region IsNumericValueDataType property

        internal bool IsNumericValueDataType
        {
            get { return m_flags[(int)ValueRangeTextBoxFlags.IsNumericValueDataType]; }
            private set { m_flags[(int)ValueRangeTextBoxFlags.IsNumericValueDataType] = value; }
        }

        #endregion

        #region IsTextReadyToBeParsed property

        internal virtual bool IsTextReadyToBeParsed
        {
            get { return true; }
        }

        #endregion

        #region IsInIMEComposition property

        internal bool IsInIMEComposition
        {
            get { return m_imePreCompositionCachedTextInfo != null; }
        }

        #endregion

        #region IsFinalizingInitialization Property

        private bool IsFinalizingInitialization
        {
            get { return m_flags[(int)ValueRangeTextBoxFlags.IsFinalizingInitialization]; }
            set { m_flags[(int)ValueRangeTextBoxFlags.IsFinalizingInitialization] = value; }
        }

        #endregion

        #region TEXT FROM VALUE

        public event EventHandler<QueryTextFromValueEventArgs> QueryTextFromValue;

        internal string GetTextFromValue(object value)
        {
            string text = QueryTextFromValueCore(value);

            QueryTextFromValueEventArgs e = new QueryTextFromValueEventArgs(value, text);

            OnQueryTextFromValue(e);

            return e.Text;
        }

        protected virtual string QueryTextFromValueCore(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return string.Empty;

            IFormatProvider formatProvider = GetActiveFormatProvider();

            CultureInfo cultureInfo = formatProvider as CultureInfo;

            if (cultureInfo != null)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(value.GetType());

                if (converter.CanConvertTo(typeof(string)))
                    return (string)converter.ConvertTo(null, cultureInfo, value, typeof(string));
            }

            try
            {
                string result = System.Convert.ToString(value, formatProvider);

                return result;
            }
            catch
            {
            }

            return value.ToString();
        }

        private void OnQueryTextFromValue(QueryTextFromValueEventArgs e)
        {
            if (QueryTextFromValue != null)
                QueryTextFromValue(this, e);
        }

        #endregion TEXT FROM VALUE

        #region VALUE FROM TEXT

        public event EventHandler<QueryValueFromTextEventArgs> QueryValueFromText;

        internal object GetValueFromText(string text, out bool hasParsingError)
        {
            object value = null;
            bool success = QueryValueFromTextCore(text, out value);

            QueryValueFromTextEventArgs e = new QueryValueFromTextEventArgs(text, value);
            e.HasParsingError = !success;

            OnQueryValueFromText(e);

            hasParsingError = e.HasParsingError;

            return e.Value;
        }

        protected virtual bool QueryValueFromTextCore(string text, out object value)
        {
            value = null;

            Type validatingType = ValueDataType;

            text = text.Trim();

            if (validatingType == null)
                return true;

            if (!validatingType.IsValueType && (validatingType != typeof(string)))
                return false;

            try
            {
                value = System.Convert.ChangeType(text, validatingType, GetActiveFormatProvider());
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void OnQueryValueFromText(QueryValueFromTextEventArgs e)
        {
            if (QueryValueFromText != null)
                QueryValueFromText(this, e);
        }

        #endregion VALUE FROM TEXT

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if ((e.ImeProcessedKey != Key.None) && (!IsInIMEComposition))
            {
                // Start of an IME Composition.  Cache all the critical infos.
                StartIMEComposition();
            }

            base.OnPreviewKeyDown(e);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            RefreshCurrentText(true);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            RefreshCurrentText(true);
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            if (IsInIMEComposition)
                EndIMEComposition();

            base.OnTextInput(e);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2208:InstantiateArgumentExceptionsCorrectly")]
        protected virtual void ValidateValue(object value)
        {
            if (value == null)
                return;

            Type validatingType = ValueDataType;

            if (validatingType == null)
                throw new InvalidOperationException(
                    "An attempt was made to set a value when the ValueDataType property is null.");

            if ((value != DBNull.Value) && (value.GetType() != validatingType))
                throw new ArgumentException("The value is not of type " + validatingType.Name + ".", "Value");

            ValidateValueInRange(MinValue, MaxValue, value);
        }

        internal static bool IsNumericType(Type type)
        {
            if (type == null)
                return false;

            if (type.IsValueType)
            {
                if ((type == typeof(int)) || (type == typeof(double)) || (type == typeof(decimal))
                    || (type == typeof(float)) || (type == typeof(short)) || (type == typeof(long))
                    || (type == typeof(ushort)) || (type == typeof(uint)) || (type == typeof(ulong))
                    || (type == typeof(byte))
                    )
                {
                    return true;
                }
            }

            return false;
        }

        internal void StartIMEComposition()
        {
            Debug.Assert(m_imePreCompositionCachedTextInfo == null,
                "EndIMEComposition should have been called before another IME Composition starts.");

            m_imePreCompositionCachedTextInfo = new CachedTextInfo(this);
        }

        internal void EndIMEComposition()
        {
            CachedTextInfo cachedTextInfo = m_imePreCompositionCachedTextInfo.Clone() as CachedTextInfo;
            m_imePreCompositionCachedTextInfo = null;

            OnIMECompositionEnded(cachedTextInfo);
        }

        internal virtual void OnIMECompositionEnded(CachedTextInfo cachedTextInfo)
        {
        }

        internal virtual void RefreshConversionHelpers()
        {
        }

        internal IFormatProvider GetActiveFormatProvider()
        {
            IFormatProvider formatProvider = FormatProvider;

            if (formatProvider != null)
                return formatProvider;

            return CultureInfo.CurrentCulture;
        }

        internal CultureInfo GetCultureInfo()
        {
            CultureInfo cultureInfo = GetActiveFormatProvider() as CultureInfo;

            if (cultureInfo != null)
                return cultureInfo;

            return CultureInfo.CurrentCulture;
        }

        internal virtual string GetCurrentText()
        {
            return Text;
        }

        internal virtual string GetParsableText()
        {
            return Text;
        }

        internal void ForceText(string text, bool preserveCaret)
        {
            IsForcingText = true;
            try
            {
                int oldCaretIndex = CaretIndex;

                Text = text;

                if ((preserveCaret) && (IsLoaded))
                {
                    try
                    {
                        SelectionStart = oldCaretIndex;
                    }
                    catch (NullReferenceException)
                    {
                    }
                }
            }
            finally
            {
                IsForcingText = false;
            }
        }

        internal bool IsValueNull(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return true;

            Type type = ValueDataType;

            if (value.GetType() != type)
                value = System.Convert.ChangeType(value, type);

            object nullValue = NullValue;

            if (nullValue == null)
                return false;

            if (nullValue.GetType() != type)
                nullValue = System.Convert.ChangeType(nullValue, type);

            return nullValue.Equals(value);
        }

        internal void ForceValue(object value)
        {
            IsForcingValue = true;
            try
            {
                Value = value;
            }
            finally
            {
                IsForcingValue = false;
            }
        }

        internal void RefreshCurrentText(bool preserveCurrentCaretPosition)
        {
            string displayText = GetCurrentText();

            if (!string.Equals(displayText, Text))
                ForceText(displayText, preserveCurrentCaretPosition);
        }

        internal void RefreshValue()
        {
            if ((IsForcingValue) || (ValueDataType == null) || (IsInIMEComposition))
                return;

            object value;
            bool hasParsingError;

            if (IsTextReadyToBeParsed)
            {
                string parsableText = GetParsableText();

                value = GetValueFromText(parsableText, out hasParsingError);

                if (IsValueNull(value))
                    value = NullValue;
            }
            else
            {
                // We don't consider empty text as a parsing error.
                hasParsingError = !GetIsEditTextEmpty();
                value = NullValue;
            }

            SetHasParsingError(hasParsingError);

            bool hasValidationError = hasParsingError;
            try
            {
                ValidateValue(value);

                SetIsValueOutOfRange(false);
            }
            catch (Exception exception)
            {
                hasValidationError = true;

                if (exception is ArgumentOutOfRangeException)
                    SetIsValueOutOfRange(true);

                value = NullValue;
            }

            if (!object.Equals(value, Value))
                ForceValue(value);

            SetHasValidationError(hasValidationError);
        }

        internal virtual bool GetIsEditTextEmpty()
        {
            return Text == string.Empty;
        }

        private static object ConvertValueToDataType(object value, Type type)
        {
            // We use InvariantCulture instead of the active format provider since the FormatProvider is only
            // used when the source type is String.  When we are converting from a string, we are
            // actually converting a value from XAML.  Therefore, if the string will have a period as a
            // decimal separator.  If we were using the active format provider, we could end up expecting a coma
            // as the decimal separator and the ChangeType method would throw.
            if (type == null)
                return null;

            if (((value != null) && (value != DBNull.Value))
                && (value.GetType() != type))
            {
                return System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
            }

            return value;
        }

        private void CanEnterLineBreak(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;
        }

        private void CanEnterParagraphBreak(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;
        }

        private void ValidateValueInRange(object minValue, object maxValue, object value)
        {
            if (IsValueNull(value))
                return;

            Type type = ValueDataType;

            if (value.GetType() != type)
                value = System.Convert.ChangeType(value, type);

            // Validate the value against the range.
            if (minValue != null)
            {
                IComparable minValueComparable = (IComparable)minValue;

                if ((maxValue != null) && (minValueComparable.CompareTo(maxValue) > 0))
                    throw new ArgumentOutOfRangeException("minValue", "MaxValue must be greater than MinValue.");

                if (minValueComparable.CompareTo(value) > 0)
                    throw new ArgumentOutOfRangeException("minValue", "Value must be greater than MinValue.");
            }

            if (maxValue != null)
            {
                IComparable maxValueComparable = (IComparable)maxValue;

                if (maxValueComparable.CompareTo(value) < 0)
                    throw new ArgumentOutOfRangeException("maxValue", "Value must be less than MaxValue.");
            }
        }

        #region ISupportInitialize

        protected override void OnInitialized(EventArgs e)
        {
            IsFinalizingInitialization = true;
            try
            {
                CoerceValue(ValueRangeTextBox.ValueDataTypeProperty);

                IsNumericValueDataType = ValueRangeTextBox.IsNumericType(ValueDataType);
                RefreshConversionHelpers();

                CoerceValue(ValueRangeTextBox.MinValueProperty);
                CoerceValue(ValueRangeTextBox.MaxValueProperty);

                CoerceValue(ValueRangeTextBox.ValueProperty);

                CoerceValue(ValueRangeTextBox.NullValueProperty);

                CoerceValue(TextBox.TextProperty);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Initialization of the ValueRangeTextBox failed.", exception);
            }
            finally
            {
                IsFinalizingInitialization = false;
            }

            base.OnInitialized(e);
        }

        #endregion ISupportInitialize

        [Flags]
        private enum ValueRangeTextBoxFlags
        {
            IsFinalizingInitialization = 1,
            IsForcingText = 2,
            IsForcingValue = 4,
            IsInValueChanged = 8,
            IsNumericValueDataType = 16
        }
    }
}