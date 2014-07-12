using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernUI.Presentation
{
    /// <summary>
    /// Represents a displayable link.
    /// </summary>
    public class Link
        : Displayable
    {
        private Uri m_source;

        /// <summary>
        /// Gets or sets the source uri.
        /// </summary>
        /// <value>The source.</value>
        public Uri Source
        {
            get { return this.m_source; }
            set
            {
                if (this.m_source != value)
                {
                    this.m_source = value;
                    OnPropertyChanged("Source");
                }
            }
        }
    }
}
