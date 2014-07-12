
/*******************************************************************
 * * 版权所有： 深圳震有科技有限公司
 * * 文件名称： 
 * * 作    者： 刘剑
 * * 创建日期： 2012-6-14 10:22:33
 * * 文件版本： 1.0.0.0
 * * 修改时间:    修改人:    修改内容:
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ModernUI.Behaviors
{
    /// <summary>
    /// 按钮点击行为(暂时不知道有啥用)
    /// </summary>
    public class ButtonBaseMouseBehavior : MouseBehavior
    {
        protected override void BindEvent(FrameworkElement frameWork)
        {
            if (frameWork is FrameworkElement)
            {
                FrameworkElement control = frameWork as FrameworkElement;
                control.PreviewMouseLeftButtonDown += base.obj_MouseLeftButtonDown;
                control.PreviewMouseLeftButtonUp += base.obj_MouseLeftButtonUp;
            }
        }
        protected override void UnBindEvent(FrameworkElement frameWork)
        {
            if (frameWork is FrameworkElement)
            {
                FrameworkElement control = frameWork as FrameworkElement;
                control.PreviewMouseLeftButtonDown -= base.obj_MouseLeftButtonDown;
                control.PreviewMouseLeftButtonUp -= base.obj_MouseLeftButtonUp;
            }
        }
    }
}
