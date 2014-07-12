/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： DataTriggerEvaluateOnLoad.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-12 00:10:14
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows;

namespace ModernUI.Behaviors
{
    /// <summary>
    /// 修复系统的DataTrigger无法在初次加载时进行状态计算的bug
    /// </summary>
    public class DataTriggerEvaluateOnLoad : Microsoft.Expression.Interactivity.Core.DataTrigger
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            var element = AssociatedObject as FrameworkElement;
            if (element != null)
            {
                element.Loaded += OnElementLoaded;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            var element = AssociatedObject as FrameworkElement;
            if (element != null)
            {
                element.Loaded -= OnElementLoaded;
            }
        }

        private void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            EvaluateBindingChange(null);
        }
    }
}
