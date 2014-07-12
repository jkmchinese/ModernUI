using System;
using System.Collections.Generic;
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
using ModernUI.ExtendedToolkit;
using ModernUI.Windows.Controls;

namespace ModernUI.App.ExtendedToolkitContent
{
    /// <summary>
    /// Interaction logic for SampleThree.xaml
    /// </summary>
    public partial class SampleFour : UserControl
    {
        private ModernWindow m_window;

        public SampleFour()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            var wizard = Resources["Wizard"] as Wizard;
            if (wizard != null)
            {
                wizard.CurrentPage = wizard.Items[0] as WizardPage;
                wizard.Margin = new Thickness(0, 32, 0, 0);

                if (m_window != null)
                {
                    m_window.Content = null;
                    m_window = null;
                }
                m_window = new ModernWindow
                {
                    Style = (Style)App.Current.Resources["EmptyWindow"],
                    Title = "Wizard demonstration",
                    Content = wizard,
                    Width = 600,
                    Height = 400,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                // Window will be closed by Wizard because FinishButtonClosesWindow = true and CancelButtonClosesWindow = true
                m_window.ShowDialog();
            }
        }

        private void OnWizardHelp(object sender, EventArgs e)
        {
            ModernDialog.ShowMessage("This is the Help for the Wizard\n\n\n\n\n", "Wizard Help", MessageBoxButton.OK);
        }
    }
}
