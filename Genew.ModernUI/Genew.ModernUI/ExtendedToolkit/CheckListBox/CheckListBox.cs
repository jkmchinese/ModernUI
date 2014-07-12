/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： CheckListBox.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-26 23:34
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows;
using ModernUI.ExtendedToolkit.Primitives;

namespace ModernUI.ExtendedToolkit
{
    /// <summary>
    /// 选择列表
    /// </summary>
    public class CheckListBox : Selector
    {
        #region Constructors

        static CheckListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckListBox),
                new FrameworkPropertyMetadata(typeof(CheckListBox)));
        }

        #endregion //Constructors
    }
}