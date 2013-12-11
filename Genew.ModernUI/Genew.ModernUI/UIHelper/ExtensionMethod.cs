/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ExtensionMethod.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-11 23:58:29
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Genew.ModernUI.UIHelper
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
        /// <returns></returns>//created by xiapengcheng at 2013-6-18 19:56:55
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
        #endregion

    }
}
