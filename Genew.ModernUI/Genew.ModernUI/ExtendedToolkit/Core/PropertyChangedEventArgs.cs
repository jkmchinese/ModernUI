/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： PropertyChangedEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-11 23:50
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;

namespace Genew.ModernUI.ExtendedToolkit
{
    public class PropertyChangedEventArgs<T> : RoutedEventArgs
    {
        #region Constructors

        public PropertyChangedEventArgs(RoutedEvent Event, T oldValue, T newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
            RoutedEvent = Event;
        }

        #endregion

        #region NewValue Property

        private readonly T _newValue;

        public T NewValue
        {
            get { return _newValue; }
        }

        #endregion

        #region OldValue Property

        private readonly T _oldValue;

        public T OldValue
        {
            get { return _oldValue; }
        }

        #endregion

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            PropertyChangedEventHandler<T> handler = (PropertyChangedEventHandler<T>)genericHandler;
            handler(genericTarget, this);
        }
    }
}