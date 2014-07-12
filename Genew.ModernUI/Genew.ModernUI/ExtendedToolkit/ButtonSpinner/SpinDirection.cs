/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： SpinDirection.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:45
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

namespace ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Represents spin directions that could be initiated by the end-user.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    public enum SpinDirection
    {
        /// <summary>
        ///     Represents a spin initiated by the end-user in order to Increase a value.
        /// </summary>
        Increase = 0,

        /// <summary>
        ///     Represents a spin initiated by the end-user in order to Decrease a value.
        /// </summary>
        Decrease = 1
    }
}