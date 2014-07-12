/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： BindingEvaluator.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-31 0:36
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows;
using System.Windows.Data;

namespace ModernUI.ExtendedToolkit.Utilities
{
    /// <summary>
    ///     A framework element that permits a binding to be evaluated in a new data
    ///     context leaf node.
    /// </summary>
    /// <typeparam name="T">The type of dynamic binding to return.</typeparam>
    internal class BindingEvaluator<T> : FrameworkElement
    {
        /// <summary>
        ///     Gets or sets the string value binding used by the control.
        /// </summary>
        private Binding _binding;

        #region public T Value

        /// <summary>
        ///     Identifies the Value dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(T),
                typeof(BindingEvaluator<T>),
                new PropertyMetadata(default(T)));

        /// <summary>
        ///     Gets or sets the data item string value.
        /// </summary>
        public T Value
        {
            get { return (T)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        #endregion public string Value

        /// <summary>
        ///     Initializes a new instance of the BindingEvaluator class.
        /// </summary>
        public BindingEvaluator()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the BindingEvaluator class,
        ///     setting the initial binding to the provided parameter.
        /// </summary>
        /// <param name="binding">The initial string value binding.</param>
        public BindingEvaluator(Binding binding)
        {
            SetBinding(ValueProperty, binding);
        }

        /// <summary>
        ///     Gets or sets the value binding.
        /// </summary>
        public Binding ValueBinding
        {
            get { return _binding; }
            set
            {
                _binding = value;
                SetBinding(ValueProperty, _binding);
            }
        }

        /// <summary>
        ///     Clears the data context so that the control does not keep a
        ///     reference to the last-looked up item.
        /// </summary>
        public void ClearDataContext()
        {
            DataContext = null;
        }

        /// <summary>
        ///     Updates the data context of the framework element and returns the
        ///     updated binding value.
        /// </summary>
        /// <param name="o">The object to use as the data context.</param>
        /// <param name="clearDataContext">
        ///     If set to true, this parameter will
        ///     clear the data context immediately after retrieving the value.
        /// </param>
        /// <returns>
        ///     Returns the evaluated T value of the bound dependency
        ///     property.
        /// </returns>
        public T GetDynamicValue(object o, bool clearDataContext)
        {
            DataContext = o;
            T value = Value;
            if (clearDataContext)
            {
                DataContext = null;
            }
            return value;
        }

        /// <summary>
        ///     Updates the data context of the framework element and returns the
        ///     updated binding value.
        /// </summary>
        /// <param name="o">The object to use as the data context.</param>
        /// <returns>
        ///     Returns the evaluated T value of the bound dependency
        ///     property.
        /// </returns>
        public T GetDynamicValue(object o)
        {
            DataContext = o;
            return Value;
        }
    }
}