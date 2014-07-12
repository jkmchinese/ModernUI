/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： AccordionItemAutomationPeer.cs
* 作   者： chenzhifen
* 创建日期： 2014-05-11 10:39
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using ModernUI.ExtendedToolkit;

namespace ModernUI.Automation.Peers
{
    /// <summary>
    ///     Exposes AccordionItem types to UI Automation.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
#if SILVERLIGHT
    public class AccordionItemAutomationPeer : FrameworkElementAutomationPeer, IExpandCollapseProvider, ISelectionItemProvider
#else
    public class AccordionItemAutomationPeer : ItemAutomationPeer, IExpandCollapseProvider, ISelectionItemProvider
#endif
    {
        /// <summary>
        ///     Gets the AccordionItem that owns this AccordionItemAutomationPeer.
        /// </summary>
        private AccordionItem OwnerAccordionItem
        {
#if SILVERLIGHT
            get { return (AccordionItem)Owner; }
#else
            get { return base.Item as AccordionItem; }
#endif
        }

#if SILVERLIGHT
    /// <summary>
    /// Initializes a new instance of the AccordionAutomationPeer class.
    /// </summary>
    /// <param name="owner">
    /// The Accordion that is associated with this
    /// AccordionAutomationPeer.
    /// </param>
        public AccordionItemAutomationPeer(AccordionItem owner)
            : base(owner)
        {
        }
#else
        /// <summary>
        ///     Initializes a new instance of the AccordionAutomationPeer class.
        /// </summary>
        /// <param name="item">
        ///     The item associated with this AutomationPeer
        /// </param>
        /// <param name="itemsControlAutomationPeer">
        ///     The Accordion that is associated with this item.
        /// </param>
        public AccordionItemAutomationPeer(object item, ItemsControlAutomationPeer itemsControlAutomationPeer)
            : base(item, itemsControlAutomationPeer)
        {
        }
#endif

        /// <summary>
        ///     Gets the control type for the AccordionItem that is associated
        ///     with this AccordionItemAutomationPeer.  This method is called by
        ///     GetAutomationControlType.
        /// </summary>
        /// <returns>Custom AutomationControlType.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.ListItem;
        }

        /// <summary>
        ///     Gets the name of the AccordionItem that is associated with this
        ///     AccordionItemAutomationPeer.  This method is called by GetClassName.
        /// </summary>
        /// <returns>The name AccordionItem.</returns>
        protected override string GetClassNameCore()
        {
            return "AccordionItem";
        }

        /// <summary>
        ///     Gets the control pattern for the AccordionItem that is associated
        ///     with this AccordionItemAutomationPeer.
        /// </summary>
        /// <param name="patternInterface">The desired PatternInterface.</param>
        /// <returns>The desired AutomationPeer or null.</returns>
        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.ExpandCollapse ||
                patternInterface == PatternInterface.SelectionItem)
            {
                return this;
            }

            return null;
        }

        /// <summary>
        ///     Gets the state (expanded or collapsed) of the Accordion.
        /// </summary>
        /// <remarks>
        ///     This API supports the .NET Framework infrastructure and is not
        ///     intended to be used directly from your code.
        /// </remarks>
        ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
        {
            get { return OwnerAccordionItem.IsSelected ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed; }
        }

        /// <summary>
        ///     Collapses the AccordionItem.
        /// </summary>
        /// <remarks>
        ///     This API supports the .NET Framework infrastructure and is not
        ///     intended to be used directly from your code.
        /// </remarks>
        void IExpandCollapseProvider.Collapse()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            AccordionItem owner = OwnerAccordionItem;
            if (owner.IsLocked)
            {
                throw new InvalidOperationException("Automation_OperationCannotBePerformed");
            }

            owner.IsSelected = false;
        }

        /// <summary>
        ///     Expands the AccordionItem.
        /// </summary>
        /// <remarks>
        ///     This API supports the .NET Framework infrastructure and is not
        ///     intended to be used directly from your code.
        /// </remarks>
        void IExpandCollapseProvider.Expand()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            AccordionItem owner = OwnerAccordionItem;
            if (owner.IsLocked)
            {
                throw new InvalidOperationException("Automation_OperationCannotBePerformed");
            }

            owner.IsSelected = true;
        }

        /// <summary>
        ///     Adds the AccordionItem to the collection of selected items.
        /// </summary>
        /// <remarks>
        ///     This API supports the .NET Framework infrastructure and is not
        ///     intended to be used directly from your code.
        /// </remarks>
        void ISelectionItemProvider.AddToSelection()
        {
            AccordionItem owner = OwnerAccordionItem;
            Accordion parent = owner.ParentAccordion;
            if (parent == null)
            {
                throw new InvalidOperationException("Automation_OperationCannotBePerformed");
            }
            parent.SelectedItems.Add(owner);
        }

        /// <summary>
        ///     Gets a value indicating whether the Accordion is selected.
        /// </summary>
        /// <remarks>
        ///     This API supports the .NET Framework infrastructure and is not
        ///     intended to be used directly from your code.
        /// </remarks>
        bool ISelectionItemProvider.IsSelected
        {
            get { return OwnerAccordionItem.IsSelected; }
        }

        /// <summary>
        ///     Removes the current Accordion from the collection of selected
        ///     items.
        /// </summary>
        /// <remarks>
        ///     This API supports the .NET Framework infrastructure and is not
        ///     intended to be used directly from your code.
        /// </remarks>
        void ISelectionItemProvider.RemoveFromSelection()
        {
            AccordionItem owner = OwnerAccordionItem;
            Accordion parent = owner.ParentAccordion;
            if (parent == null)
            {
                throw new InvalidOperationException("Automation_OperationCannotBePerformed");
            }
            parent.SelectedItems.Remove(owner);
        }

        /// <summary>
        ///     Clears selection from currently selected items and then proceeds to
        ///     select the current Accordion.
        /// </summary>
        /// <remarks>
        ///     This API supports the .NET Framework infrastructure and is not
        ///     intended to be used directly from your code.
        /// </remarks>
        void ISelectionItemProvider.Select()
        {
            OwnerAccordionItem.IsSelected = true;
        }

        /// <summary>
        ///     Gets the UI Automation provider that implements ISelectionProvider
        ///     and acts as the container for the calling object.
        /// </summary>
        /// <remarks>
        ///     This API supports the .NET Framework infrastructure and is not
        ///     intended to be used directly from your code.
        /// </remarks>
        IRawElementProviderSimple ISelectionItemProvider.SelectionContainer
        {
            get
            {
                Accordion parent = OwnerAccordionItem.ParentAccordion;
                if (parent != null)
                {
#if SILVERLIGHT
                    AutomationPeer peer = FromElement(parent);
#else
                    AutomationPeer peer = UIElementAutomationPeer.FromElement(parent);
#endif
                    if (peer != null)
                    {
                        return ProviderFromPeer(peer);
                    }
                }
                return null;
            }
        }
    }
}