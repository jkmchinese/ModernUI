/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： AccordionSelectionMode.cs
* 作   者： chenzhifen
* 创建日期： 2014-05-11 10:02
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

namespace ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Defines the minimum and maximum number of selected items allowed in an Accordion control.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    public enum AccordionSelectionMode
    {
        /// <summary>
        ///     Exactly one item must be selected in the Accordion.
        /// </summary>
        One,

        /// <summary>
        ///     At least one item must be selected in the Accordion.
        /// </summary>
        OneOrMore,

        /// <summary>
        ///     No more than one item can be selected in the accordion.
        /// </summary>
        ZeroOrOne,

        /// <summary>
        ///     Any number of  items can be selected in the Accordion.
        /// </summary>
        ZeroOrMore
    }
}