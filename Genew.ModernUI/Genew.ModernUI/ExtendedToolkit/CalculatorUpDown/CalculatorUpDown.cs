﻿/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： CalculatorUpDown.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-30 13:18
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ModernUI.ExtendedToolkit.Utilities;

namespace ModernUI.ExtendedToolkit
{
    [TemplatePart(Name = PART_CalculatorPopup, Type = typeof(Popup))]
    [TemplatePart(Name = PART_Calculator, Type = typeof(Calculator))]
    public class CalculatorUpDown : DecimalUpDown
    {
        private const string PART_CalculatorPopup = "PART_CalculatorPopup";
        private const string PART_Calculator = "PART_Calculator";

        #region Members

        private Calculator m_calculator;
        private Popup m_calculatorPopup;
        private Decimal? m_initialValue;

        #endregion //Members

        #region Properties

        #region DisplayText

        public static readonly DependencyProperty DisplayTextProperty = DependencyProperty.Register("DisplayText",
            typeof(string), typeof(CalculatorUpDown), new UIPropertyMetadata("0"));

        public string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }

        #endregion //DisplayText

        #region EnterClosesCalculator

        public static readonly DependencyProperty EnterClosesCalculatorProperty =
            DependencyProperty.Register("EnterClosesCalculator", typeof(bool), typeof(CalculatorUpDown),
                new UIPropertyMetadata(false));

        public bool EnterClosesCalculator
        {
            get { return (bool)GetValue(EnterClosesCalculatorProperty); }
            set { SetValue(EnterClosesCalculatorProperty, value); }
        }

        #endregion //EnterClosesCalculator

        #region IsOpen

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool),
            typeof(CalculatorUpDown), new UIPropertyMetadata(false, OnIsOpenChanged));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void OnIsOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            CalculatorUpDown calculatorUpDown = o as CalculatorUpDown;
            if (calculatorUpDown != null)
                calculatorUpDown.OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsOpenChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                m_initialValue = Value;
        }

        #endregion //IsOpen

        #region Memory

        public static readonly DependencyProperty MemoryProperty = DependencyProperty.Register("Memory",
            typeof(decimal), typeof(CalculatorUpDown), new UIPropertyMetadata(default(decimal)));

        public decimal Memory
        {
            get { return (decimal)GetValue(MemoryProperty); }
            set { SetValue(MemoryProperty, value); }
        }

        #endregion //Memory

        #region Precision

        public static readonly DependencyProperty PrecisionProperty = DependencyProperty.Register("Precision",
            typeof(int), typeof(CalculatorUpDown), new UIPropertyMetadata(6));

        public int Precision
        {
            get { return (int)GetValue(PrecisionProperty); }
            set { SetValue(PrecisionProperty, value); }
        }

        #endregion //Precision

        #endregion //Properties

        #region Constructors

        static CalculatorUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalculatorUpDown),
                new FrameworkPropertyMetadata(typeof(CalculatorUpDown)));
        }

        public CalculatorUpDown()
        {
            Keyboard.AddKeyDownHandler(this, OnKeyDown);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
        }

        #endregion //Constructors

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (m_calculatorPopup != null)
                m_calculatorPopup.Opened -= CalculatorPopup_Opened;

            m_calculatorPopup = GetTemplateChild(PART_CalculatorPopup) as Popup;

            if (m_calculatorPopup != null)
                m_calculatorPopup.Opened += CalculatorPopup_Opened;

            if (m_calculator != null)
                m_calculator.ValueChanged -= OnCalculatorValueChanged;

            m_calculator = GetTemplateChild(PART_Calculator) as Calculator;

            if (m_calculator != null)
                m_calculator.ValueChanged += OnCalculatorValueChanged;
        }

        private void OnCalculatorValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (IsValid(m_calculator.Value))
            {
                Value = m_calculator.Value;
            }
        }

        #endregion //Base Class Overrides

        #region Event Handlers

        private void CalculatorPopup_Opened(object sender, EventArgs e)
        {
            if (m_calculator != null)
            {
                m_calculator.InitializeToValue(Value);
                m_calculator.Focus();
            }
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            if (IsOpen && EnterClosesCalculator)
            {
                var buttonType = CalculatorUtilities.GetCalculatorButtonTypeFromText(e.Text);
                if (buttonType == CalculatorButtonType.Equal)
                {
                    CloseCalculatorUpDown(true);
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsOpen)
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    IsOpen = true;
                    // Calculator will get focus in CalculatorPopup_Opened().
                    e.Handled = true;
                }
            }
            else
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    CloseCalculatorUpDown(true);
                    e.Handled = true;
                }
                else if (e.Key == Key.Escape)
                {
                    if (EnterClosesCalculator)
                        Value = m_initialValue;
                    CloseCalculatorUpDown(true);
                    e.Handled = true;
                }
            }
        }

        private void OnMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            CloseCalculatorUpDown(false);
        }

        #endregion //Event Handlers

        #region Methods

        private void CloseCalculatorUpDown(bool isFocusOnTextBox)
        {
            if (IsOpen)
                IsOpen = false;
            ReleaseMouseCapture();

            if (isFocusOnTextBox && (TextBox != null))
                TextBox.Focus();
        }

        #endregion //Methods
    }
}