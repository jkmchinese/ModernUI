/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： PopulatedEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-30 23:06
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Collections;
using System.Windows;

namespace Genew.ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Provides data for the
    ///     <see cref="E:System.Windows.Controls.AutoCompleteBox.Populated" />
    ///     event.
    /// </summary>
    /// <QualityBand>Stable</QualityBand>
    public class PopulatedEventArgs : RoutedEventArgs
    {
        /// <summary>
        ///     Gets the list of possible matches added to the drop-down portion of
        ///     the <see cref="T:System.Windows.Controls.AutoCompleteBox" />
        ///     control.
        /// </summary>
        /// <value>
        ///     The list of possible matches added to the
        ///     <see cref="T:System.Windows.Controls.AutoCompleteBox" />.
        /// </value>
        public IEnumerable Data { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:System.Windows.Controls.PopulatedEventArgs" />.
        /// </summary>
        /// <param name="data">
        ///     The list of possible matches added to the
        ///     drop-down portion of the
        ///     <see cref="T:System.Windows.Controls.AutoCompleteBox" /> control.
        /// </param>
        public PopulatedEventArgs(IEnumerable data)
        {
            Data = data;
        }

#if !SILVERLIGHT
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:System.Windows.Controls.PopulatedEventArgs" />.
        /// </summary>
        /// <param name="data">
        ///     The list of possible matches added to the
        ///     drop-down portion of the
        ///     <see cref="T:System.Windows.Controls.AutoCompleteBox" /> control.
        /// </param>
        /// <param name="routedEvent">The routed event identifier for this instance.</param>
        public PopulatedEventArgs(IEnumerable data, RoutedEvent routedEvent)
            : base(routedEvent)
        {
            Data = data;
        }
#endif
    }
}