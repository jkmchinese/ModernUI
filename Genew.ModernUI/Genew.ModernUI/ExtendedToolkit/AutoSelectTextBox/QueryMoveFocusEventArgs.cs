/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： QueryMoveFocusEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-25 0:21
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows;
using System.Windows.Input;

namespace Genew.ModernUI.ExtendedToolkit
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")]
    public delegate void QueryMoveFocusEventHandler(object sender, QueryMoveFocusEventArgs e);

    public class QueryMoveFocusEventArgs : RoutedEventArgs
    {
        //default CTOR private to prevent its usage.
        private readonly FocusNavigationDirection m_navigationDirection;
        private readonly bool m_reachedMaxLength;
        private bool m_canMove = true; //defaults to true... if nobody does nothing, then its capable of moving focus.

        private QueryMoveFocusEventArgs()
        {
        }

        //internal to prevent anybody from building this type of event.
        internal QueryMoveFocusEventArgs(FocusNavigationDirection direction, bool reachedMaxLength)
            : base(AutoSelectTextBox.QueryMoveFocusEvent)
        {
            m_navigationDirection = direction;
            m_reachedMaxLength = reachedMaxLength;
        }

        public FocusNavigationDirection FocusNavigationDirection
        {
            get { return m_navigationDirection; }
        }

        public bool ReachedMaxLength
        {
            get { return m_reachedMaxLength; }
        }

        public bool CanMoveFocus
        {
            get { return m_canMove; }
            set { m_canMove = value; }
        }
    }
}