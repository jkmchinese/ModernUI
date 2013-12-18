/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： KeyboardUtilities.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-18 10:53
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows.Input;

namespace Genew.ModernUI.ExtendedToolkit.Utilities
{
    internal class KeyboardUtilities
    {
        internal static bool IsKeyModifyingPopupState(KeyEventArgs e)
        {
            return ((((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt) &&
                     ((e.SystemKey == Key.Down) || (e.SystemKey == Key.Up)))
                    || (e.Key == Key.F4));
        }
    }
}