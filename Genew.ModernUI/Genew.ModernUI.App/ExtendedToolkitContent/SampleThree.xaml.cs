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
    public partial class SampleThree : UserControl
    {
        public SampleThree()
        {
            InitializeComponent();
        }

        private void SplitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ModernDialog.ShowMessage("Thanks for clicking me!", "SplitButton Click", MessageBoxButton.OK);
        }

        private void ButtonSpinner_Spin(object sender, SpinEventArgs e)
        {
            String[] names = (String[])this.Resources["Names"];

            ButtonSpinner spinner = (ButtonSpinner)sender;
            TextBox txtBox = (TextBox)spinner.Content;

            int value = Array.IndexOf(names, txtBox.Text);
            if (e.Direction == SpinDirection.Increase)
                value++;
            else
                value--;

            if (value < 0)
                value = names.Length - 1;
            else if (value >= names.Length)
                value = 0;

            txtBox.Text = names[value];
        }
    }
}
