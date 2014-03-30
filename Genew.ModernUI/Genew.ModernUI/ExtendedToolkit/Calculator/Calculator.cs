/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： Calculator.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-29 17:09:12
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Genew.ModernUI.ExtendedToolkit.Utilities;

namespace Genew.ModernUI.ExtendedToolkit
{
    /// <summary>
    /// Calculator
    /// </summary>
    [TemplatePart(Name = "PART_CalculatorButtonPanel", Type = typeof(ContentControl))]
    public class Calculator : Control
    {
        #region Fields

        private const string PART_CalculatorButtonPanel = "PART_CalculatorButtonPanel";
        private ContentControl m_buttonPanel;
        private bool m_showNewNumber = true;
        private decimal m_previousValue;
        private Operation m_lastOperation = Operation.None;
        private readonly Dictionary<Button, DispatcherTimer> m_timers = new Dictionary<Button, DispatcherTimer>();

        public static readonly DependencyProperty CalculatorButtonTypeProperty = DependencyProperty.RegisterAttached("CalculatorButtonType", typeof(CalculatorButtonType), typeof(Calculator), new UIPropertyMetadata(CalculatorButtonType.None, OnCalculatorButtonTypeChanged));
        public static readonly DependencyProperty DisplayTextProperty = DependencyProperty.Register("DisplayText", typeof(string), typeof(Calculator), new UIPropertyMetadata("0", OnDisplayTextChanged));
        public static readonly DependencyProperty MemoryProperty = DependencyProperty.Register("Memory", typeof(decimal), typeof(Calculator), new UIPropertyMetadata(default(decimal)));
        public static readonly DependencyProperty PrecisionProperty = DependencyProperty.Register("Precision", typeof(int), typeof(Calculator), new UIPropertyMetadata(6));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(decimal?), typeof(Calculator), new FrameworkPropertyMetadata(default(decimal), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(Calculator));

        #endregion

        #region Constructors

        static Calculator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Calculator), new FrameworkPropertyMetadata(typeof(Calculator)));
        }

        public Calculator()
        {
            CommandBindings.Add(new CommandBinding(CalculatorCommands.CalculatorButtonClick, ExecuteCalculatorButtonClick));
            AddHandler(MouseDownEvent, new MouseButtonEventHandler(Calculator_OnMouseDown), true);
        }

        #endregion

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_buttonPanel = GetTemplateChild(PART_CalculatorButtonPanel) as ContentControl;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            var buttonType = CalculatorUtilities.GetCalculatorButtonTypeFromText(e.Text);
            if (buttonType != CalculatorButtonType.None)
            {
                SimulateCalculatorButtonClick(buttonType);
                ProcessCalculatorButton(buttonType);
            }
        }

        #endregion

        #region Properties

        public static CalculatorButtonType GetCalculatorButtonType(DependencyObject target)
        {
            return (CalculatorButtonType)target.GetValue(CalculatorButtonTypeProperty);
        }

        public string DisplayText
        {
            get
            {
                return (string)GetValue(DisplayTextProperty);
            }
            set
            {
                SetValue(DisplayTextProperty, value);
            }
        }

        public decimal Memory
        {
            get
            {
                return (decimal)GetValue(MemoryProperty);
            }
            set
            {
                SetValue(MemoryProperty, value);
            }
        }

        public int Precision
        {
            get
            {
                return (int)GetValue(PrecisionProperty);
            }
            set
            {
                SetValue(PrecisionProperty, value);
            }
        }

        public decimal? Value
        {
            get
            {
                return (decimal?)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
        
        #endregion

        #region Events

        public event RoutedPropertyChangedEventHandler<object> ValueChanged
        {
            add
            {
                AddHandler(ValueChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ValueChangedEvent, value);
            }
        }

        #endregion

        #region Private Methods

        private void Calculator_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsFocused)
            {
                Focus();
                e.Handled = true;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= Timer_Tick;

            if (m_timers.ContainsValue(timer))
            {
                var button = m_timers.Where(x => x.Value == timer).Select(x => x.Key).FirstOrDefault();
                if (button != null)
                {
                    VisualStateManager.GoToState(button, button.IsMouseOver ? "MouseOver" : "Normal", true);
                    m_timers.Remove(button);
                }
            }
        }

        private static void OnCalculatorButtonTypeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            OnCalculatorButtonTypeChanged(o, (CalculatorButtonType)e.OldValue, (CalculatorButtonType)e.NewValue);
        }

        private static void OnCalculatorButtonTypeChanged(DependencyObject o, CalculatorButtonType oldValue, CalculatorButtonType newValue)
        {
            var button = o as Button;
            if (button != null)
            {
                button.CommandParameter = newValue;
                button.Content = CalculatorUtilities.GetCalculatorButtonContent(newValue);
            }

        }

        private static void OnDisplayTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Calculator calculator = o as Calculator;
            if (calculator != null)
                calculator.OnDisplayTextChanged((string)e.OldValue, (string)e.NewValue);
        }

        private static void OnValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Calculator calculator = o as Calculator;
            if (calculator != null)
                calculator.OnValueChanged((decimal?)e.OldValue, (decimal?)e.NewValue);
        }

        internal void InitializeToValue(decimal? value)
        {
            m_previousValue = 0;
            m_lastOperation = Operation.None;
            m_showNewNumber = true;
            Value = value;
            // Since the display text may be out of sync
            // with "Value", this call will force the
            // text update if Value was already equal to
            // the value parameter.
            this.SetDisplayText(value);
        }

        private void Calculate()
        {
            if (m_lastOperation == Operation.None)
                return;

            try
            {
                Value = Decimal.Round(CalculateValue(m_lastOperation), Precision);
                SetDisplayText(Value); //Set DisplayText even when Value doesn't change
            }
            catch
            {
                Value = null;
                DisplayText = "ERROR";
            }
        }

        private void SetDisplayText(decimal? newValue)
        {
            if (newValue.HasValue && (newValue.Value != 0))
                DisplayText = newValue.ToString();
            else
                DisplayText = "0";
        }

        private void Calculate(Operation newOperation)
        {
            if (!m_showNewNumber)
                Calculate();

            m_lastOperation = newOperation;
        }

        private void Calculate(Operation currentOperation, Operation newOperation)
        {
            m_lastOperation = currentOperation;
            Calculate();
            m_lastOperation = newOperation;
        }

        private decimal CalculateValue(Operation operation)
        {
            decimal newValue = decimal.Zero;
            decimal currentValue = CalculatorUtilities.ParseDecimal(DisplayText);

            switch (operation)
            {
                case Operation.Add:
                    newValue = CalculatorUtilities.Add(m_previousValue, currentValue);
                    break;
                case Operation.Subtract:
                    newValue = CalculatorUtilities.Subtract(m_previousValue, currentValue);
                    break;
                case Operation.Multiply:
                    newValue = CalculatorUtilities.Multiply(m_previousValue, currentValue);
                    break;
                case Operation.Divide:
                    newValue = CalculatorUtilities.Divide(m_previousValue, currentValue);
                    break;
                //case Operation.Percent:
                //    newValue = CalculatorUtilities.Percent(_previousValue, currentValue);
                //    break;
                case Operation.Sqrt:
                    newValue = CalculatorUtilities.SquareRoot(currentValue);
                    break;
                case Operation.Fraction:
                    newValue = CalculatorUtilities.Fraction(currentValue);
                    break;
                case Operation.Negate:
                    newValue = CalculatorUtilities.Negate(currentValue);
                    break;
                default:
                    newValue = decimal.Zero;
                    break;
            }

            return newValue;
        }

        private void ProcessBackKey()
        {
            string displayText;
            if (DisplayText.Length > 1 && !(DisplayText.Length == 2 && DisplayText[0] == '-'))
            {
                displayText = DisplayText.Remove(DisplayText.Length - 1, 1);
            }
            else
            {
                displayText = "0";
                m_showNewNumber = true;
            }

            DisplayText = displayText;
        }

        private void ProcessCalculatorButton(CalculatorButtonType buttonType)
        {
            if (CalculatorUtilities.IsDigit(buttonType))
                ProcessDigitKey(buttonType);
            else if ((CalculatorUtilities.IsMemory(buttonType)))
                ProcessMemoryKey(buttonType);
            else
                ProcessOperationKey(buttonType);
        }

        private void ProcessDigitKey(CalculatorButtonType buttonType)
        {
            if (m_showNewNumber)
                DisplayText = CalculatorUtilities.GetCalculatorButtonContent(buttonType);
            else
                DisplayText += CalculatorUtilities.GetCalculatorButtonContent(buttonType);

            m_showNewNumber = false;
        }

        private void ProcessMemoryKey(CalculatorButtonType buttonType)
        {
            decimal currentValue = CalculatorUtilities.ParseDecimal(DisplayText);

            switch (buttonType)
            {
                case CalculatorButtonType.MAdd:
                    Memory += currentValue;
                    break;
                case CalculatorButtonType.MC:
                    Memory = decimal.Zero;
                    break;
                case CalculatorButtonType.MR:
                    DisplayText = Memory.ToString();
                    break;
                case CalculatorButtonType.MS:
                    Memory = currentValue;
                    break;
                case CalculatorButtonType.MSub:
                    Memory -= currentValue;
                    break;
                default:
                    break;
            }

            m_showNewNumber = true;
        }

        private void ProcessOperationKey(CalculatorButtonType buttonType)
        {
            switch (buttonType)
            {
                case CalculatorButtonType.Add:
                    Calculate(Operation.Add);
                    break;
                case CalculatorButtonType.Subtract:
                    Calculate(Operation.Subtract);
                    break;
                case CalculatorButtonType.Multiply:
                    Calculate(Operation.Multiply);
                    break;
                case CalculatorButtonType.Divide:
                    Calculate(Operation.Divide);
                    break;
                case CalculatorButtonType.Percent:
                    if (m_lastOperation != Operation.None)
                    {
                        decimal currentValue = CalculatorUtilities.ParseDecimal(DisplayText);
                        decimal newValue = CalculatorUtilities.Percent(m_previousValue, currentValue);
                        DisplayText = newValue.ToString();
                    }
                    else
                    {
                        DisplayText = "0";
                        m_showNewNumber = true;
                    }
                    return;
                case CalculatorButtonType.Sqrt:
                    Calculate(Operation.Sqrt, Operation.None);
                    break;
                case CalculatorButtonType.Fraction:
                    Calculate(Operation.Fraction, Operation.None);
                    break;
                case CalculatorButtonType.Negate:
                    Calculate(Operation.Negate, Operation.None);
                    break;
                case CalculatorButtonType.Equal:
                    Calculate(Operation.None);
                    break;
                case CalculatorButtonType.Clear:
                    Calculate(Operation.Clear, Operation.None);
                    break;
                case CalculatorButtonType.Cancel:
                    DisplayText = m_previousValue.ToString();
                    m_lastOperation = Operation.None;
                    m_showNewNumber = true;
                    return;
                case CalculatorButtonType.Back:
                    ProcessBackKey();
                    return;
                default:
                    break;
            }

            Decimal.TryParse(DisplayText, out m_previousValue);
            m_showNewNumber = true;
        }

        private void SimulateCalculatorButtonClick(CalculatorButtonType buttonType)
        {
            var button = CalculatorUtilities.FindButtonByCalculatorButtonType(m_buttonPanel, buttonType);
            if (button != null)
            {
                VisualStateManager.GoToState(button, "Pressed", true);
                DispatcherTimer timer;
                if (m_timers.ContainsKey(button))
                {
                    timer = m_timers[button];
                    timer.Stop();
                }
                else
                {
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromMilliseconds(100);
                    timer.Tick += Timer_Tick;
                    m_timers.Add(button, timer);
                }

                timer.Start();
            }
        }

        private void ExecuteCalculatorButtonClick(object sender, ExecutedRoutedEventArgs e)
        {
            var buttonType = (CalculatorButtonType)e.Parameter;
            ProcessCalculatorButton(buttonType);
        }

        #endregion

        #region Protect Methods

        protected virtual void OnDisplayTextChanged(string oldValue, string newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        protected virtual void OnValueChanged(decimal? oldValue, decimal? newValue)
        {
            SetDisplayText(newValue);

            RoutedPropertyChangedEventArgs<object> args = new RoutedPropertyChangedEventArgs<object>(oldValue, newValue);
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set CalculatorButton Type
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetCalculatorButtonType(DependencyObject target, CalculatorButtonType value)
        {
            target.SetValue(CalculatorButtonTypeProperty, value);
        }

        #endregion

    }

    /// <summary>
    /// Type of Calculator Button 
    /// </summary>
    public enum CalculatorButtonType
    {
        Add,
        Back,
        Cancel,
        Clear,
        Decimal,
        Divide,
        Eight,
        Equal,
        Five,
        Four,
        Fraction,
        MAdd,
        MC,
        MR,
        MS,
        MSub,
        Multiply,
        Negate,
        Nine,
        None,
        One,
        Percent,
        Seven,
        Six,
        Sqrt,
        Subtract,
        Three,
        Two,
        Zero
    }

    /// <summary>
    /// Type of Calculator Operation
    /// </summary>
    public enum Operation
    {
        Add,
        Subtract,
        Divide,
        Multiply,
        Percent,
        Sqrt,
        Fraction,
        None,
        Clear,
        Negate
    }
}
