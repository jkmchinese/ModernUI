/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： MouseBehavior.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-11 23:59:36
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows;
using System.Windows.Interactivity;
using Genew.ModernUI.Properties;

namespace Genew.ModernUI.Behaviors
{
    /// <summary>
    /// 鼠标Behavior
    /// </summary>
    public class MouseBehavior : Behavior<DependencyObject>
    {
        #region Override Methods

        protected override void OnAttached()
        {
            if (AssociatedObject is FrameworkElement)
            {
                var obj = AssociatedObject as FrameworkElement;

                BindEvent(obj);

                m_timer = new DispatcherTimer(m_doubleClickDuration, DispatcherPriority.Normal, m_timer_Tick, Application.Current.Dispatcher);
            }
            else
            {
                throw new NotSupportedException("MouseBehavior只支持附加到FrameworkElement");
            }
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            UnBindEvent(AssociatedObject as FrameworkElement);

            m_timer = null;

            base.OnDetaching();
        }

        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(MouseBehavior));
        public static readonly DependencyProperty DoubleClickCommandProperty = DependencyProperty.Register("DoubleClickCommand", typeof(ICommand), typeof(MouseBehavior));
        public static readonly DependencyProperty LongPressedCommandProperty = DependencyProperty.Register("LongPressedCommand", typeof(ICommand), typeof(MouseBehavior));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(MouseBehavior));
        #endregion

        #region Properties
        public ICommand ClickCommand
        {
            get
            {
                return (ICommand)GetValue(ClickCommandProperty);
            }
            set
            {
                SetValue(ClickCommandProperty, value);
            }
        }
        public ICommand DoubleClickCommand
        {
            get
            {
                return (ICommand)GetValue(DoubleClickCommandProperty);
            }
            set
            {
                SetValue(DoubleClickCommandProperty, value);
            }
        }
        public ICommand LongPressedCommand
        {
            get
            {
                return (ICommand)GetValue(LongPressedCommandProperty);
            }
            set
            {
                SetValue(LongPressedCommandProperty, value);
            }
        }
        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        #endregion

        #region Event Handlers

        protected virtual void UnBindEvent(FrameworkElement element)
        {
            element.MouseDown -= obj_MouseLeftButtonDown;
            element.MouseUp -= obj_MouseLeftButtonUp;
        }

        protected virtual void BindEvent(FrameworkElement obj)
        {
            obj.MouseDown += new MouseButtonEventHandler(obj_MouseLeftButtonDown);
            obj.MouseUp += new MouseButtonEventHandler(obj_MouseLeftButtonUp);
        }

        protected void obj_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                m_hasDoubleClick = true;
                Reset();
            }
            else if (e.ClickCount == 1)
            {
                m_lastDownTime = DateTime.Now;
            }
        }

        protected void obj_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (m_hasDoubleClick)
            {
                InvokeDoubleClickCommand();
                m_hasDoubleClick = false;
                return;
            }

            if (DateTime.Now - m_lastDownTime > m_longPressedDuration)
            {
                Reset();
                InvokeLongPressedCommand();
            }
            else if (e.ClickCount == 1)
            {
                m_hasClick = true;
                m_timer.Start();
            }
        }

        private void m_timer_Tick(object sender, EventArgs e)
        {
            if (m_hasClick)
            {
                InvokeClickCommand();
            }
            Reset();
        }
        #endregion

        #region Private Methods

        private void Reset()
        {
            m_hasClick = false;
            m_timer.Stop();
        }

        private void InvokeClickCommand()
        {
            if (ClickCommand != null)
            {
                ClickCommand.Execute(CommandParameter);
            }
        }

        private void InvokeDoubleClickCommand()
        {
            if (DoubleClickCommand != null)
            {
                DoubleClickCommand.Execute(CommandParameter);
            }
        }

        private void InvokeLongPressedCommand()
        {
            if (LongPressedCommand != null)
            {
                LongPressedCommand.Execute(CommandParameter);
            }
        }
        #endregion

        #region Fields
        private readonly TimeSpan m_doubleClickDuration = TimeSpan.FromMilliseconds(int.Parse(Resources.DoublClickDuration));
        private readonly TimeSpan m_longPressedDuration = TimeSpan.FromMilliseconds(int.Parse(Resources.LongPressedDuration));
        private DateTime m_lastDownTime;
        private bool m_hasClick;
        private bool m_hasDoubleClick;
        private DispatcherTimer m_timer;
        #endregion
    }

}
