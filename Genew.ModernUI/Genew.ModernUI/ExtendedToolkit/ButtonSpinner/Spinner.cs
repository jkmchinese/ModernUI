/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： Spinner.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-17 20:45
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;

namespace ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     微调控件基类(Base class for controls that represents controls that can spin.)
    /// </summary>
    public abstract class Spinner : Control
    {
        #region Properties

        /// <summary>
        ///     Identifies the ValidSpinDirection dependency property.
        /// </summary>
        public static readonly DependencyProperty ValidSpinDirectionProperty =
            DependencyProperty.Register("ValidSpinDirection", typeof (ValidSpinDirections), typeof (Spinner),
                new PropertyMetadata(ValidSpinDirections.Increase | ValidSpinDirections.Decrease,
                    OnValidSpinDirectionPropertyChanged));

        /// <summary>
        /// 有效微调方向
        /// </summary>
        public ValidSpinDirections ValidSpinDirection
        {
            get { return (ValidSpinDirections) GetValue(ValidSpinDirectionProperty); }
            set { SetValue(ValidSpinDirectionProperty, value); }
        }

        /// <summary>
        ///     ValidSpinDirectionProperty property changed handler.
        /// </summary>
        /// <param name="d">ButtonSpinner that changed its ValidSpinDirection.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnValidSpinDirectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Spinner source = (Spinner) d;
            ValidSpinDirections oldvalue = (ValidSpinDirections) e.OldValue;
            ValidSpinDirections newvalue = (ValidSpinDirections) e.NewValue;
            source.OnValidSpinDirectionChanged(oldvalue, newvalue);
        }

        #endregion //Properties

        /// <summary>
        ///     当用户微调时触发该事件(Occurs when spinning is initiated by the end-user.)
        /// </summary>
        public event EventHandler<SpinEventArgs> Spin;

        /// <summary>
        ///     Raises the OnSpin event when spinning is initiated by the end-user.
        /// </summary>
        /// <param name="e">Spin event args.</param>
        protected virtual void OnSpin(SpinEventArgs e)
        {
            ValidSpinDirections valid = e.Direction == SpinDirection.Increase
                ? ValidSpinDirections.Increase
                : ValidSpinDirections.Decrease;

            //Only raise the event if spin is allowed.
            if ((ValidSpinDirection & valid) == valid)
            {
                EventHandler<SpinEventArgs> handler = Spin;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        /// <summary>
        ///     Called when valid spin direction changed.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnValidSpinDirectionChanged(ValidSpinDirections oldValue, ValidSpinDirections newValue)
        {
        }
    }
}