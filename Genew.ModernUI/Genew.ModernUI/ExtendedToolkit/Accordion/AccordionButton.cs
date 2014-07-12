/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： AccordionButton.cs
* 作   者： chenzhifen
* 创建日期： 2014-05-11 10:02
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Represents the header for an accordion item.
    /// </summary>
    /// <remarks>
    ///     By creating a seperate control, there is more flexibility in
    ///     the templating possibilities.
    /// </remarks>
    /// <QualityBand>Preview</QualityBand>
    [TemplateVisualState(Name = VisualStates.StateNormal, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StateMouseOver, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StatePressed, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StateDisabled, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StateFocused, GroupName = VisualStates.GroupFocus)]
    [TemplateVisualState(Name = VisualStates.StateUnfocused, GroupName = VisualStates.GroupFocus)]
    [TemplateVisualState(Name = VisualStates.StateExpanded, GroupName = VisualStates.GroupExpansion)]
    [TemplateVisualState(Name = VisualStates.StateCollapsed, GroupName = VisualStates.GroupExpansion)]
    [TemplateVisualState(Name = VisualStates.StateExpandDown, GroupName = VisualStates.GroupExpandDirection)]
    [TemplateVisualState(Name = VisualStates.StateExpandUp, GroupName = VisualStates.GroupExpandDirection)]
    [TemplateVisualState(Name = VisualStates.StateExpandLeft, GroupName = VisualStates.GroupExpandDirection)]
    [TemplateVisualState(Name = VisualStates.StateExpandRight, GroupName = VisualStates.GroupExpandDirection)]
    public class AccordionButton : ToggleButton
    {
        #region Parent AccordionItem

        /// <summary>
        ///     Gets or sets a reference to the parent AccordionItem
        ///     of an AccordionButton.
        /// </summary>
        /// <value>The parent accordion item.</value>
        internal AccordionItem ParentAccordionItem { get; set; }

        #endregion Parent AccordionItem

#if !SILVERLIGHT
        /// <summary>
        ///     Static constructor
        /// </summary>
        static AccordionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AccordionButton),
                new FrameworkPropertyMetadata(typeof(AccordionButton)));
        }
#endif

        /// <summary>
        ///     Updates the state of the visual.
        /// </summary>
        /// <param name="useTransitions">If set to <c>true</c> use transitions.</param>
        /// <remarks>The header will follow the parent accordionitem states.</remarks>
        internal virtual void UpdateVisualState(bool useTransitions)
        {
            // the visualstate of the header is completely dependent on the parent state.
            if (ParentAccordionItem == null)
            {
                return;
            }

            if (ParentAccordionItem.IsSelected)
            {
                VisualStates.GoToState(this, useTransitions, VisualStates.StateExpanded);
            }
            else
            {
                VisualStates.GoToState(this, useTransitions, VisualStates.StateCollapsed);
            }

            switch (ParentAccordionItem.ExpandDirection)
            {
                // no animations on an expanddirection change.
                case ExpandDirection.Down:
                    VisualStates.GoToState(this, false, VisualStates.StateExpandDown);
                    break;

                case ExpandDirection.Up:
                    VisualStates.GoToState(this, false, VisualStates.StateExpandUp);
                    break;

                case ExpandDirection.Left:
                    VisualStates.GoToState(this, false, VisualStates.StateExpandLeft);
                    break;

                default:
                    VisualStates.GoToState(this, false, VisualStates.StateExpandRight);
                    break;
            }
        }
    }
}