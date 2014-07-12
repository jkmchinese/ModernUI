/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ExtensionMethod.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-11 23:58:29
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ModernUI.UIHelper
{
    /// <summary>
    /// UI扩展方法
    /// </summary>
    public static class ExtensionMethod
    {

        #region Private Methods
        /// <summary>
        /// 寻找特定类型的所有子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="children"></param>
        private static void FindChilds<T>(DependencyObject element, IList<T> children) where T : DependencyObject
        {
            int index = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < index; i++)
            {
                DependencyObject depObj = VisualTreeHelper.GetChild(element, i);
                if (null != depObj)
                {
                    if (depObj is T)
                        children.Add(depObj as T);
                    else
                    {
                        FindChilds<T>(depObj, children);
                    }
                }
            }
        }

        /// <summary>
        /// 获取依赖对象的父元素
        /// </summary>
        /// <param name="element">依赖对象</param>
        /// <returns>存在则返回依赖对象的父元素,否则返回null</returns>
        private static DependencyObject GetParent(DependencyObject element)
        {
            if (element is FrameworkElement)
            {
                var frameworkElement = element as FrameworkElement;
                if (frameworkElement.Parent != null)
                    return frameworkElement.Parent;
                else if (frameworkElement.TemplatedParent != null)
                    return frameworkElement.TemplatedParent;
            }
            if (element is Visual)
            {
                return VisualTreeHelper.GetParent(element);
            }
            return null;
        }

        /// <summary>
        /// 获取依赖对象的子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element">依赖对象的</param>
        /// <returns>存在则返回依赖对象的子元素,否则返回null</returns>
        private static T GetChild<T>(DependencyObject element) where T : DependencyObject
        {
            int index = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < index; i++)
            {
                DependencyObject depObj = VisualTreeHelper.GetChild(element, i);
                if (null != depObj)
                {
                    if (depObj is T)
                        return depObj as T;
                    if (VisualTreeHelper.GetChildrenCount(depObj) <= 0) continue;
                    DependencyObject sub = GetChild<T>(depObj);
                    if (null != sub)
                    {
                        return sub as T;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取依赖对象的父元素(对Popup支持)
        /// </summary>
        /// <param name="element"></param>
        /// <param name="recurseIntoPopup"></param>
        /// <returns></returns>
        private static DependencyObject GetParent(DependencyObject element, bool recurseIntoPopup)
        {
            if (recurseIntoPopup)
            {
                // Case 126732 : To correctly detect parent of a popup we must do that exception case
                var popup = element as Popup;

                if ((popup != null) && (popup.PlacementTarget != null))
                    return popup.PlacementTarget;
            }

            var visual = element as Visual;
            DependencyObject parent = (visual == null) ? null : VisualTreeHelper.GetParent(visual);

            if (parent == null)
            {
                // No Visual parent. Check in the logical tree.
                var fe = element as FrameworkElement;

                if (fe != null)
                {
                    parent = fe.Parent;

                    if (parent == null)
                    {
                        parent = fe.TemplatedParent;
                    }
                }
                else
                {
                    var fce = element as FrameworkContentElement;

                    if (fce != null)
                    {
                        parent = fce.Parent;

                        if (parent == null)
                        {
                            parent = fce.TemplatedParent;
                        }
                    }
                }
            }

            return parent;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// 寻找父元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T FindAncestor<T>(this DependencyObject element) where T : DependencyObject
        {
            if (element == null)
                return null;

            DependencyObject parent = GetParent(element);

            if (parent == null)
                return null;

            if (parent is T)
                return parent as T;

            return parent.FindAncestor<T>();
        }

        /// <summary>
        /// 获取element控件的所有T类型的子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindAllChilds<T>(this DependencyObject element) where T : DependencyObject
        {
            IList<T> children = new List<T>();
            if (element == null)
            {
                return children;
            }

            FindChilds<T>(element, children);
            return children;
        }

        /// <summary>
        /// 获取子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T FindChild<T>(this DependencyObject element) where T : DependencyObject
        {
            if (element == null)
                return null;

            DependencyObject parent = GetChild<T>(element);

            if (parent == null)
                return null;

            return parent as T;
        }

        /// <summary>
        /// 判断元素的包含关系
        /// </summary>
        /// <param name="element">父元素</param>
        /// <param name="child">子元素</param>
        /// <returns></returns>
        public static bool ContainChild(DependencyObject element, DependencyObject child)
        {
            if (element == child)
            {
                return true;
            }
            if (element == null || child == null)
            {
                return false;
            }
            int index = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < index; i++)
            {
                DependencyObject depObj = VisualTreeHelper.GetChild(element, i);
                if (depObj == child || ContainChild(depObj, child))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if the specified element is a child of parent somewhere in the visual 
        /// tree. This method will work for Visual, FrameworkElement and FrameworkContentElement.
        /// </summary>
        /// <param name="element">子元素</param>
        /// <param name="parent">父元素</param>
        /// <returns></returns>
        public static bool IsDescendantOf(DependencyObject element, DependencyObject parent)
        {
            return IsDescendantOf(element, parent, true);
        }

        /// <summary>
        /// Returns true if the specified element is a child of parent somewhere in the visual 
        /// tree. This method will work for Visual, FrameworkElement and FrameworkContentElement.
        /// </summary>
        /// <param name="element">子元素</param>
        /// <param name="parent">父元素</param>
        /// <param name="recurseIntoPopup"></param>
        /// <returns></returns>
        public static bool IsDescendantOf(DependencyObject element, DependencyObject parent, bool recurseIntoPopup)
        {
            while (element != null)
            {
                if (Equals(element, parent))
                    return true;

                element = GetParent(element, recurseIntoPopup);
            }

            return false;
        }

        /// <summary>
        /// Retrieves all the visual children of a framework element.
        /// </summary>
        /// <param name="parent">The parent framework element.</param>
        /// <returns>The visual children of the framework element.</returns>
        internal static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject parent)
        {
            Debug.Assert(parent != null, "The parent cannot be null.");

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int counter = 0; counter < childCount; counter++)
            {
                yield return VisualTreeHelper.GetChild(parent, counter);
            }
        }

        /// <summary>
        /// Retrieves all the logical children of a framework element using a 
        /// breadth-first search.  A visual element is assumed to be a logical 
        /// child of another visual element if they are in the same namescope.
        /// For performance reasons this method manually manages the queue 
        /// instead of using recursion.
        /// </summary>
        /// <param name="parent">The parent framework element.</param>
        /// <returns>The logical children of the framework element.</returns>
        internal static IEnumerable<FrameworkElement> GetLogicalChildrenBreadthFirst(this FrameworkElement parent)
        {
            Debug.Assert(parent != null, "The parent cannot be null.");

            Queue<FrameworkElement> queue =
                new Queue<FrameworkElement>(parent.GetVisualChildren().OfType<FrameworkElement>());

            while (queue.Count > 0)
            {
                FrameworkElement element = queue.Dequeue();
                yield return element;

                foreach (FrameworkElement visualChild in element.GetVisualChildren().OfType<FrameworkElement>())
                {
                    queue.Enqueue(visualChild);
                }
            }
        }
        #endregion

    }
}
