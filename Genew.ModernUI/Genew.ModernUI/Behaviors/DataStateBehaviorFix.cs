/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： DataStateBehaviorFix.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-12 00:10:52
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Linq;
using Microsoft.Expression.Interactivity.Core;
using System.Windows;

namespace Genew.ModernUI.Behaviors
{
    /// <summary>
    /// 修复系统的DataStateBehavior无法在初次加载时进行状态计算的bug
    /// </summary>
    public class DataStateBehaviorFix : DataStateBehavior
    {
        public bool UseTransitionsOnLoad { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Evaluate();
        }

        private void Evaluate()
        {
            if (Value == null)
            {
                GotoState(Binding == null, this.AssociatedObject);
            }
            else GotoState(Value.Equals(Binding), this.AssociatedObject);
        }


        private void GotoState(bool flag, FrameworkElement element)
        {
            string stateName = flag ? TrueState : FalseState;

            if (HasState(element, stateName))
            {
                bool ret = System.Windows.VisualStateManager.GoToElementState(element, stateName, UseTransitionsOnLoad);
            }
            else if (element.Parent as FrameworkElement != null)
                GotoState(flag, element.Parent as FrameworkElement);
        }

        private bool HasState(FrameworkElement element, string stateName)
        {
            var groups = Microsoft.Expression.Interactivity.VisualStateUtilities.GetVisualStateGroups(element).Cast<VisualStateGroup>();

            return groups.Any(p => p.States.Cast<VisualState>().Any(s => s.Name == stateName));
        }
    }

}
