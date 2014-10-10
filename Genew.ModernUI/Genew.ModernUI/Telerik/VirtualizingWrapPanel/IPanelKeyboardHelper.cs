using System.Windows;
using System.Windows.Controls;

namespace ModernUI.ExtendedToolkit.Internal
{
    internal interface IPanelKeyboardHelper
    {
        IPanelHelper PanelHelper { get; set; }

        Point GetOffsets(int index);

        int GetPageUpIndex(int fromIndex);

        int GetPageDownIndex(int fromIndex);

        double GetVerticalOffsetForTouch();

        double GetHorizontalOffsetForTouch();
    }
}