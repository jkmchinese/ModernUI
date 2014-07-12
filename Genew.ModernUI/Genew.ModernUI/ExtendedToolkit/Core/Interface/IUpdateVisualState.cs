/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： IUpdateVisualState.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-30 22:20
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Diagnostics.CodeAnalysis;

namespace ModernUI.ExtendedToolkit.Interface
{
    /// <summary>
    /// The IUpdateVisualState interface is used to provide the
    /// InteractionHelper with access to the type's UpdateVisualState method.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic", Justification = "This is not an exception class.")]
    internal interface IUpdateVisualState
    {
        /// <summary>
        /// Update the visual state of the control.
        /// </summary>
        /// <param name="useTransitions">
        /// A value indicating whether to automatically generate transitions to
        /// the new state, or instantly transition to the new state.
        /// </param>
        void UpdateVisualState(bool useTransitions);
    }
}