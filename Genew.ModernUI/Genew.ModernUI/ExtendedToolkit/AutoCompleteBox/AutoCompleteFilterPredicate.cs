/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： AutoCompleteFilterPredicate.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-30 22:57
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

namespace ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Represents the filter used by the
    ///     <see cref="T:System.Windows.Controls.AutoCompleteBox" /> control to
    ///     determine whether an item is a possible match for the specified text.
    /// </summary>
    /// <returns>
    ///     true to indicate <paramref name="item" /> is a possible match
    ///     for <paramref name="search" />; otherwise false.
    /// </returns>
    /// <param name="search">The string used as the basis for filtering.</param>
    /// <param name="item">
    ///     The item that is compared with the
    ///     <paramref name="search" /> parameter.
    /// </param>
    /// <typeparam name="T">
    ///     The type used for filtering the
    ///     <see cref="T:System.Windows.Controls.AutoCompleteBox" />. This type can
    ///     be either a string or an object.
    /// </typeparam>
    /// <QualityBand>Stable</QualityBand>
    public delegate bool AutoCompleteFilterPredicate<T>(string search, T item);
}