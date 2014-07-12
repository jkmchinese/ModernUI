/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： KeyDownCommandBehavior.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-11 19:05:00
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows;

namespace ModernUI.Behaviors
{
    /// <summary>
    /// 按键命令行为
    /// </summary>
    public class KeyDownCommandBehavior : Behavior<DependencyObject>
    {
        #region Override Methods

        protected override void OnAttached()
        {
            if (AssociatedObject is FrameworkElement)
            {
                var obj = AssociatedObject as FrameworkElement;
                BindEvent(obj);
            }
            else
            {
                throw new NotSupportedException("KeyDownCommandBehavior只支持附加到FrameworkElement");
            }
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            UnBindEvent(AssociatedObject as FrameworkElement);
            base.OnDetaching();
        }

        #endregion

        #region Dependency Properties
        /// <summary>
        /// 命令附加属性
        /// </summary>
        public static readonly DependencyProperty ExcuteCommandProperty = DependencyProperty.Register(
            "ExcuteCommand", typeof(ICommand), typeof(KeyDownCommandBehavior));
        /// <summary>
        /// 命令参数附加属性
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
        "CommandParameter", typeof(object), typeof(KeyDownCommandBehavior));
        /// <summary>
        /// Key属性
        /// </summary>
        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register(
            "Key", typeof(Key), typeof(KeyDownCommandBehavior));
        /// <summary>
        /// Modifier属性
        /// </summary>
        public static readonly DependencyProperty ModifierKeysProperty = DependencyProperty.Register(
            "ModifierKeys", typeof(ModifierKeys), typeof(KeyDownCommandBehavior));
        #endregion

        #region Properties
        /// <summary>
        /// 命令附加属性
        /// </summary>
        public ICommand ExcuteCommand
        {
            get
            {
                return (ICommand)GetValue(ExcuteCommandProperty);
            }
            set
            {
                SetValue(ExcuteCommandProperty, value);
            }
        }

        /// <summary>
        /// 命令附加属性
        /// </summary>
        public Key Key
        {
            get
            {
                return (Key)GetValue(KeyProperty);
            }
            set
            {
                SetValue(KeyProperty, value);
            }
        }

        /// <summary>
        /// 命令附加属性
        /// </summary>
        public ModifierKeys ModifierKeys
        {
            get
            {
                return (ModifierKeys)GetValue(ModifierKeysProperty);
            }
            set
            {
                SetValue(ModifierKeysProperty, value);
            }
        }

        /// <summary>
        /// 命令参数附加属性
        /// </summary>
        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        /// <summary>
        /// KeyDown标记
        /// </summary>
        public bool KeyDownFlag { get; set; }

        #endregion

        #region Event Handlers

        protected virtual void BindEvent(FrameworkElement element)
        {
            element.KeyDown += element_KeyDown;
            element.KeyUp += element_KeyUp;
        }

        protected virtual void UnBindEvent(FrameworkElement element)
        {
            element.KeyDown -= element_KeyDown;
            element.KeyUp -= element_KeyUp;
        }

        void element_KeyUp(object sender, KeyEventArgs e)
        {
            if (KeyDownFlag)
            {
                InvokeExcuteCommand();
                KeyDownFlag = false;
            }
        }

        void element_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key && Keyboard.Modifiers == ModifierKeys)
            {
                KeyDownFlag = true;
            }
        }

        #endregion

        #region Private Methods

        ///执行Binding的命令
        private void InvokeExcuteCommand()
        {
            if (ExcuteCommand != null)
            {
                ExcuteCommand.Execute(CommandParameter);
            }
        }

        #endregion
    }
}
