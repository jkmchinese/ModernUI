/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： CachedTextInfo.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-25 17:59
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows.Controls;

namespace Genew.ModernUI.ExtendedToolkit.Primitives
{
    internal class CachedTextInfo : ICloneable
    {
        private CachedTextInfo(string text, int caretIndex, int selectionStart, int selectionLength)
        {
            Text = text;
            CaretIndex = caretIndex;
            SelectionStart = selectionStart;
            SelectionLength = selectionLength;
        }

        public CachedTextInfo(TextBox textBox)
            : this(textBox.Text, textBox.CaretIndex, textBox.SelectionStart, textBox.SelectionLength)
        {
        }

        public string Text { get; private set; }
        public int CaretIndex { get; private set; }
        public int SelectionStart { get; private set; }
        public int SelectionLength { get; private set; }

        #region ICloneable Members

        public object Clone()
        {
            return new CachedTextInfo(Text, CaretIndex, SelectionStart, SelectionLength);
        }

        #endregion
    }
}