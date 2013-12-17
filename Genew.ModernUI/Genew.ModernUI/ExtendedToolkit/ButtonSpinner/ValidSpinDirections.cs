/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ValidSpinDirections.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:45
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace Genew.ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Represents spin directions that are valid.
    /// </summary>
    [Flags]
    public enum ValidSpinDirections
    {
        /// <summary>
        ///     Can not increase nor decrease.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Can increase.
        /// </summary>
        Increase = 1,

        /// <summary>
        ///     Can decrease.
        /// </summary>
        Decrease = 2
    }
}