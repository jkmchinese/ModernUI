using System.Collections;
using System.Windows;

namespace ModernUI.ExtendedToolkit.Internal
{
    internal interface IPanelHelper
    {
        IList Children { get; }

        Size DesiredSizeAt(int index);

        double Width { get; }

        double Height { get; }

        Rect GetLayoutSlot(FrameworkElement item);
    }
}