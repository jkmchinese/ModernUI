/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： InputValidationErrorEventArgs.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:37
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace ModernUI.ExtendedToolkit.Input
{
    public delegate void InputValidationErrorEventHandler(object sender, InputValidationErrorEventArgs e);

    public class InputValidationErrorEventArgs : EventArgs
    {
        #region Constructors

        public InputValidationErrorEventArgs(Exception e)
        {
            Exception = e;
        }

        #endregion

        #region Exception Property

        public Exception Exception { get; private set; }

        #endregion

        #region ThrowException Property

        public bool ThrowException { get; set; }

        #endregion
    }
}