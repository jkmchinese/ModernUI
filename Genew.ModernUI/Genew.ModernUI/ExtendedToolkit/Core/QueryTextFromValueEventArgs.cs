/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： QueryTextFromValueEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-25 17:56
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace Genew.ModernUI.ExtendedToolkit
{
    public class QueryTextFromValueEventArgs : EventArgs
    {
        public QueryTextFromValueEventArgs(object value, string text)
        {
            m_value = value;
            Text = text;
        }

        #region Value Property

        private readonly object m_value;

        public object Value
        {
            get { return m_value; }
        }

        #endregion Value Property

        #region Text Property

        public string Text { get; set; }

        #endregion Text Property
    }
}