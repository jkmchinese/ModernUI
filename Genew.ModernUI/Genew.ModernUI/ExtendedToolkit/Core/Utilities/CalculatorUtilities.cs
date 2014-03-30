/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： CalculatorUtilities.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-29 17:22:33
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Genew.ModernUI.ExtendedToolkit.Utilities
{
    /// <summary>
    /// Calculator Utilities
    /// </summary>
    internal static class CalculatorUtilities
    {
        public static CalculatorButtonType GetCalculatorButtonTypeFromText(string text)
        {
            switch (text)
            {
                case "0":
                    return CalculatorButtonType.Zero;
                case "1":
                    return CalculatorButtonType.One;
                case "2":
                    return CalculatorButtonType.Two;
                case "3":
                    return CalculatorButtonType.Three;
                case "4":
                    return CalculatorButtonType.Four;
                case "5":
                    return CalculatorButtonType.Five;
                case "6":
                    return CalculatorButtonType.Six;
                case "7":
                    return CalculatorButtonType.Seven;
                case "8":
                    return CalculatorButtonType.Eight;
                case "9":
                    return CalculatorButtonType.Nine;
                case "+":
                    return CalculatorButtonType.Add;
                case "-":
                    return CalculatorButtonType.Subtract;
                case "*":
                    return CalculatorButtonType.Multiply;
                case "/":
                    return CalculatorButtonType.Divide;
                case "%":
                    return CalculatorButtonType.Percent;
                case "\b":
                    return CalculatorButtonType.Back;
                case "\r":
                case "=":
                    return CalculatorButtonType.Equal;
            }

            //the check for the decimal is not in the switch statement. To help localize we check against the current culture's decimal seperator
            if (text == CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)
                return CalculatorButtonType.Decimal;

            //check for the escape key
            if (text == ((char)27).ToString())
                return CalculatorButtonType.Clear;

            return CalculatorButtonType.None;
        }

        public static Button FindButtonByCalculatorButtonType(DependencyObject parent, CalculatorButtonType type)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                object buttonType = child.GetValue(Button.CommandParameterProperty);

                if (buttonType != null && (CalculatorButtonType)buttonType == type)
                {
                    return child as Button;
                }
                else
                {
                    var result = FindButtonByCalculatorButtonType(child, type);

                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        public static string GetCalculatorButtonContent(CalculatorButtonType type)
        {
            string content = string.Empty;
            switch (type)
            {
                case CalculatorButtonType.Add:
                    content = "+";
                    break;
                case CalculatorButtonType.Back:
                    content = "Back";
                    break;
                case CalculatorButtonType.Cancel:
                    content = "CE";
                    break;
                case CalculatorButtonType.Clear:
                    content = "C";
                    break;
                case CalculatorButtonType.Decimal:
                    content = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                    break;
                case CalculatorButtonType.Divide:
                    content = "/";
                    break;
                case CalculatorButtonType.Eight:
                    content = "8";
                    break;
                case CalculatorButtonType.Equal:
                    content = "=";
                    break;
                case CalculatorButtonType.Five:
                    content = "5";
                    break;
                case CalculatorButtonType.Four:
                    content = "4";
                    break;
                case CalculatorButtonType.Fraction:
                    content = "1/x";
                    break;
                case CalculatorButtonType.MAdd:
                    content = "M+";
                    break;
                case CalculatorButtonType.MC:
                    content = "MC";
                    break;
                case CalculatorButtonType.MR:
                    content = "MR";
                    break;
                case CalculatorButtonType.MS:
                    content = "MS";
                    break;
                case CalculatorButtonType.MSub:
                    content = "M-";
                    break;
                case CalculatorButtonType.Multiply:
                    content = "*";
                    break;
                case CalculatorButtonType.Nine:
                    content = "9";
                    break;
                case CalculatorButtonType.None:
                    break;
                case CalculatorButtonType.One:
                    content = "1";
                    break;
                case CalculatorButtonType.Percent:
                    content = "%";
                    break;
                case CalculatorButtonType.Seven:
                    content = "7";
                    break;
                case CalculatorButtonType.Negate:
                    content = "+/-";
                    break;
                case CalculatorButtonType.Six:
                    content = "6";
                    break;
                case CalculatorButtonType.Sqrt:
                    content = "Sqrt";
                    break;
                case CalculatorButtonType.Subtract:
                    content = "-";
                    break;
                case CalculatorButtonType.Three:
                    content = "3";
                    break;
                case CalculatorButtonType.Two:
                    content = "2";
                    break;
                case CalculatorButtonType.Zero:
                    content = "0";
                    break;
            }
            return content;
        }

        public static bool IsDigit(CalculatorButtonType buttonType)
        {
            switch (buttonType)
            {
                case CalculatorButtonType.Zero:
                case CalculatorButtonType.One:
                case CalculatorButtonType.Two:
                case CalculatorButtonType.Three:
                case CalculatorButtonType.Four:
                case CalculatorButtonType.Five:
                case CalculatorButtonType.Six:
                case CalculatorButtonType.Seven:
                case CalculatorButtonType.Eight:
                case CalculatorButtonType.Nine:
                case CalculatorButtonType.Decimal:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsMemory(CalculatorButtonType buttonType)
        {
            switch (buttonType)
            {
                case CalculatorButtonType.MAdd:
                case CalculatorButtonType.MC:
                case CalculatorButtonType.MR:
                case CalculatorButtonType.MS:
                case CalculatorButtonType.MSub:
                    return true;
                default:
                    return false;
            }
        }

        public static decimal ParseDecimal(string text)
        {
            return Decimal.Parse(text, CultureInfo.CurrentCulture);
        }

        public static decimal Add(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber + secondNumber;
        }

        public static decimal Subtract(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber - secondNumber;
        }

        public static decimal Multiply(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber * secondNumber;
        }

        public static decimal Divide(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber / secondNumber;
        }

        public static decimal Percent(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber * secondNumber / 100M;
        }

        public static decimal SquareRoot(decimal operand)
        {
            return Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(operand)));
        }

        public static decimal Fraction(decimal operand)
        {
            return 1 / operand;
        }

        public static decimal Negate(decimal operand)
        {
            return operand * -1M;
        }
    }
}
