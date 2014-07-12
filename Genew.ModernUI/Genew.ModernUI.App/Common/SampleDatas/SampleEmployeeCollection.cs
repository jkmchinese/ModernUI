/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： SampleEmployeeCollection.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-31 23:22
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows.Controls.Samples;

namespace ModernUI.App.Common.SampleDatas
{
    /// <summary>
    ///     A collection type that makes it easy to place sample employee data into
    ///     XAML.
    /// </summary>
    public class SampleEmployeeCollection : ObjectCollection
    {
        /// <summary>
        ///     Initializes a new instance of the SampleEmployeeCollection class.
        /// </summary>
        public SampleEmployeeCollection()
            : base(Employee.AllExecutives)
        {
        }
    }
}