/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： SpinEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:45
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows;

namespace Genew.ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Provides data for the Spinner.Spin event.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    public class SpinEventArgs : RoutedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the SpinEventArgs class.
        /// </summary>
        /// <param name="direction">Spin direction.</param>
        public SpinEventArgs(SpinDirection direction)
        {
            Direction = direction;
        }

        public SpinEventArgs(SpinDirection direction, bool usingMouseWheel)
        {
            Direction = direction;
            UsingMouseWheel = usingMouseWheel;
        }

        /// <summary>
        ///     Gets the SpinDirection for the spin that has been initiated by the
        ///     end-user.
        /// </summary>
        public SpinDirection Direction { get; private set; }

        /// <summary>
        ///     Get or set whheter the spin event originated from a mouse wheel event.
        /// </summary>
        public bool UsingMouseWheel { get; private set; }
    }
}