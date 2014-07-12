/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： SplitButton.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-24 20:53
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows;
using System.Windows.Controls;

namespace ModernUI.ExtendedToolkit
{
    [TemplatePart(Name = PART_ActionButton, Type = typeof(Button))]
    public class SplitButton : DropDownButton
    {
        private const string PART_ActionButton = "PART_ActionButton";

        #region Constructors

        static SplitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButton),
                new FrameworkPropertyMetadata(typeof(SplitButton)));
        }

        #endregion //Constructors

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Button = GetTemplateChild(PART_ActionButton) as Button;
        }

        #endregion //Base Class Overrides
    }
}