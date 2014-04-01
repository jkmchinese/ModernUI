/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： PopulatingEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-30 23:06
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows;

namespace Genew.ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Provides data for the
    ///     <see cref="E:System.Windows.Controls.AutoCompleteBox.Populating" />
    ///     event.
    /// </summary>
    /// <QualityBand>Stable</QualityBand>
    public class PopulatingEventArgs : RoutedEventArgs
    {
        /// <summary>
        ///     Gets the text that is used to determine which items to display in
        ///     the <see cref="T:System.Windows.Controls.AutoCompleteBox" />
        ///     control.
        /// </summary>
        /// <value>
        ///     The text that is used to determine which items to display in
        ///     the <see cref="T:System.Windows.Controls.AutoCompleteBox" />.
        /// </value>
        public string Parameter { get; private set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the
        ///     <see cref="E:System.Windows.Controls.AutoCompleteBox.Populating" />
        ///     event should be canceled.
        /// </summary>
        /// <value>
        ///     True to cancel the event, otherwise false. The default is
        ///     false.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:System.Windows.Controls.PopulatingEventArgs" />.
        /// </summary>
        /// <param name="parameter">
        ///     The value of the
        ///     <see cref="P:System.Windows.Controls.AutoCompleteBox.SearchText" />
        ///     property, which is used to filter items for the
        ///     <see cref="T:System.Windows.Controls.AutoCompleteBox" /> control.
        /// </param>
        public PopulatingEventArgs(string parameter)
        {
            Parameter = parameter;
        }

#if !SILVERLIGHT
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:System.Windows.Controls.PopulatingEventArgs" />.
        /// </summary>
        /// <param name="parameter">
        ///     The value of the
        ///     <see cref="P:System.Windows.Controls.AutoCompleteBox.SearchText" />
        ///     property, which is used to filter items for the
        ///     <see cref="T:System.Windows.Controls.AutoCompleteBox" /> control.
        /// </param>
        /// <param name="routedEvent">The routed event identifier for this instance.</param>
        public PopulatingEventArgs(string parameter, RoutedEvent routedEvent)
            : base(routedEvent)
        {
            Parameter = parameter;
        }
#endif
    }
}