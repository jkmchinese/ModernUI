/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ItemSelectBehavior.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-12 00:09:14
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace ModernUI.Behaviors
{
    /// <summary>
    /// 控件展示时选中指定项
    /// </summary>
    public class ItemSelectBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ItemSelectBehavior));

        /// <summary>
        /// 需要在Load之后定位的Item
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return base.GetValue(SelectedItemProperty);
            }
            set
            {
                base.SetValue(SelectedItemProperty, value);
            }
        }

        protected override void OnAttached()
        {
            //使用委托取值，因为在OnAttached时SelectedItem还未赋值
            ItemSelecter.InitSelecter(AssociatedObject, () => SelectedItem);
            base.OnAttached();
        }
    }

    /// <summary>
    /// 选中指定项的策略
    /// </summary>
    internal class ItemSelecter
    {
        private static Dictionary<Type, Func<ItemSelecter>> _selecters;

        private static ItemSelecter _instance = new ItemSelecter();

        protected FrameworkElement m_container;

        protected ItemSelecter()
        {

        }

        static ItemSelecter()
        {
            _selecters = new Dictionary<Type, Func<ItemSelecter>>();
            _selecters.Add(typeof(TreeView), () => new TreeItemSelecter());
            _selecters.Add(typeof(ListBox), () => new ListItemSelecter());
            _selecters.Add(typeof(ListView), () => new ListItemSelecter());
        }

        /// <summary>
        /// 初始化container的选中Item策略
        /// </summary>
        /// <param name="container"></param>
        /// <param name="selectedItemGetter"></param>
        public static void InitSelecter(FrameworkElement container, Func<object> selectedItemGetter)
        {
            Func<ItemSelecter> selecterCreater;
            _selecters.TryGetValue(container.GetType(), out selecterCreater);
            if (selecterCreater != null)//container载入的时候定位到指定项
            {
                container.Loaded += (s, e) =>
                {
                    ItemSelecter selecter = selecterCreater();
                    selecter.m_container = container;
                    selecter.SelectItem(container, selectedItemGetter());
                };

                container.TargetUpdated += (s, e) =>
                {
                    if (!container.IsLoaded)
                    {
                        return;
                    }
                    ItemSelecter selecter = selecterCreater();
                    selecter.m_container = container;
                    selecter.SelectItem(container, selectedItemGetter());
                };
            }
        }

        protected virtual void SelectItem(FrameworkElement container, object item)
        {

        }
    }

    internal class TreeItemSelecter : ItemSelecter
    {
        private bool m_founded = false;

        protected override void SelectItem(FrameworkElement container, object item)
        {
            base.SelectItem(container, item);
            ItemsControl itemsControl = container as ItemsControl;
            if (itemsControl == null)
            {
                return;
            }
            SelectTreeViewItem(itemsControl, item);
        }

        /// <summary>
        /// 选中itemsControl下的item节点
        /// </summary>
        /// <param name="itemsControl"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool SelectTreeViewItem(ItemsControl itemsControl, object item)
        {
            if (itemsControl.ItemsSource == null)
            {
                return false;
            }
            if (m_founded)
            {
                return true;
            }
            TreeViewItem currentTreeViewItem = (TreeViewItem)itemsControl.ItemContainerGenerator.ContainerFromItem(item);
            if (currentTreeViewItem != null)
            {
                m_founded = true;
                //(m_container as TreeView).CollapseAll();//折叠因查找item而展开的所有项
                currentTreeViewItem.BringIntoView();
                return true;
            }

            foreach (object treeItem in itemsControl.ItemsSource)
            {
                currentTreeViewItem = (TreeViewItem)itemsControl.ItemContainerGenerator.ContainerFromItem(treeItem);
                if (currentTreeViewItem == null)//未显示的项的TreeViewItem为null
                {
                    continue;
                }
                if (currentTreeViewItem.ItemsSource == null)
                {
                    continue;
                }
                currentTreeViewItem.IsExpanded = true;//展开             
                WaitForPriority(DispatcherPriority.Background);
                ItemContainerGenerator itemContainerGenerator = currentTreeViewItem.ItemContainerGenerator;
                if (itemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                {
                    if (SelectTreeViewItem(currentTreeViewItem, item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static void WaitForPriority(DispatcherPriority priority)
        {
            DispatcherFrame frame = new DispatcherFrame();
            DispatcherOperation dispatcherOperation = Dispatcher.CurrentDispatcher.BeginInvoke(priority, new DispatcherOperationCallback(ExitFrameOperation), frame);
            Dispatcher.PushFrame(frame);
            if (dispatcherOperation.Status != DispatcherOperationStatus.Completed)
            {
                dispatcherOperation.Abort();
            }
        }

        private static object ExitFrameOperation(object obj)
        {
            ((DispatcherFrame)obj).Continue = false;
            return null;
        }
    }

    internal class ListItemSelecter : ItemSelecter
    {
        protected override void SelectItem(FrameworkElement container, object item)
        {
            base.SelectItem(container, item);
            ListBox list = (container as ListBox);
            if (list == null)
            {
                return;
            }
            list.ScrollIntoView(item);
        }
    }
}
