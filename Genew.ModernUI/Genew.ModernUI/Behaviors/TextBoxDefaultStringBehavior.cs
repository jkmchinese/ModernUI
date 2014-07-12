/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： TextBoxDefaultStringBehavior.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-11 23:33:25
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using System.ComponentModel;

namespace ModernUI.Behaviors
{
    /// <summary>
    /// 呈现文本框默认文本行为
    /// </summary>
    public sealed class TextBoxDefaultStringBehavior : Behavior<TextBox>, INotifyPropertyChanged
    {
        #region Constructors

        public TextBoxDefaultStringBehavior()
        {
            DefaultString = "文本框默认文本";
        }

        #endregion

        #region Fields
        public static readonly DependencyProperty DefaultStringProperty = DependencyProperty.Register("DefaultString", typeof(string), typeof(TextBoxDefaultStringBehavior), new PropertyMetadata(OnDefaultStringChanged));

        /// <summary>
        /// 文本框无内容时应用的画刷
        /// </summary>
        private VisualBrush m_defaultBrush;

        /// <summary>
        /// 文本框原本的画刷
        /// </summary>
        private Brush m_associatedBrush;

        public Brush AssociatedBrush
        {
            get
            {
                //modified by weixuanan at 2013-1-22 17:53:39
                if (AssociatedObject == null)
                {
                    return m_defaultBrush;
                }
                if (AssociatedObject.IsFocused
                    || !string.IsNullOrEmpty(AssociatedObject.Text)
                    )
                {
                    return m_associatedBrush;
                }
                else
                {
                    return m_defaultBrush;
                }
            }
            set
            {
                if (value == m_defaultBrush)
                {
                    PropertyChangedEventHandler e = PropertyChanged;
                    if (e != null)
                    {
                        e(this, new PropertyChangedEventArgs("TbkBrush"));
                    }
                    return;
                }
                m_associatedBrush = value;
                PropertyChangedEventHandler ev = PropertyChanged;
                if (ev != null)
                {
                    ev(this, new PropertyChangedEventArgs("TbkBrush"));
                }
            }
        }

        public Brush TbkBrush
        {
            get
            {
                return m_associatedBrush;
            }
        }

        public double FontSize
        {
            get;
            set;
        }

        #endregion

        #region Properties

        public string DefaultString
        {
            get
            {
                return (string)GetValue(DefaultStringProperty);
            }
            set
            {
                SetValue(DefaultStringProperty, value);
            }
        }
        #endregion

        #region Override Methods

        protected override void OnAttached()
        {
            AssociatedBrush = AssociatedObject.Background;
            AssociatedObject.SetBinding(TextBox.BackgroundProperty, new Binding() { Source = this, Path = new PropertyPath("AssociatedBrush"), Mode = BindingMode.TwoWay });

            TextBox tb = new TextBox() { FontWeight = FontWeights.Bold, FontStyle = FontStyles.Italic, Foreground = Brushes.Gray, TextAlignment = TextAlignment.Left };
            tb.SetBinding(TextBox.TextProperty, new Binding() { Source = this, Path = new PropertyPath("DefaultString"), Mode = BindingMode.OneWay });
            tb.SetBinding(TextBox.BackgroundProperty, new Binding() { Source = this, Path = new PropertyPath("TbkBrush"), Mode = BindingMode.OneWay });
            if (FontSize == 0)
            {
                tb.SetBinding(TextBox.FontSizeProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath("FontSize"), Mode = BindingMode.OneWay });
            }
            else
            {
                tb.FontSize = FontSize;
            }
            tb.SetBinding(TextBox.WidthProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath("ActualWidth"), Mode = BindingMode.OneWay });
            tb.SetBinding(TextBox.HeightProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath("ActualHeight"), Mode = BindingMode.OneWay });
            tb.SetBinding(TextBox.VerticalAlignmentProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath("VerticalAlignment"), Mode = BindingMode.OneWay });
            tb.SetBinding(TextBox.HorizontalAlignmentProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath("HorizontalAlignment"), Mode = BindingMode.OneWay });
            tb.SetBinding(TextBox.VerticalContentAlignmentProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath("VerticalContentAlignment"), Mode = BindingMode.OneWay });
            tb.SetBinding(TextBox.HorizontalContentAlignmentProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath("HorizontalContentAlignment"), Mode = BindingMode.OneWay });
            tb.SetBinding(TextBox.BorderThicknessProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath("BorderThickness"), Mode = BindingMode.OneWay });
            tb.SetBinding(TextBox.BorderBrushProperty, new Binding() { Source = AssociatedObject, Path = new PropertyPath("BorderBrush"), Mode = BindingMode.OneWay });
            tb.IsHitTestVisible = false;

            m_defaultBrush = new VisualBrush();
            m_defaultBrush.Visual = tb;
            m_defaultBrush.Stretch = Stretch.None;

            AssociatedObject.GotFocus += new RoutedEventHandler(AssociatedObject_GotFocus);
            AssociatedObject.LostFocus += new RoutedEventHandler(AssociatedObject_LostFocus);
            if (AssociatedObject.Text == string.Empty)
            {
                SetDefault();
            }
            base.OnAttached();

            AssociatedObject.TextChanged += new TextChangedEventHandler(AssociatedObject_TextChanged);
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AssociatedObject.Text != string.Empty)
            {
                SetAssociated();
            }
            else if (!AssociatedObject.IsFocused)
            {
                SetDefault();
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.GotFocus -= new RoutedEventHandler(AssociatedObject_GotFocus);
            AssociatedObject.LostFocus -= new RoutedEventHandler(AssociatedObject_LostFocus);
            m_defaultBrush = null;

            base.OnDetaching();
        }

        #endregion

        #region Private Methods

        void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AssociatedObject.Text))
            {
                SetDefault();
            }
        }

        void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {
            SetAssociated();
        }

        private static void OnDefaultStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBoxDefaultStringBehavior)
            {
                TextBox textBox = (d as TextBoxDefaultStringBehavior).AssociatedObject;
                if (textBox != null
                    && !textBox.IsFocused
                    && (textBox.Text == (string)e.OldValue
                    || e.OldValue == null))
                {
                    (d as TextBoxDefaultStringBehavior).SetDefault();
                }
            }
        }
        #endregion

        #region Internal Methods
        internal void SetDefault()
        {
            AssociatedObject.Background = m_defaultBrush;
        }

        internal void SetAssociated()
        {
            AssociatedObject.Background = m_associatedBrush;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
