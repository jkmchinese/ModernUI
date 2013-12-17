/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： IValidateInput.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:37
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

namespace Genew.ModernUI.ExtendedToolkit.Input
{
    public interface IValidateInput
    {
        event InputValidationErrorEventHandler InputValidationError;
        bool CommitInput();
    }
}