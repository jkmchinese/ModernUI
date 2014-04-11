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

namespace Genew.ModernUI.App.ExtendedToolkitContent.AutoCompletion
{
    /// <summary>
    /// Interaction logic for AutoCompleteLambda.xaml
    /// </summary>
    public partial class AutoCompleteLambda : UserControl
    {
        public AutoCompleteLambda()
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
            // Provide airport data and a custom filter
            ObjectCollection airports = Airport.SampleAirports;
            DepartureAirport.ItemsSource = airports;
            ArrivalAirport.ItemsSource = airports;
            DepartureAirport.ItemFilter = (search, item) =>
            {
                Airport airport = item as Airport;
                if (airport != null)
                {
                    // Interested in: Name, City, FAA code
                    string filter = search.ToUpper(CultureInfo.InvariantCulture);
                    return (airport.CodeFaa.ToUpper(CultureInfo.InvariantCulture).Contains(filter)
                        || airport.City.ToUpper(CultureInfo.InvariantCulture).Contains(filter)
                        || airport.Name.ToUpper(CultureInfo.InvariantCulture).Contains(filter));
                }

                return false;
            };
            ArrivalAirport.ItemFilter = DepartureAirport.ItemFilter;
        }
    }
}
