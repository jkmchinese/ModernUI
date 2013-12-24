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
using Genew.ModernUI.Windows.Controls;

namespace Genew.ModernUI.App.ExtendedToolkitContent
{
    /// <summary>
    /// Interaction logic for SampleOne.xaml
    /// </summary>
    public partial class SampleOne : UserControl
    {
        public SampleOne()
        {
            InitializeComponent();
        }

        private void SplitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ModernDialog.ShowMessage("Thanks for clicking me!", "SplitButton Click", MessageBoxButton.OK);
        }
    }
}
