/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ScrollStateBehavior.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-11 23:34:15
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Specialized;
using System.Windows.Controls.Primitives;
using ModernUI.UIHelper;

namespace ModernUI.Behaviors
{
    /// <summary>
    /// 将控件的滚动条与导航控件进行绑定的行为
    /// </summary>
    public class ScrollStateBehavior : Behavior<FrameworkElement>
    {
        private ScrollViewer m_scrollViewer = null;

        public UIElement NaviLeft
        {
            get;
            set;
        }
        public UIElement NaviRight
        {
            get;
            set;
        }
        public UIElement NaviUp
        {
            get;
            set;
        }
        public UIElement NaviDown
        {
            get;
            set;
        }
        public bool AutoScroll
        {
            get;
            set;
        }

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
            if (AutoScroll && (AssociatedObject is ItemsControl))
            {
                var collection = (AssociatedObject as ItemsControl).Items.SourceCollection
                    as INotifyCollectionChanged;

                if (collection != null)
                {
                    collection.CollectionChanged += ScrollStateBehavior_CollectionChanged;
                }
            }
            base.OnAttached();
        }

        void ScrollStateBehavior_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!AssociatedObject.IsLoaded)
            {
                return;
            }
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                m_scrollViewer.ScrollToBottom();
                m_scrollViewer.ScrollToLeftEnd();
            }
        }

        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            m_scrollViewer = AssociatedObject.FindChild<ScrollViewer>() as ScrollViewer;
            if (m_scrollViewer != null)
            {
                m_scrollViewer.ScrollChanged += m_scrollViewer_ScrollChanged;
            }
            m_scrollViewer_ScrollChanged(null, null);
            BindButtonEvent(NaviLeft as ButtonBase, NaviRight as ButtonBase, NaviUp as ButtonBase, NaviDown as ButtonBase);
        }

        void m_scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (NaviLeft != null)
            {
                NaviLeft.IsEnabled = m_scrollViewer.HorizontalOffset > 0;
            }
            if (NaviRight != null)
            {
                NaviRight.IsEnabled = m_scrollViewer.HorizontalOffset + m_scrollViewer.ViewportWidth < m_scrollViewer.ExtentWidth;
            }
            if (NaviUp != null)
            {
                NaviUp.IsEnabled = m_scrollViewer.VerticalOffset > 0;
            }
            if (NaviDown != null)
            {
                NaviDown.IsEnabled = m_scrollViewer.VerticalOffset + m_scrollViewer.ViewportHeight < m_scrollViewer.ExtentHeight;
            }
        }

        private void BindButtonEvent(params ButtonBase[] buttons)
        {
            foreach (ButtonBase button in buttons)
            {
                if (button == null)
                {
                    continue;
                }
                button.Click += button_Click;
            }
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == NaviDown)
            {
                m_scrollViewer.ScrollToVerticalOffset(m_scrollViewer.VerticalOffset + m_scrollViewer.ViewportHeight);
            }
            else if (sender == NaviUp)
            {
                m_scrollViewer.ScrollToVerticalOffset(m_scrollViewer.VerticalOffset - m_scrollViewer.ViewportHeight);
            }
            else if (sender == NaviLeft)
            {
                m_scrollViewer.ScrollToHorizontalOffset(m_scrollViewer.HorizontalOffset + m_scrollViewer.ViewportWidth);
            }
            else if (sender == NaviRight)
            {
                m_scrollViewer.ScrollToHorizontalOffset(m_scrollViewer.HorizontalOffset - m_scrollViewer.ViewportWidth);
            }
        }
    }
}
