using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genew.ModernUI.Presentation
{
    /// <summary>
    /// Provides a base implementation for objects that are displayed in the UI.
    /// </summary>
    public abstract class Displayable
        : NotifyPropertyChanged
    {
        private string m_displayName;

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get { return this.m_displayName; }
            set
            {
                if (this.m_displayName != value)
                {
                    this.m_displayName = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }
    }
}
