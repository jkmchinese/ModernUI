using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for SampleEight.xaml
    /// </summary>
    public partial class SampleEight : UserControl
    {
        public SampleEight()
        {
            InitializeComponent();
            this.DataContext = new ExamplesViewModel();
        }
    }

    /// <summary>
    /// ExamplesViewModel.
    /// </summary>
    public class ExamplesViewModel : INotifyPropertyChanged
    {
        private IQueryable<MyBusinessObject> randomProducts;
        /// <summary>
        /// Gets the random products.
        /// </summary>
        /// <value>The random products.</value>
        public IQueryable<MyBusinessObject> RandomProducts
        {
            get
            {
                if (this.randomProducts == null)
                {
                    this.randomProducts = new MyBusinessObjects().GetData(5000).AsQueryable();
                }

                return this.randomProducts;
            }
        }

        ObservableCollection<MyBusinessObject> view;
        public ObservableCollection<MyBusinessObject> View
        {
            get
            {
                if (object.Equals(view, null))
                {
                    var data = RandomProducts
                        .Where(i => i.Name.ToLower().Contains(SearchValue.ToLower()))
                        //.Sort(new SortDescriptor[] { new SortDescriptor() { Member = SelectedProperty } })
                        .Cast<MyBusinessObject>();

                    view = new ObservableCollection<MyBusinessObject>(data);
                }
                return view;
            }
            private set
            {
                if (!object.Equals(view, value))
                {
                    view = value;

                    OnPropertyChanged("View");
                }
            }
        }

        IEnumerable<string> properties;
        public IEnumerable<string> Properties
        {
            get
            {
                if (properties == null)
                {
                    properties = from p in typeof(MyBusinessObject).GetProperties()
                                 select p.Name;

                    selectedProperty = properties.FirstOrDefault();
                }

                return properties;
            }
        }

        string selectedProperty;
        public string SelectedProperty
        {
            get
            {
                return selectedProperty;
            }
            set
            {
                if (selectedProperty != value)
                {
                    selectedProperty = value;

                    View = null;

                    OnPropertyChanged("SelectedProperty");
                }
            }
        }

        string searchValue = string.Empty;
        public string SearchValue
        {
            get
            {
                return searchValue;
            }
            set
            {
                if (searchValue != value)
                {
                    searchValue = value;

                    View = null;

                    OnPropertyChanged("SearchValue");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

    /// <summary>
    /// MyBusinessObjects.
    /// </summary>
    public class MyBusinessObjects
    {
        string[] names = new string[] { "Côte de Blaye", "Boston Crab Meat", 
            "Singaporean Hokkien Fried Mee", "Gula Malacca", "Rogede sild", 
            "Spegesild", "Zaanse koeken", "Chocolade", "Maxilaku", "Valkoinen suklaa" };
        double[] prizes = new double[] { 23.2500, 9.0000, 45.6000, 32.0000, 
            14.0000, 19.0000, 263.5000, 18.4000, 3.0000, 14.0000 };
        DateTime[] dates = new DateTime[] { new DateTime(2007, 5, 10), new DateTime(2008, 9, 13), 
            new DateTime(2008, 2, 22), new DateTime(2009, 1, 2), new DateTime(2007, 4, 13), 
            new DateTime(2008, 5, 12), new DateTime(2008, 1, 19), new DateTime(2008, 8, 26), 
            new DateTime(2008, 7, 31), new DateTime(2007, 7, 16) };
        bool[] bools = new bool[] { true, false, true, false, true, false, true, false, true, false };

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="maxItems">The max items.</param>
        /// <returns></returns>
        public IEnumerable<MyBusinessObject> GetData(int maxItems)
        {
            Random rnd = new Random();

            return from i in Enumerable.Range(1, maxItems)
                   select new MyBusinessObject(i
                       , names[rnd.Next(9)]
                       , prizes[rnd.Next(9)]
                       , dates[rnd.Next(9)]
                       , bools[rnd.Next(9)]);
        }

        /// <summary>
        /// Generates the random business object.
        /// </summary>
        /// <param name="random">The random.</param>
        /// <returns></returns>
        public MyBusinessObject GenerateRandomBusinessObject(Random random)
        {
            return new MyBusinessObject(random.Next(0, 100)
                , names[random.Next(9)]
                , prizes[random.Next(9)]
                , dates[random.Next(9)]
                , bools[random.Next(9)]);
        }
    }

    /// <summary>
    /// MyBusinessObject.
    /// </summary>
    public class MyBusinessObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private string name;
        private double unitPrice;
        private DateTime date;
        private bool discontinued;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyBusinessObject"/> class.
        /// </summary>
        public MyBusinessObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyBusinessObject"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="unitPrice">The unit price.</param>
        /// <param name="date">The date.</param>
        /// <param name="discontinued">if set to <c>true</c> [discontinued].</param>
        public MyBusinessObject(int id, string name, double unitPrice, DateTime date, bool discontinued)
        {
            this.id = id;
            this.name = name;
            this.unitPrice = unitPrice;
            this.date = date;
            this.discontinued = discontinued;
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                if (this.ID != value)
                {
                    this.id = value;
                    this.OnPropertyChanged("ID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.Name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>The unit price.</value>
        public double UnitPrice
        {
            get
            {
                return this.unitPrice;
            }
            set
            {
                if (this.UnitPrice != value)
                {
                    this.unitPrice = value;
                    this.OnPropertyChanged("UnitPrice");
                }
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if (this.Date != value)
                {
                    this.date = value;
                    this.OnPropertyChanged("Date");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MyBusinessObject"/> is discontinued.
        /// </summary>
        /// <value><c>true</c> if discontinued; otherwise, <c>false</c>.</value>
        public bool Discontinued
        {
            get
            {
                return this.discontinued;
            }
            set
            {
                if (this.Discontinued != value)
                {
                    this.discontinued = value;
                    this.OnPropertyChanged("Discontinued");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
