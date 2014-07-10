/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： AccordionItemWrapperAutomationPeer.cs
* 作   者： chenzhifen
* 创建日期： 2014-05-11 10:39
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows.Automation.Peers;
using Genew.ModernUI.ExtendedToolkit;

namespace Genew.ModernUI.Automation.Peers
{
    /// <summary>
    ///     Wraps an <see cref="T:System.Windows.Controls.AccordionItem" />.
    /// </summary>
    public class AccordionItemWrapperAutomationPeer : FrameworkElementAutomationPeer
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="item">The <see cref="T:System.Windows.Controls.AccordionItem" /> to wrap.</param>
        public AccordionItemWrapperAutomationPeer(AccordionItem item)
            : base(item)
        {
        }
    }
}