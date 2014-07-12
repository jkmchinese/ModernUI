/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： CancelRoutedEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-29 23:52
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows;

namespace ModernUI.ExtendedToolkit
{
    public delegate void CancelRoutedEventHandler(object sender, CancelRoutedEventArgs e);

    /// <summary>
    ///     An event data class that allows to inform the sender that the handler wants to cancel
    ///     the ongoing action.
    ///     The handler can set the "Cancel" property to false to cancel the action.
    /// </summary>
    public class CancelRoutedEventArgs : RoutedEventArgs
    {
        public CancelRoutedEventArgs()
        {
        }

        public CancelRoutedEventArgs(RoutedEvent routedEvent)
            : base(routedEvent)
        {
        }

        public CancelRoutedEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source)
        {
        }

        #region Cancel Property

        public bool Cancel { get; set; }

        #endregion Cancel Property
    }
}