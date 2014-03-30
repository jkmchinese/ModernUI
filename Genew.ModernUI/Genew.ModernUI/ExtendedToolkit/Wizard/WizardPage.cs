/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： WizardPage.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-29 23:46
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Genew.ModernUI.ExtendedToolkit
{
    /// <summary>
    /// Represents a ContentControl that can be used in Wizard as an item.
    /// </summary>
    public class WizardPage : ContentControl
    {
        #region Properties

        public static readonly DependencyProperty BackButtonVisibilityProperty =
            DependencyProperty.Register("BackButtonVisibility", typeof(WizardPageButtonVisibility), typeof(WizardPage),
                new UIPropertyMetadata(WizardPageButtonVisibility.Inherit));

        public static readonly DependencyProperty CanCancelProperty = DependencyProperty.Register("CanCancel",
            typeof(bool?), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CancelButtonVisibilityProperty =
            DependencyProperty.Register("CancelButtonVisibility", typeof(WizardPageButtonVisibility),
                typeof(WizardPage), new UIPropertyMetadata(WizardPageButtonVisibility.Inherit));

        public static readonly DependencyProperty CanFinishProperty = DependencyProperty.Register("CanFinish",
            typeof(bool?), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CanHelpProperty = DependencyProperty.Register("CanHelp",
            typeof(bool?), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CanSelectNextPageProperty =
            DependencyProperty.Register("CanSelectNextPage", typeof(bool?), typeof(WizardPage),
                new UIPropertyMetadata(null));

        public static readonly DependencyProperty CanSelectPreviousPageProperty =
            DependencyProperty.Register("CanSelectPreviousPage", typeof(bool?), typeof(WizardPage),
                new UIPropertyMetadata(null));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description",
            typeof(string), typeof(WizardPage));

        public static readonly DependencyProperty ExteriorPanelBackgroundProperty =
            DependencyProperty.Register("ExteriorPanelBackground", typeof(Brush), typeof(WizardPage),
                new UIPropertyMetadata(null));

        public static readonly DependencyProperty ExteriorPanelContentProperty =
            DependencyProperty.Register("ExteriorPanelContent", typeof(object), typeof(WizardPage),
                new UIPropertyMetadata(null));

        public static readonly DependencyProperty FinishButtonVisibilityProperty =
            DependencyProperty.Register("FinishButtonVisibility", typeof(WizardPageButtonVisibility),
                typeof(WizardPage), new UIPropertyMetadata(WizardPageButtonVisibility.Inherit));

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(WizardPage),
                new UIPropertyMetadata(Brushes.White));

        public static readonly DependencyProperty HeaderImageProperty = DependencyProperty.Register("HeaderImage",
            typeof(ImageSource), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty HelpButtonVisibilityProperty =
            DependencyProperty.Register("HelpButtonVisibility", typeof(WizardPageButtonVisibility), typeof(WizardPage),
                new UIPropertyMetadata(WizardPageButtonVisibility.Inherit));

        public static readonly DependencyProperty NextButtonVisibilityProperty =
            DependencyProperty.Register("NextButtonVisibility", typeof(WizardPageButtonVisibility), typeof(WizardPage),
                new UIPropertyMetadata(WizardPageButtonVisibility.Inherit));

        public static readonly DependencyProperty NextPageProperty = DependencyProperty.Register("NextPage",
            typeof(WizardPage), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty PageTypeProperty = DependencyProperty.Register("PageType",
            typeof(WizardPageType), typeof(WizardPage), new UIPropertyMetadata(WizardPageType.Exterior));

        public static readonly DependencyProperty PreviousPageProperty = DependencyProperty.Register("PreviousPage",
            typeof(WizardPage), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string),
            typeof(WizardPage));

        public WizardPageButtonVisibility BackButtonVisibility
        {
            get { return (WizardPageButtonVisibility)GetValue(BackButtonVisibilityProperty); }
            set { SetValue(BackButtonVisibilityProperty, value); }
        }

        public bool? CanCancel
        {
            get { return (bool?)GetValue(CanCancelProperty); }
            set { SetValue(CanCancelProperty, value); }
        }

        public WizardPageButtonVisibility CancelButtonVisibility
        {
            get { return (WizardPageButtonVisibility)GetValue(CancelButtonVisibilityProperty); }
            set { SetValue(CancelButtonVisibilityProperty, value); }
        }

        public bool? CanFinish
        {
            get { return (bool?)GetValue(CanFinishProperty); }
            set { SetValue(CanFinishProperty, value); }
        }

        public bool? CanHelp
        {
            get { return (bool?)GetValue(CanHelpProperty); }
            set { SetValue(CanHelpProperty, value); }
        }

        public bool? CanSelectNextPage
        {
            get { return (bool?)GetValue(CanSelectNextPageProperty); }
            set { SetValue(CanSelectNextPageProperty, value); }
        }

        public bool? CanSelectPreviousPage
        {
            get { return (bool?)GetValue(CanSelectPreviousPageProperty); }
            set { SetValue(CanSelectPreviousPageProperty, value); }
        }

        public string Description
        {
            get { return (string)base.GetValue(DescriptionProperty); }
            set { base.SetValue(DescriptionProperty, value); }
        }

        public Brush ExteriorPanelBackground
        {
            get { return (Brush)GetValue(ExteriorPanelBackgroundProperty); }
            set { SetValue(ExteriorPanelBackgroundProperty, value); }
        }

        public object ExteriorPanelContent
        {
            get { return GetValue(ExteriorPanelContentProperty); }
            set { SetValue(ExteriorPanelContentProperty, value); }
        }

        public WizardPageButtonVisibility FinishButtonVisibility
        {
            get { return (WizardPageButtonVisibility)GetValue(FinishButtonVisibilityProperty); }
            set { SetValue(FinishButtonVisibilityProperty, value); }
        }

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public ImageSource HeaderImage
        {
            get { return (ImageSource)GetValue(HeaderImageProperty); }
            set { SetValue(HeaderImageProperty, value); }
        }

        public WizardPageButtonVisibility HelpButtonVisibility
        {
            get { return (WizardPageButtonVisibility)GetValue(HelpButtonVisibilityProperty); }
            set { SetValue(HelpButtonVisibilityProperty, value); }
        }

        public WizardPageButtonVisibility NextButtonVisibility
        {
            get { return (WizardPageButtonVisibility)GetValue(NextButtonVisibilityProperty); }
            set { SetValue(NextButtonVisibilityProperty, value); }
        }

        public WizardPage NextPage
        {
            get { return (WizardPage)GetValue(NextPageProperty); }
            set { SetValue(NextPageProperty, value); }
        }

        public WizardPageType PageType
        {
            get { return (WizardPageType)GetValue(PageTypeProperty); }
            set { SetValue(PageTypeProperty, value); }
        }

        public WizardPage PreviousPage
        {
            get { return (WizardPage)GetValue(PreviousPageProperty); }
            set { SetValue(PreviousPageProperty, value); }
        }

        public string Title
        {
            get { return (string)base.GetValue(TitleProperty); }
            set { base.SetValue(TitleProperty, value); }
        }

        #endregion //Properties

        #region Constructors

        static WizardPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WizardPage),
                new FrameworkPropertyMetadata(typeof(WizardPage)));
        }

        public WizardPage()
        {
            Loaded += WizardPage_Loaded;
            Unloaded += WizardPage_Unloaded;
        }

        private void WizardPage_Unloaded(object sender, RoutedEventArgs e)
        {
            base.RaiseEvent(new RoutedEventArgs(WizardPage.LeaveEvent, this));
        }

        private void WizardPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsVisible)
            {
                base.RaiseEvent(new RoutedEventArgs(WizardPage.EnterEvent, this));
            }
        }

        #endregion //Constructors

        #region Overrides

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if ((e.Property.Name == "CanSelectNextPage") || (e.Property.Name == "CanHelp") ||
                (e.Property.Name == "CanFinish")
                || (e.Property.Name == "CanCancel") || (e.Property.Name == "CanSelectPreviousPage"))
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }

        #endregion

        #region Events

        #region Enter Event

        public static readonly RoutedEvent EnterEvent = EventManager.RegisterRoutedEvent("Enter", RoutingStrategy.Bubble,
            typeof(EventHandler), typeof(WizardPage));

        public event RoutedEventHandler Enter
        {
            add { AddHandler(EnterEvent, value); }
            remove { RemoveHandler(EnterEvent, value); }
        }

        #endregion //Enter Event

        #region Leave Event

        public static readonly RoutedEvent LeaveEvent = EventManager.RegisterRoutedEvent("Leave", RoutingStrategy.Bubble,
            typeof(EventHandler), typeof(WizardPage));

        public event RoutedEventHandler Leave
        {
            add { AddHandler(LeaveEvent, value); }
            remove { RemoveHandler(LeaveEvent, value); }
        }

        #endregion //Leave Event

        #endregion  //Events
    }
}