/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： MultiLineTextEditor.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-25 23:23
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
    /// <summary>
    /// 多行编辑框
    /// </summary>
    [TemplatePart(Name = PART_ResizeThumb, Type = typeof(Thumb))]
    public class MultiLineTextEditor : ContentControl
    {
        private const string PART_ResizeThumb = "PART_ResizeThumb";

        #region Members

        private Thumb _resizeThumb;

        #endregion //Members

        #region Properties

        #region DropDownHeight

        public static readonly DependencyProperty DropDownHeightProperty = DependencyProperty.Register(
            "DropDownHeight", typeof(double), typeof(MultiLineTextEditor), new UIPropertyMetadata(150.0));

        /// <summary>
        /// 编辑窗高度
        /// </summary>
        public double DropDownHeight
        {
            get { return (double)GetValue(DropDownHeightProperty); }
            set { SetValue(DropDownHeightProperty, value); }
        }

        #endregion DropDownHeight

        #region DropDownWidth

        public static readonly DependencyProperty DropDownWidthProperty = DependencyProperty.Register("DropDownWidth",
            typeof(double), typeof(MultiLineTextEditor), new UIPropertyMetadata(200.0));

        /// <summary>
        /// 编辑窗宽度
        /// </summary>
        public double DropDownWidth
        {
            get { return (double)GetValue(DropDownWidthProperty); }
            set { SetValue(DropDownWidthProperty, value); }
        }

        #endregion DropDownWidth

        #region IsOpen

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool),
            typeof(MultiLineTextEditor), new UIPropertyMetadata(false, OnIsOpenChanged));

        /// <summary>
        /// 打开编辑窗
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void OnIsOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MultiLineTextEditor multiLineTextEditor = o as MultiLineTextEditor;
            if (multiLineTextEditor != null)
                multiLineTextEditor.OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsOpenChanged(bool oldValue, bool newValue)
        {
        }

        #endregion //IsOpen

        #region IsSpellCheckEnabled

        public static readonly DependencyProperty IsSpellCheckEnabledProperty =
            DependencyProperty.Register("IsSpellCheckEnabled", typeof(bool), typeof(MultiLineTextEditor),
                new UIPropertyMetadata(false));

        /// <summary>
        /// 是否打开文件拼写检查
        /// </summary>
        public bool IsSpellCheckEnabled
        {
            get { return (bool)GetValue(IsSpellCheckEnabledProperty); }
            set { SetValue(IsSpellCheckEnabledProperty, value); }
        }

        #endregion IsSpellCheckEnabled

        #region IsReadOnly

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly",
            typeof(bool), typeof(MultiLineTextEditor), new UIPropertyMetadata(false));

        /// <summary>
        /// 文本只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        #endregion IsReadOnly

        #region Text

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string),
            typeof(MultiLineTextEditor),
            new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnTextChanged));

        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MultiLineTextEditor textEditor = o as MultiLineTextEditor;
            if (textEditor != null)
                textEditor.OnTextChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnTextChanged(string oldValue, string newValue)
        {
        }

        #endregion //Text

        #region TextAlignment

        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment",
            typeof(TextAlignment), typeof(MultiLineTextEditor), new UIPropertyMetadata(TextAlignment.Left));

        /// <summary>
        /// 文本对齐方式
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        #endregion TextAlignment

        #region TextWrapping

        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping",
            typeof(TextWrapping), typeof(MultiLineTextEditor), new UIPropertyMetadata(TextWrapping.NoWrap));

        /// <summary>
        /// 文本换行
        /// </summary>
        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }

        #endregion TextWrapping

        #endregion //Properties

        #region Constructors

        static MultiLineTextEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiLineTextEditor),
                new FrameworkPropertyMetadata(typeof(MultiLineTextEditor)));
        }

        public MultiLineTextEditor()
        {
            Keyboard.AddKeyDownHandler(this, OnKeyDown);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
        }

        #endregion //Constructors

        #region Bass Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_resizeThumb != null)
                _resizeThumb.DragDelta -= ResizeThumb_DragDelta;

            _resizeThumb = GetTemplateChild(PART_ResizeThumb) as Thumb;

            if (_resizeThumb != null)
                _resizeThumb.DragDelta += ResizeThumb_DragDelta;
        }

        #endregion //Bass Class Overrides

        #region Event Handlers

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsOpen)
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    IsOpen = true;
                    e.Handled = true;
                }
            }
            else
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e)
                    || (e.Key == Key.Escape)
                    || (e.Key == Key.Tab))
                {
                    CloseEditor();
                    e.Handled = true;
                }
            }
        }

        private void OnMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            CloseEditor();
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double yadjust = DropDownHeight + e.VerticalChange;
            double xadjust = DropDownWidth + e.HorizontalChange;

            if ((xadjust >= 0) && (yadjust >= 0))
            {
                DropDownWidth = xadjust;
                DropDownHeight = yadjust;
            }
        }

        #endregion //Event Handlers

        #region Methods

        private void CloseEditor()
        {
            if (IsOpen)
                IsOpen = false;
            ReleaseMouseCapture();
        }

        #endregion //Methods
    }
}