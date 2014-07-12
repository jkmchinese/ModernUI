/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： SelectorItem.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-26 23:22
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows;
using System.Windows.Controls;

namespace ModernUI.ExtendedToolkit.Primitives
{
    public class SelectorItem : ContentControl
    {
        #region Constructors

        static SelectorItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectorItem),
                new FrameworkPropertyMetadata(typeof(SelectorItem)));
        }

        #endregion //Constructors

        #region Properties

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected",
            typeof(bool), typeof(SelectorItem), new UIPropertyMetadata(false, OnIsSelectedChanged));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        internal Selector ParentSelector
        {
            get { return ItemsControl.ItemsControlFromItemContainer(this) as Selector; }
        }

        private static void OnIsSelectedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SelectorItem selectorItem = o as SelectorItem;
            if (selectorItem != null)
                selectorItem.OnIsSelectedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                RaiseEvent(new RoutedEventArgs(Selector.SelectedEvent, this));
            else
                RaiseEvent(new RoutedEventArgs(Selector.UnSelectedEvent, this));
        }

        #endregion //Properties

        #region Events

        public static readonly RoutedEvent SelectedEvent = Selector.SelectedEvent.AddOwner(typeof(SelectorItem));
        public static readonly RoutedEvent UnselectedEvent = Selector.UnSelectedEvent.AddOwner(typeof(SelectorItem));

        #endregion
    }
}