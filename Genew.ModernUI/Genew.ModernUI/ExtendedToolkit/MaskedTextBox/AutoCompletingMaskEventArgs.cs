/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： AutoCompletingMaskEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-25 17:54
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.ComponentModel;

namespace Genew.ModernUI.ExtendedToolkit
{
    public class AutoCompletingMaskEventArgs : CancelEventArgs
    {
        public AutoCompletingMaskEventArgs(MaskedTextProvider maskedTextProvider, int startPosition, int selectionLength,
            string input)
        {
            AutoCompleteStartPosition = -1;

            m_maskedTextProvider = maskedTextProvider;
            m_startPosition = startPosition;
            m_selectionLength = selectionLength;
            m_input = input;
        }

        #region MaskedTextProvider PROPERTY

        private readonly MaskedTextProvider m_maskedTextProvider;

        public MaskedTextProvider MaskedTextProvider
        {
            get { return m_maskedTextProvider; }
        }

        #endregion MaskedTextProvider PROPERTY

        #region StartPosition PROPERTY

        private readonly int m_startPosition;

        public int StartPosition
        {
            get { return m_startPosition; }
        }

        #endregion StartPosition PROPERTY

        #region SelectionLength PROPERTY

        private readonly int m_selectionLength;

        public int SelectionLength
        {
            get { return m_selectionLength; }
        }

        #endregion SelectionLength PROPERTY

        #region Input PROPERTY

        private readonly string m_input;

        public string Input
        {
            get { return m_input; }
        }

        #endregion Input PROPERTY

        #region AutoCompleteStartPosition PROPERTY

        public int AutoCompleteStartPosition { get; set; }

        #endregion AutoCompleteStartPosition PROPERTY

        #region AutoCompleteText PROPERTY

        public string AutoCompleteText { get; set; }

        #endregion AutoCompleteText PROPERTY
    }
}