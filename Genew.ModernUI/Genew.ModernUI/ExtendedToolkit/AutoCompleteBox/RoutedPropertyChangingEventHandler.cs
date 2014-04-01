/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： RoutedPropertyChangingEventHandler.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-30 23:06
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

namespace Genew.ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Represents methods that handle various routed events that track property
    ///     values changing.  Typically the events denote a cancellable action.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the value for the dependency property that is changing.
    /// </typeparam>
    /// <param name="sender">
    ///     The object where the initiating property is changing.
    /// </param>
    /// <param name="e">Event data for the event.</param>
    /// <QualityBand>Preview</QualityBand>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances",
        Justification = "To match pattern of RoutedPropertyChangedEventHandler<T>")]
    public delegate void RoutedPropertyChangingEventHandler<T>(object sender, RoutedPropertyChangingEventArgs<T> e);
}