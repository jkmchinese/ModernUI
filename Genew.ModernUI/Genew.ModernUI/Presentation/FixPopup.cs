/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： FixPopup.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-02 21:33:15
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;

namespace ModernUI.Presentation
{
    /// <summary>
    /// Popup支持中文输入法
    /// </summary>
    public class FixPopup : Popup
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetActiveWindow(IntPtr hWnd);

        static FixPopup()
        {
            EventManager.RegisterClassHandler(typeof(FixPopup), PreviewGotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(OnPreviewGotKeyboardFocus), true);
        }

        private static void OnPreviewGotKeyboardFocus(Object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = e.NewFocus as TextBoxBase;
            if (textBox != null)
            {
                var hwndSource = PresentationSource.FromVisual(textBox) as HwndSource;
                if (hwndSource != null)
                {
                    SetActiveWindow(hwndSource.Handle);
                }
            }
        }
    }
}
