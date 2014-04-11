using System;
using System.Collections.Generic;
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
using Genew.ModernUI.App.Common.SampleDatas;
using Genew.ModernUI.ExtendedToolkit;

namespace Genew.ModernUI.App.ExtendedToolkitContent.AutoCompletion
{
    /// <summary>
    /// Interaction logic for AutoCompleteLambda.xaml
    /// </summary>
    public partial class CustomEvents : UserControl
    {
        public CustomEvents()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Airports_Loaded);
        }

        /// <summary>
        /// Handle the Loaded event of the page.
        /// </summary>
        /// <param name="sender">The source object.</param>
        /// <param name="e">The event arguments.</param>
        private void Airports_Loaded(object sender, RoutedEventArgs e)
        {
            NowAutoComplete.Populating += OnPopulatingSynchronous;
            NowAutoComplete2.Populating += OnPopulatingSynchronous;
            LaterAutoComplete.Populating += OnPopulatingAsynchronous;
            LaterAutoComplete2.Populating += OnPopulatingAsynchronous;
        }

        /// <summary>
        /// The Populating event handler.
        /// </summary>
        /// <param name="sender">The source object.</param>
        /// <param name="e">The event data.</param>
        private void OnPopulatingSynchronous(object sender, PopulatingEventArgs e)
        {
            AutoCompleteBox source = (AutoCompleteBox)sender;

            source.ItemsSource = new string[]
            {
                e.Parameter + "1",
                e.Parameter + "2",
                e.Parameter + "3",
            };
        }

        /// <summary>
        /// The populating handler.
        /// </summary>
        /// <param name="sender">The source object.</param>
        /// <param name="e">The event data.</param>
        private void OnPopulatingAsynchronous(object sender, PopulatingEventArgs e)
        {
            AutoCompleteBox source = (AutoCompleteBox)sender;

            // Cancel the populating value: this will allow us to call 
            // PopulateComplete as necessary.
            e.Cancel = true;

            // Use the dispatcher to simulate an asynchronous callback when 
            // data becomes available
            Dispatcher.BeginInvoke(
                new Action(delegate()
                {
                    source.ItemsSource = new string[]
                    {
                        e.Parameter + "1",
                        e.Parameter + "2",
                        e.Parameter + "3",
                    };

                    // Population is complete
                    source.PopulateComplete();
                }));
        }
    }
}
