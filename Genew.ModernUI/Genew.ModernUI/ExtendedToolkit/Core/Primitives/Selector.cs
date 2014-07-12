/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： Selector.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-26 23:22
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ModernUI.ExtendedToolkit.Utilities;

namespace ModernUI.ExtendedToolkit.Primitives
{
    /// <summary>
    /// 选择器
    /// </summary>
    public class Selector : ItemsControl, IWeakEventListener //should probably make this control an ICommandSource
    {
        #region Members

        private readonly ValueChangeHelper _selectedMemberPathValuesHelper;
        private readonly ValueChangeHelper _valueMemberPathValuesHelper;
        private bool _ignoreSelectedItemChanged;
        private int _ignoreSelectedItemsCollectionChanged;
        private int _ignoreSelectedMemberPathValuesChanged;
        private bool _ignoreSelectedValueChanged;
        private IList _selectedItems;
        private bool _surpressItemSelectionChanged;

        #endregion //Members

        #region Constructors

        public Selector()
        {
            SelectedItems = new ObservableCollection<object>();
            AddHandler(Selector.SelectedEvent,
                new RoutedEventHandler((s, args) => OnItemSelectionChangedCore(args, false)));
            AddHandler(Selector.UnSelectedEvent,
                new RoutedEventHandler((s, args) => OnItemSelectionChangedCore(args, true)));
            _selectedMemberPathValuesHelper = new ValueChangeHelper(OnSelectedMemberPathValuesChanged);
            _valueMemberPathValuesHelper = new ValueChangeHelper(OnValueMemberPathValuesChanged);
        }

        #endregion //Constructors

        #region Properties

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command",
            typeof(ICommand), typeof(Selector), new PropertyMetadata((ICommand)null));

        public static readonly DependencyProperty SelectedMemberPathProperty =
            DependencyProperty.Register("SelectedMemberPath", typeof(string), typeof(Selector),
                new UIPropertyMetadata(null, OnSelectedMemberPathChanged));

        [TypeConverter(typeof(CommandConverter))]
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Set the 'IsSelect' value back to the Model
        /// </summary>
        public string SelectedMemberPath
        {
            get { return (string)GetValue(SelectedMemberPathProperty); }
            set { SetValue(SelectedMemberPathProperty, value); }
        }

        #region Delimiter

        public static readonly DependencyProperty DelimiterProperty = DependencyProperty.Register("Delimiter",
            typeof(string), typeof(Selector), new UIPropertyMetadata(",", OnDelimiterChanged));

        public string Delimiter
        {
            get { return (string)GetValue(DelimiterProperty); }
            set { SetValue(DelimiterProperty, value); }
        }

        private static void OnDelimiterChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((Selector)o).UpdateSelectedValue();
        }

        #endregion

        #region SelectedItem property

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem",
            typeof(object), typeof(Selector), new UIPropertyMetadata(null, OnSelectedItemChanged));

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Selector)sender).OnSelectedItemChanged(args.OldValue, args.NewValue);
        }

        private void OnSelectedItemChanged(object oldValue, object newValue)
        {
            if (_ignoreSelectedItemChanged)
                return;

            _ignoreSelectedItemsCollectionChanged++;
            SelectedItems.Clear();
            if (newValue != null)
            {
                SelectedItems.Add(newValue);
            }
            UpdateFromSelectedItems();
            _ignoreSelectedItemsCollectionChanged--;
        }

        #endregion

        #region SelectedItems Property

        public IList SelectedItems
        {
            get { return _selectedItems; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                INotifyCollectionChanged oldCollection = _selectedItems as INotifyCollectionChanged;
                INotifyCollectionChanged newCollection = value as INotifyCollectionChanged;

                if (oldCollection != null)
                {
                    CollectionChangedEventManager.RemoveListener(oldCollection, this);
                }

                if (newCollection != null)
                {
                    CollectionChangedEventManager.AddListener(newCollection, this);
                }

                _selectedItems = value;

                UpdateFromSelectedItems();
            }
        }

        #endregion SelectedItems

        #region SelectedItemsOverride property

        public static readonly DependencyProperty SelectedItemsOverrideProperty =
            DependencyProperty.Register("SelectedItemsOverride", typeof(IList), typeof(Selector),
                new UIPropertyMetadata(null, SelectedItemsOverrideChanged));

        public IList SelectedItemsOverride
        {
            get { return (IList)GetValue(SelectedItemsOverrideProperty); }
            set { SetValue(SelectedItemsOverrideProperty, value); }
        }

        private static void SelectedItemsOverrideChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs args)
        {
            ((Selector)sender).OnSelectedItemsOverrideChanged((IList)args.OldValue, (IList)args.NewValue);
        }

        private void OnSelectedItemsOverrideChanged(IList oldValue, IList newValue)
        {
            SelectedItems = (newValue != null) ? newValue : new ObservableCollection<object>();
        }

        #endregion

        private static void OnSelectedMemberPathChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((Selector)o).UpdateSelectedMemberPathValuesBindings();
        }

        #region SelectedValue

        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue",
            typeof(string), typeof(Selector),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnSelectedValueChanged));

        public string SelectedValue
        {
            get { return (string)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        private static void OnSelectedValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Selector selector = o as Selector;
            if (selector != null)
                selector.OnSelectedValueChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnSelectedValueChanged(string oldValue, string newValue)
        {
            if (_ignoreSelectedValueChanged)
                return;

            UpdateFromSelectedValue();
        }

        #endregion //SelectedValue

        #region ValueMemberPath

        public static readonly DependencyProperty ValueMemberPathProperty =
            DependencyProperty.Register("ValueMemberPath", typeof(string), typeof(Selector),
                new UIPropertyMetadata(OnValueMemberPathChanged));

        public string ValueMemberPath
        {
            get { return (string)GetValue(ValueMemberPathProperty); }
            set { SetValue(ValueMemberPathProperty, value); }
        }

        private static void OnValueMemberPathChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((Selector)o).UpdateValueMemberPathValuesBindings();
        }

        #endregion

        #region ItemsCollection Property

        protected IEnumerable ItemsCollection
        {
            get { return ItemsSource ?? (Items ?? (IEnumerable)new object[0]); }
        }

        #endregion

        #endregion //Properties

        #region Base Class Overrides

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is SelectorItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SelectorItem();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            _surpressItemSelectionChanged = true;
            var selectorItem = element as FrameworkElement;

            selectorItem.SetValue(SelectorItem.IsSelectedProperty, SelectedItems.Contains(item));

            _surpressItemSelectionChanged = false;
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            var oldCollection = oldValue as INotifyCollectionChanged;
            var newCollection = newValue as INotifyCollectionChanged;

            if (oldCollection != null)
            {
                CollectionChangedEventManager.RemoveListener(oldCollection, this);
            }

            if (newCollection != null)
            {
                CollectionChangedEventManager.AddListener(newCollection, this);
            }

            RemoveUnavailableSelectedItems();
            UpdateSelectedMemberPathValuesBindings();
            UpdateValueMemberPathValuesBindings();
        }

        #endregion //Base Class Overrides

        #region Events

        public static readonly RoutedEvent SelectedEvent = EventManager.RegisterRoutedEvent("SelectedEvent",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Selector));

        public static readonly RoutedEvent UnSelectedEvent = EventManager.RegisterRoutedEvent("UnSelectedEvent",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Selector));

        public static readonly RoutedEvent ItemSelectionChangedEvent =
            EventManager.RegisterRoutedEvent("ItemSelectionChanged", RoutingStrategy.Bubble,
                typeof(ItemSelectionChangedEventHandler), typeof(Selector));

        public event ItemSelectionChangedEventHandler ItemSelectionChanged
        {
            add { AddHandler(ItemSelectionChangedEvent, value); }
            remove { RemoveHandler(ItemSelectionChangedEvent, value); }
        }

        #endregion //Events

        #region Methods

        protected object GetItemValue(object item)
        {
            if (!String.IsNullOrEmpty(ValueMemberPath) && (item != null))
            {
                var property = item.GetType().GetProperty(ValueMemberPath);
                if (property != null)
                    return property.GetValue(item, null);
            }

            return item;
        }

        protected object ResolveItemByValue(string value)
        {
            if (!String.IsNullOrEmpty(ValueMemberPath))
            {
                foreach (object item in ItemsCollection)
                {
                    var property = item.GetType().GetProperty(ValueMemberPath);
                    if (property != null)
                    {
                        var propertyValue = property.GetValue(item, null);
                        if (value.Equals(propertyValue.ToString(), StringComparison.InvariantCultureIgnoreCase))
                            return item;
                    }
                }
            }

            return value;
        }

        private bool? GetSelectedMemberPathValue(object item)
        {
            PropertyInfo prop = GetSelectedMemberPathProperty(item);

            return (prop != null)
                ? (bool)prop.GetValue(item, null)
                : (bool?)null;
        }

        private void SetSelectedMemberPathValue(object item, bool value)
        {
            PropertyInfo prop = GetSelectedMemberPathProperty(item);

            if (prop != null)
            {
                prop.SetValue(item, value, null);
            }
        }

        private PropertyInfo GetSelectedMemberPathProperty(object item)
        {
            PropertyInfo propertyInfo = null;
            if (!String.IsNullOrEmpty(SelectedMemberPath) && (item != null))
            {
                var property = item.GetType().GetProperty(SelectedMemberPath);
                if (property != null && property.PropertyType == typeof(bool))
                {
                    propertyInfo = property;
                }
            }

            return propertyInfo;
        }

        /// <summary>
        ///     When SelectedItems collection implements INotifyPropertyChanged, this is the callback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_ignoreSelectedItemsCollectionChanged > 0)
                return;

            // Keep it simple for now. Just update all
            UpdateFromSelectedItems();
        }

        private void OnItemSelectionChangedCore(RoutedEventArgs args, bool unselected)
        {
            object item = ItemContainerGenerator.ItemFromContainer((DependencyObject)args.OriginalSource);

            // When the item is it's own container, "UnsetValue" will be returned.
            if (item == DependencyProperty.UnsetValue)
            {
                item = args.OriginalSource;
            }

            if (unselected)
            {
                while (SelectedItems.Contains(item))
                    SelectedItems.Remove(item);
            }
            else
            {
                if (!SelectedItems.Contains(item))
                    SelectedItems.Add(item);
            }

            OnItemSelectionChanged(
                new ItemSelectionChangedEventArgs(Selector.ItemSelectionChangedEvent, this, item, !unselected));
        }

        /// <summary>
        ///     When the ItemsSource implements INotifyPropertyChanged, this is the change callback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            RemoveUnavailableSelectedItems();
            UpdateSelectedMemberPathValuesBindings();
            UpdateValueMemberPathValuesBindings();
        }

        /// <summary>
        ///     This is called when any value of any item referenced by SelectedMemberPath
        ///     is modified. This may affect the SelectedItems collection.
        /// </summary>
        private void OnSelectedMemberPathValuesChanged()
        {
            if (_ignoreSelectedMemberPathValuesChanged > 0)
                return;

            UpdateFromSelectedMemberPathValues();
        }

        /// <summary>
        ///     This is called when any value of any item referenced by ValueMemberPath
        ///     is modified. This will affect the SelectedValue property
        /// </summary>
        private void OnValueMemberPathValuesChanged()
        {
            UpdateSelectedValue();
        }

        private void UpdateSelectedMemberPathValuesBindings()
        {
            _selectedMemberPathValuesHelper.UpdateValueSource(ItemsCollection, SelectedMemberPath);
        }

        private void UpdateValueMemberPathValuesBindings()
        {
            _valueMemberPathValuesHelper.UpdateValueSource(ItemsCollection, ValueMemberPath);
        }

        /// <summary>
        ///     This method will be called when the "IsSelected" property of an SelectorItem
        ///     has been modified.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnItemSelectionChanged(ItemSelectionChangedEventArgs args)
        {
            if (_surpressItemSelectionChanged)
                return;

            RaiseEvent(args);

            if (Command != null)
                Command.Execute(args.Item);
        }

        /// <summary>
        ///     Updates the SelectedValue property based on what is present in the SelectedItems property.
        /// </summary>
        private void UpdateSelectedValue()
        {
            string newValue = String.Join(Delimiter, SelectedItems.Cast<object>().Select(GetItemValue));

            if (String.IsNullOrEmpty(SelectedValue) || !SelectedValue.Equals(newValue))
            {
                _ignoreSelectedValueChanged = true;
                SelectedValue = newValue;
                _ignoreSelectedValueChanged = false;
            }
        }

        /// <summary>
        ///     Updates the SelectedItem property based on what is present in the SelectedItems property.
        /// </summary>
        private void UpdateSelectedItem()
        {
            if (!SelectedItems.Contains(SelectedItem))
            {
                _ignoreSelectedItemChanged = true;
                SelectedItem = (SelectedItems.Count > 0) ? SelectedItems[0] : null;
                _ignoreSelectedItemChanged = false;
            }
        }

        /// <summary>
        ///     Update the SelectedItems collection based on the values
        ///     refered to by the SelectedMemberPath property.
        /// </summary>
        private void UpdateFromSelectedMemberPathValues()
        {
            _ignoreSelectedItemsCollectionChanged++;
            foreach (var item in ItemsCollection)
            {
                bool? isSelected = GetSelectedMemberPathValue(item);
                if (isSelected != null)
                {
                    if (isSelected.Value)
                    {
                        if (!SelectedItems.Contains(item))
                        {
                            SelectedItems.Add(item);
                        }
                    }
                    else
                    {
                        if (SelectedItems.Contains(item))
                        {
                            SelectedItems.Remove(item);
                        }
                    }
                }
            }
            _ignoreSelectedItemsCollectionChanged--;
            UpdateFromSelectedItems();
        }

        /// <summary>
        ///     Updates the following based on the content of SelectedItems:
        ///     - All SelectorItems "IsSelected" properties
        ///     - Values refered to by SelectedMemberPath
        ///     - SelectedItem property
        ///     - SelectedValue property
        ///     Refered to by the SelectedMemberPath property.
        /// </summary>
        private void UpdateFromSelectedItems()
        {
            foreach (object o in ItemsCollection)
            {
                bool isSelected = SelectedItems.Contains(o);

                _ignoreSelectedMemberPathValuesChanged++;
                SetSelectedMemberPathValue(o, isSelected);
                _ignoreSelectedMemberPathValuesChanged--;

                var selectorItem = ItemContainerGenerator.ContainerFromItem(o) as SelectorItem;
                if (selectorItem != null)
                {
                    selectorItem.IsSelected = isSelected;
                }
            }

            UpdateSelectedItem();
            UpdateSelectedValue();
        }

        /// <summary>
        ///     Removes all items from SelectedItems that are no longer in ItemsSource.
        /// </summary>
        private void RemoveUnavailableSelectedItems()
        {
            _ignoreSelectedItemsCollectionChanged++;
            HashSet<object> hash = new HashSet<object>(ItemsCollection.Cast<object>());

            for (int i = 0; i < SelectedItems.Count; i++)
            {
                if (!hash.Contains(SelectedItems[i]))
                {
                    SelectedItems.RemoveAt(i);
                    i--;
                }
            }
            _ignoreSelectedItemsCollectionChanged--;

            UpdateSelectedItem();
            UpdateSelectedValue();
        }

        /// <summary>
        ///     Updates the SelectedItems collection based on the content of
        ///     the SelectedValue property.
        /// </summary>
        private void UpdateFromSelectedValue()
        {
            _ignoreSelectedItemsCollectionChanged++;
            // Just update the SelectedItems collection content 
            // and let the synchronization be made from UpdateFromSelectedItems();
            SelectedItems.Clear();

            if (!String.IsNullOrEmpty(SelectedValue))
            {
                List<string> selectedValues =
                    SelectedValue.Split(new[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries).ToList();
                ValueEqualityComparer comparer = new ValueEqualityComparer();

                foreach (object item in ItemsCollection)
                {
                    object itemValue = GetItemValue(item);

                    bool isSelected = (itemValue != null)
                                      && selectedValues.Contains(itemValue.ToString(), comparer);

                    if (isSelected)
                    {
                        SelectedItems.Add(item);
                    }
                }
            }
            _ignoreSelectedItemsCollectionChanged--;

            UpdateFromSelectedItems();
        }

        #endregion //Methods

        #region IWeakEventListener Members

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(CollectionChangedEventManager))
            {
                if (object.ReferenceEquals(_selectedItems, sender))
                {
                    OnSelectedItemsCollectionChanged(sender, (NotifyCollectionChangedEventArgs)e);
                    return true;
                }
                if (object.ReferenceEquals(ItemsCollection, sender))
                {
                    OnItemsSourceCollectionChanged(sender, (NotifyCollectionChangedEventArgs)e);
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region ValueEqualityComparer private class

        private class ValueEqualityComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(string obj)
            {
                return 1;
            }
        }

        #endregion
    }


    public delegate void ItemSelectionChangedEventHandler(object sender, ItemSelectionChangedEventArgs e);

    public class ItemSelectionChangedEventArgs : RoutedEventArgs
    {
        public ItemSelectionChangedEventArgs(RoutedEvent routedEvent, object source, object item, bool isSelected)
            : base(routedEvent, source)
        {
            Item = item;
            IsSelected = isSelected;
        }

        public bool IsSelected { get; private set; }
        public object Item { get; private set; }
    }
}