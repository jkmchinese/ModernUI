/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： PopulatedEventHandler.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-30 23:06
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Diagnostics.CodeAnalysis;

namespace ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Represents the method that will handle the
    ///     <see cref="E:System.Windows.Controls.AutoCompleteBox.Populated" />
    ///     event of a <see cref="T:System.Windows.Controls.AutoCompleteBox" />
    ///     control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">
    ///     A
    ///     <see cref="T:System.Windows.Controls.PopulatedEventArgs" /> that
    ///     contains the event data.
    /// </param>
    /// <QualityBand>Stable</QualityBand>
    [SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances",
        Justification = "There is no generic RoutedEventHandler.")]
    public delegate void PopulatedEventHandler(object sender, PopulatedEventArgs e);
}