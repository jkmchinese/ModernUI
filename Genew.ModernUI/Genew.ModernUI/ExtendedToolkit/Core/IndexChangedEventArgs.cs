/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： IndexChangedEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-11 23:57
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;

namespace Genew.ModernUI.ExtendedToolkit
{
    public class IndexChangedEventArgs : PropertyChangedEventArgs<int>
    {
        #region Constructors

        public IndexChangedEventArgs(RoutedEvent routedEvent, int oldIndex, int newIndex)
            : base(routedEvent, oldIndex, newIndex)
        {
        }

        #endregion

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            ((IndexChangedEventHandler)genericHandler)(genericTarget, this);
        }
    }
}