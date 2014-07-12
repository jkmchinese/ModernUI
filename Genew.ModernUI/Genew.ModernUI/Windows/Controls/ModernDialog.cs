using ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModernUI.Windows.Controls
{
    /// <summary>
    /// Represents a Modern UI styled dialog window.
    /// </summary>
    public class ModernDialog
        : Window
    {
        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty BackgroundContentProperty = DependencyProperty.Register("BackgroundContent", typeof(object), typeof(ModernDialog));
        /// <summary>
        /// Identifies the Buttons dependency property.
        /// </summary>
        public static readonly DependencyProperty ButtonsProperty = DependencyProperty.Register("Buttons", typeof(IEnumerable<Button>), typeof(ModernDialog));

        private readonly ICommand m_closeCommand;

        private Button m_okButton;
        private Button m_cancelButton;
        private Button m_yesButton;
        private Button m_noButton;
        private Button m_closeButton;

        private MessageBoxResult m_dialogResult = MessageBoxResult.None;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModernDialog"/> class.
        /// </summary>
        public ModernDialog()
        {
            this.DefaultStyleKey = typeof(ModernDialog);
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            this.m_closeCommand = new RelayCommand(o =>
            {
                var result = o as MessageBoxResult?;
                if (result.HasValue)
                {
                    this.m_dialogResult = result.Value;
                }
                Close();
            });

            this.Buttons = new Button[] { this.CloseButton };

            // set the default owner to the app main window (if possible)
            if (Application.Current != null && Application.Current.MainWindow != this)
            {
                this.Owner = Application.Current.MainWindow;
            }
        }

        private Button CreateCloseDialogButton(string content, bool isDefault, bool isCancel, MessageBoxResult result)
        {
            return new Button
            {
                Content = content,
                Command = this.CloseCommand,
                CommandParameter = result,
                IsDefault = isDefault,
                IsCancel = isCancel,
                MinHeight = 21,
                MinWidth = 65,
                Margin = new Thickness(4, 0, 0, 0)
            };
        }

        /// <summary>
        /// Gets the close window command.
        /// </summary>
        public ICommand CloseCommand
        {
            get { return this.m_closeCommand; }
        }

        /// <summary>
        /// Gets the Ok button.
        /// </summary>
        public Button OkButton
        {
            get
            {
                if (this.m_okButton == null)
                {
                    this.m_okButton = CreateCloseDialogButton(LWLCX.ModernUI.Properties.Resources.Ok, true, false, MessageBoxResult.OK);
                }
                return this.m_okButton;
            }
        }

        /// <summary>
        /// Gets the Cancel button.
        /// </summary>
        public Button CancelButton
        {
            get
            {
                if (this.m_cancelButton == null)
                {
                    this.m_cancelButton = CreateCloseDialogButton(LWLCX.ModernUI.Properties.Resources.Cancel, false, true, MessageBoxResult.Cancel);
                }
                return this.m_cancelButton;
            }
        }

        /// <summary>
        /// Gets the Yes button.
        /// </summary>
        public Button YesButton
        {
            get
            {
                if (this.m_yesButton == null)
                {
                    this.m_yesButton = CreateCloseDialogButton(LWLCX.ModernUI.Properties.Resources.Yes, true, false, MessageBoxResult.Yes);
                }
                return this.m_yesButton;
            }
        }

        /// <summary>
        /// Gets the No button.
        /// </summary>
        public Button NoButton
        {
            get
            {
                if (this.m_noButton == null)
                {
                    this.m_noButton = CreateCloseDialogButton(LWLCX.ModernUI.Properties.Resources.No, false, true, MessageBoxResult.No);
                }
                return this.m_noButton;
            }
        }

        /// <summary>
        /// Gets the Close button.
        /// </summary>
        public Button CloseButton
        {
            get
            {
                if (this.m_closeButton == null)
                {
                    this.m_closeButton = CreateCloseDialogButton(LWLCX.ModernUI.Properties.Resources.Close, true, false, MessageBoxResult.None);
                }
                return this.m_closeButton;
            }
        }

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public object BackgroundContent
        {
            get { return GetValue(BackgroundContentProperty); }
            set { SetValue(BackgroundContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the dialog buttons.
        /// </summary>
        public IEnumerable<Button> Buttons
        {
            get { return (IEnumerable<Button>)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }

        /// <summary>
        /// Displays a messagebox.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="title">The title.</param>
        /// <param name="button">The button.</param>
        /// <returns></returns>
        public static MessageBoxResult ShowMessage(string text, string title, MessageBoxButton button)
        {
            var dlg = new ModernDialog
            {
                Title = title,
                Content = new BBCodeBlock { BBCode = text, Margin = new Thickness(0, 0, 0, 8) },
                MinHeight = 0,
                MinWidth = 0,
                MaxHeight = 480,
                MaxWidth = 640,
            };

            dlg.Buttons = GetButtons(dlg, button);
            dlg.ShowDialog();
            return dlg.m_dialogResult;
        }

        private static IEnumerable<Button> GetButtons(ModernDialog owner, MessageBoxButton button)
        {
            if (button == MessageBoxButton.OK)
            {
                yield return owner.OkButton;
            }
            else if (button == MessageBoxButton.OKCancel)
            {
                yield return owner.OkButton;
                yield return owner.CancelButton;
            }
            else if (button == MessageBoxButton.YesNo)
            {
                yield return owner.YesButton;
                yield return owner.NoButton;
            }
            else if (button == MessageBoxButton.YesNoCancel)
            {
                yield return owner.YesButton;
                yield return owner.NoButton;
                yield return owner.CancelButton;
            }
        }
    }
}
