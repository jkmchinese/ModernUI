/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ZoomboxViewChangedEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-11 23:43
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace ModernUI.ExtendedToolkit
{
    public class ZoomboxViewChangedEventArgs : PropertyChangedEventArgs<ZoomboxView>
    {
        #region Constructors

        public ZoomboxViewChangedEventArgs(
            ZoomboxView oldView,
            ZoomboxView newView,
            int oldViewStackIndex,
            int newViewStackIndex)
            : base(Zoombox.CurrentViewChangedEvent, oldView, newView)
        {
            _newViewStackIndex = newViewStackIndex;
            _oldViewStackIndex = oldViewStackIndex;
        }

        #endregion

        #region NewViewStackIndex Property

        private readonly int _newViewStackIndex = -1;

        public int NewViewStackIndex
        {
            get { return _newViewStackIndex; }
        }

        #endregion

        #region NewViewStackIndex Property

        private readonly int _oldViewStackIndex = -1;

        public int OldViewStackIndex
        {
            get { return _oldViewStackIndex; }
        }

        #endregion

        #region NewViewStackIndex Property

        public bool IsNewViewFromStack
        {
            get { return _newViewStackIndex >= 0; }
        }

        #endregion

        #region NewViewStackIndex Property

        public bool IsOldViewFromStack
        {
            get { return _oldViewStackIndex >= 0; }
        }

        #endregion

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            ((ZoomboxViewChangedEventHandler)genericHandler)(genericTarget, this);
        }
    }
}