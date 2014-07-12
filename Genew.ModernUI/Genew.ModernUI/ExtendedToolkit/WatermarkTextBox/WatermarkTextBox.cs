/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： WatermarkTextBox.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-18 11:07
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModernUI.ExtendedToolkit
{
    /// <summary>
    /// 水印文本框
    /// </summary>
    public class WatermarkTextBox : AutoSelectTextBox
    {
        #region Properties

        #region SelectAllOnGotFocus

        [Obsolete("This property is obsolete and should no longer be used. Use AutoSelectTextBox.AutoSelectBehavior instead.")]
        public static readonly DependencyProperty SelectAllOnGotFocusProperty =
            DependencyProperty.Register("SelectAllOnGotFocus", typeof(bool), typeof(WatermarkTextBox),
                new PropertyMetadata(false));

        /// <summary>
        /// 聚焦时选中文本
        /// </summary>
        [Obsolete("This property is obsolete and should no longer be used. Use AutoSelectTextBox.AutoSelectBehavior instead.")]
        public bool SelectAllOnGotFocus
        {
            get { return (bool)GetValue(SelectAllOnGotFocusProperty); }
            set { SetValue(SelectAllOnGotFocusProperty, value); }
        }

        #endregion //SelectAllOnGotFocus

        #region Watermark

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark",
            typeof(object), typeof(WatermarkTextBox), new UIPropertyMetadata(null));

        /// <summary>
        /// 水印
        /// </summary>
        public object Watermark
        {
            get { return GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        #endregion //Watermark

        #region WatermarkTemplate

        public static readonly DependencyProperty WatermarkTemplateProperty =
            DependencyProperty.Register("WatermarkTemplate", typeof(DataTemplate), typeof(WatermarkTextBox),
                new UIPropertyMetadata(null));

        /// <summary>
        /// 水印数据模板
        /// </summary>
        public DataTemplate WatermarkTemplate
        {
            get { return (DataTemplate)GetValue(WatermarkTemplateProperty); }
            set { SetValue(WatermarkTemplateProperty, value); }
        }

        #endregion //WatermarkTemplate

        #endregion //Properties

        #region Constructors

        static WatermarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkTextBox),
                new FrameworkPropertyMetadata(typeof(WatermarkTextBox)));
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if (AutoSelectBehavior == AutoSelectBehavior.OnFocus)
                SelectAll();
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (!IsKeyboardFocused && AutoSelectBehavior == AutoSelectBehavior.OnFocus)
            {
                e.Handled = true;
                Focus();
            }

            base.OnPreviewMouseLeftButtonDown(e);
        }

        #endregion //Base Class Overrides
    }
}