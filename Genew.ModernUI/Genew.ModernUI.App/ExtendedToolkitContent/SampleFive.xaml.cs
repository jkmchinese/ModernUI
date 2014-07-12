using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Samples;
using ModernUI.Windows.Controls;

namespace ModernUI.App.ExtendedToolkitContent
{
    /// <summary>
    /// Interaction logic for SampleThree.xaml
    /// </summary>
    public partial class SampleFive : UserControl
    {
        //private ModernWindow m_window;

        public SampleFive()
        {
            InitializeComponent();
            Loaded += SampleFive_Loaded;
        }

        void SampleFive_Loaded(object sender, RoutedEventArgs e)
        {
            // Words
            WordComplete.ItemsSource = Words.GetAliceInWonderland();
            SetPrefixLength.ValueChanged += (s, args) => WordComplete.MinimumPrefixLength = (int)Math.Floor(SetPrefixLength.Value);
        }
    }
}
