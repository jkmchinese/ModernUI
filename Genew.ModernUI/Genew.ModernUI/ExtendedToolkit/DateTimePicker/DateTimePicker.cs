/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： DateTimePicker.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-18 10:31
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ModernUI.ExtendedToolkit.Utilities;

namespace ModernUI.ExtendedToolkit
{
    [TemplatePart(Name = PART_Calendar, Type = typeof(Calendar))]
    [TemplatePart(Name = PART_Popup, Type = typeof(Popup))]
    [TemplatePart(Name = PART_TimeUpDown, Type = typeof(TimePicker))]
    public class DateTimePicker : DateTimeUpDown
    {
        private const string PART_Calendar = "PART_Calendar";
        private const string PART_Popup = "PART_Popup";
        private const string PART_TimeUpDown = "PART_TimeUpDown";

        #region Members

        private Calendar _calendar;
        private DateTime? _initialValue;
        private Popup _popup;
        private TimePicker _timePicker;

        #endregion //Members

        #region Properties

        #region AutoCloseCalendar

        public static readonly DependencyProperty AutoCloseCalendarProperty =
            DependencyProperty.Register("AutoCloseCalendar", typeof(bool), typeof(DateTimePicker),
                new UIPropertyMetadata(false));

        public bool AutoCloseCalendar
        {
            get { return (bool)GetValue(AutoCloseCalendarProperty); }
            set { SetValue(AutoCloseCalendarProperty, value); }
        }

        #endregion //AutoCloseCalendar

        #region IsOpen

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool),
            typeof(DateTimePicker), new UIPropertyMetadata(false, OnIsOpenChanged));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)d;
            if (dateTimePicker != null)
                dateTimePicker.OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private void OnIsOpenChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                _initialValue = Value;
        }

        #endregion //IsOpen

        #region TimeFormat

        public static readonly DependencyProperty TimeFormatProperty = DependencyProperty.Register("TimeFormat",
            typeof(TimeFormat), typeof(DateTimePicker), new UIPropertyMetadata(TimeFormat.ShortTime));

        public TimeFormat TimeFormat
        {
            get { return (TimeFormat)GetValue(TimeFormatProperty); }
            set { SetValue(TimeFormatProperty, value); }
        }

        #endregion //TimeFormat

        #region TimeFormatString

        public static readonly DependencyProperty TimeFormatStringProperty =
            DependencyProperty.Register("TimeFormatString", typeof(string), typeof(DateTimePicker),
                new UIPropertyMetadata(default(String)), IsTimeFormatStringValid);

        public string TimeFormatString
        {
            get { return (string)GetValue(TimeFormatStringProperty); }
            set { SetValue(TimeFormatStringProperty, value); }
        }

        private static bool IsTimeFormatStringValid(object value)
        {
            return DateTimeUpDown.IsFormatStringValid(value);
        }

        #endregion //TimeFormatString

        #region TimeWatermark

        public static readonly DependencyProperty TimeWatermarkProperty = DependencyProperty.Register("TimeWatermark",
            typeof(object), typeof(DateTimePicker), new UIPropertyMetadata(null));

        public object TimeWatermark
        {
            get { return GetValue(TimeWatermarkProperty); }
            set { SetValue(TimeWatermarkProperty, value); }
        }

        #endregion //TimeWatermark

        #region TimeWatermarkTemplate

        public static readonly DependencyProperty TimeWatermarkTemplateProperty =
            DependencyProperty.Register("TimeWatermarkTemplate", typeof(DataTemplate), typeof(DateTimePicker),
                new UIPropertyMetadata(null));

        public DataTemplate TimeWatermarkTemplate
        {
            get { return (DataTemplate)GetValue(TimeWatermarkTemplateProperty); }
            set { SetValue(TimeWatermarkTemplateProperty, value); }
        }

        #endregion //TimeWatermarkTemplate

        #endregion //Properties

        #region Constructors

        static DateTimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimePicker),
                new FrameworkPropertyMetadata(typeof(DateTimePicker)));
        }

        public DateTimePicker()
        {
            AddHandler(UIElement.KeyDownEvent, new KeyEventHandler(OnKeyDown), true);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
        }

        #endregion //Constructors

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_popup != null)
                _popup.Opened -= Popup_Opened;

            _popup = GetTemplateChild(PART_Popup) as Popup;

            if (_popup != null)
                _popup.Opened += Popup_Opened;

            if (_calendar != null)
                _calendar.SelectedDatesChanged -= Calendar_SelectedDatesChanged;

            _calendar = GetTemplateChild(PART_Calendar) as Calendar;

            if (_calendar != null)
            {
                _calendar.SelectedDatesChanged += Calendar_SelectedDatesChanged;
                _calendar.SelectedDate = Value ?? null;
                _calendar.DisplayDate = Value ?? DateTime.Now;
            }

            _timePicker = GetTemplateChild(PART_TimeUpDown) as TimePicker;
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (Mouse.Captured is CalendarItem)
            {
                Mouse.Capture(null);

                if (AutoCloseCalendar)
                {
                    CloseDateTimePicker(true);
                }
            }
        }

        protected override void OnValueChanged(DateTime? oldValue, DateTime? newValue)
        {
            if (_calendar != null && _calendar.SelectedDate != newValue)
            {
                _calendar.SelectedDate = newValue;
                _calendar.DisplayDate = newValue ?? DateTime.Now;
            }

            base.OnValueChanged(oldValue, newValue);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            //if the calendar is open then we don't want to modify the behavior of navigating the calendar control with the Up/Down keys.
            if (!IsOpen)
                base.OnPreviewKeyDown(e);
        }

        #endregion //Base Class Overrides

        #region Event Handlers

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsOpen)
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    IsOpen = true;
                    // Calendar will get focus in Calendar_Loaded().
                    e.Handled = true;
                }
            }
            else
            {
                if (_calendar.IsKeyboardFocusWithin)
                {
                    if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                    {
                        CloseDateTimePicker(true);
                        e.Handled = true;
                    }
                    else
                    {
                        ManageEnterEscapeKeys(e);
                    }
                }
                else if (_timePicker.IsKeyboardFocusWithin && !_timePicker.IsOpen && !e.Handled)
                {
                    ManageEnterEscapeKeys(e);
                }
            }
        }

        private void OnMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            CloseDateTimePicker(false);
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var newDate = (DateTime?)e.AddedItems[0];

                if ((Value != null) && (newDate != null) && newDate.HasValue)
                {
                    // Only change the year, month, and day part of the value. Keep everything to the last "tick."
                    // "Milliseconds" aren't precise enough. Use a mathematical scheme instead.
                    newDate = newDate.Value.Date + Value.Value.TimeOfDay;
                }

                if (!object.Equals(newDate, Value))
                    Value = newDate;
            }
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            if (_calendar != null)
                _calendar.Focus();
        }

        #endregion //Event Handlers

        #region Methods

        private void CloseDateTimePicker(bool isFocusOnTextBox)
        {
            if (IsOpen)
                IsOpen = false;
            ReleaseMouseCapture();

            if (isFocusOnTextBox && (TextBox != null))
                TextBox.Focus();
        }

        private void ManageEnterEscapeKeys(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CloseDateTimePicker(true);
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // Avoid setting the "Value" property when no change has occurred.
                // The original value may not be a local value. Setting
                // it, even with the same value, will override a one-way binding.
                if (!object.Equals(Value, _initialValue))
                {
                    Value = _initialValue;
                }
                CloseDateTimePicker(true);
                e.Handled = true;
            }
        }

        #endregion //Methods
    }
}