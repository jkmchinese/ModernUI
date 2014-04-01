/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： Department.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-31 22:55
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Samples;
using System.Windows.Markup;

namespace Genew.ModernUI.App.Common.SampleDatas
{
    /// <summary>
    ///     Represents a department in an organization.
    /// </summary>
    [ContentProperty("Divisions")]
    public class Department
    {
        /// <summary>
        ///     Initializes a new instance of the Department class.
        /// </summary>
        public Department()
        {
            Divisions = new Collection<Department>();
            Employees = new Collection<Employee>();
        }

        /// <summary>
        ///     Gets or sets the title of the department.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets a collection of employees in the department.
        /// </summary>
        public Collection<Employee> Employees { get; private set; }

        /// <summary>
        ///     Gets a collection of divisions inside the department.
        /// </summary>
        public Collection<Department> Divisions { get; private set; }

        /// <summary>
        ///     Gets a sample hierarchy of departments and employees.
        /// </summary>
        public static IEnumerable<Department> AllDepartments
        {
            get
            {
                IEnumerable<object> data =
                    Application.Current.Resources["DepartmentOrganization"] as IEnumerable<object>;
                return (data != null)
                    ? data.Cast<Department>()
                    : Enumerable.Empty<Department>();
            }
        }
    }
}