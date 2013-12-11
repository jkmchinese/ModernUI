/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： TextSelectAllBehavior.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-11 23:31:47
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Controls;

namespace Genew.ModernUI.Behaviors
{
    /// <summary>
    /// TextBox获得焦点时全选
    /// </summary>
    [Obsolete]
    public sealed class TextSelectAllBehavior : Behavior<DependencyObject>
    {
        protected override void OnAttached()
        {
            if (AssociatedObject is TextBox)
            {
                var obj = AssociatedObject as FrameworkElement;
                obj.GotFocus += new RoutedEventHandler(obj_GotFocus);
            }
        }

        private void obj_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject is TextBox)
            {
                var txtBox = AssociatedObject as TextBox;
                if (!string.IsNullOrEmpty(txtBox.Text))
                {
                    txtBox.SelectAll();
                }
            }
        }
    }
}
