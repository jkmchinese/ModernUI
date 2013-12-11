using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genew.ModernUI.Presentation
{
    /// <summary>
    /// Represents a named group of links.
    /// </summary>
    public class LinkGroup
        : Displayable
    {
        private string m_groupName;
        private Link m_selectedLink;
        private readonly LinkCollection m_links = new LinkCollection();

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        public string GroupName
        {
            get { return this.m_groupName; }
            set
            {
                if (this.m_groupName != value)
                {
                    this.m_groupName = value;
                    OnPropertyChanged("GroupName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected link in this group.
        /// </summary>
        /// <value>The selected link.</value>
        internal Link SelectedLink
        {
            get { return this.m_selectedLink; }
            set
            {
                if (this.m_selectedLink != value)
                {
                    this.m_selectedLink = value;
                    OnPropertyChanged("SelectedLink");
                }
            }
        }

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <value>The links.</value>
        public LinkCollection Links
        {
            get { return this.m_links; }
        }
    }
}
