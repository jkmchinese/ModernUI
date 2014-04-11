/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： InvalidContentException.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-11 23:38
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace Genew.ModernUI.ExtendedToolkit
{
    public class InvalidContentException : Exception
    {
        #region Constructors

        public InvalidContentException(string message)
            : base(message)
        {
        }

        public InvalidContentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}