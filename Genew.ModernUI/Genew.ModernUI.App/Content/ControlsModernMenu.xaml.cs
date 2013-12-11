using Genew.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Genew.ModernUI.App.Content
{
    /// <summary>
    /// Interaction logic for ControlsModernMenu.xaml
    /// </summary>
    public partial class ControlsModernMenu : UserControl
    {
        private int m_groupId = 2;
        private int m_linkId = 5;

        public ControlsModernMenu()
        {
            InitializeComponent();

            // add group command
            this.AddGroup.Command = new RelayCommand(o => this.Menu.LinkGroups.Add(new LinkGroup
            {
                DisplayName = string.Format(CultureInfo.InvariantCulture, "group {0}", ++m_groupId)
            }));

            // add link to selected group command
            this.AddLink.Command = new RelayCommand(o => this.Menu.SelectedLinkGroup.Links.Add(new Link
            {
                DisplayName = string.Format(CultureInfo.InvariantCulture, "link {0}", ++m_linkId),
                Source = new Uri(string.Format(CultureInfo.InvariantCulture, "/link{0}", m_linkId), UriKind.Relative)
            }), o => this.Menu.SelectedLinkGroup != null);

            // remove selected group command
            this.RemoveGroup.Command = new RelayCommand(o => this.Menu.LinkGroups.Remove(this.Menu.SelectedLinkGroup),
                o => this.Menu.SelectedLinkGroup != null);

            // remove selected linkcommand
            this.RemoveLink.Command = new RelayCommand(o => this.Menu.SelectedLinkGroup.Links.Remove(this.Menu.SelectedLink),
                o => this.Menu.SelectedLinkGroup != null && this.Menu.SelectedLink != null);

            // log SourceChanged events
            this.Menu.SelectedSourceChanged += (o, e) => Debug.WriteLine("SelectedSourceChanged: {0}", e.Source);
        }
    }
}
