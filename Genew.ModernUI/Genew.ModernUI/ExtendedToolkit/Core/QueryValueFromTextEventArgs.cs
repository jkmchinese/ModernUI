/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： QueryValueFromTextEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-25 18:00
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace ModernUI.ExtendedToolkit
{
    public class QueryValueFromTextEventArgs : EventArgs
    {
        public QueryValueFromTextEventArgs(string text, object value)
        {
            m_text = text;
            Value = value;
        }

        #region Text Property

        private readonly string m_text;

        public string Text
        {
            get { return m_text; }
        }

        #endregion Text Property

        #region Value Property

        public object Value { get; set; }

        #endregion Value Property

        #region HasParsingError Property

        public bool HasParsingError { get; set; }

        #endregion HasParsingError Property
    }
}