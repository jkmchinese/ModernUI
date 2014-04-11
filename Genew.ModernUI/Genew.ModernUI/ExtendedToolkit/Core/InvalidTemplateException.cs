/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： InvalidTemplateException.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-12 0:04
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace Genew.ModernUI.ExtendedToolkit
{
    public class InvalidTemplateException : Exception
    {
        #region Constructors

        public InvalidTemplateException(string message)
            : base(message)
        {
        }

        public InvalidTemplateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}