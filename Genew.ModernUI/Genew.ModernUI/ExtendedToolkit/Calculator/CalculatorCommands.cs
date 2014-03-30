/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： CalculatorCommands.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-29 17:31:29
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows.Input;

namespace Genew.ModernUI.ExtendedToolkit
{
    /// <summary>
    /// Calculator Commands
    /// </summary>
    internal class CalculatorCommands
    {
        private static readonly RoutedCommand _calculatorButtonClickCommand = new RoutedCommand();

        public static RoutedCommand CalculatorButtonClick
        {
            get
            {
                return _calculatorButtonClickCommand;
            }
        }
    }
}
