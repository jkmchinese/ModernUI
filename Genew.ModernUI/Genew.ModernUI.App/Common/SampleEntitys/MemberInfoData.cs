/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： MemberInfoData.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-31 22:55
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace Genew.ModernUI.App.Common.SampleDatas
{
    /// <summary>
    ///     A wrapper type that can be used for visually displaying reflected type
    ///     information.
    /// </summary>
    public class MemberInfoData
    {
        /// <summary>
        ///     Initializes a new instance of the MemberInfoData class
        ///     with the provided MemberInfo object's data.
        /// </summary>
        /// <param name="mi">The member info object.</param>
        public MemberInfoData(MemberInfo mi)
        {
            MemberInfo = mi;
            Name = mi.Name;

            if (mi is PropertyInfo)
            {
                IconName = "Property.png";
            }

            MethodInfo methodInfo = mi as MethodInfo;
            if (methodInfo != null)
            {
                IconName = methodInfo.IsStatic ? "Static.png" : "Method.png";
            }

            if (mi is EventInfo)
            {
                IconName = "Event.png";
            }

            FieldInfo field = mi as FieldInfo;
            if (field != null)
            {
                IconName = "Static.png";
            }
        }

        /// <summary>
        ///     Gets the member information object.
        /// </summary>
        public MemberInfo MemberInfo { get; private set; }

        /// <summary>
        ///     Gets or sets the icon string name.
        /// </summary>
        private string IconName { get; set; }

        /// <summary>
        ///     Gets the icon.
        /// </summary>
        public Image Icon
        {
            get { return IconName == null ? null : SharedResources.GetIcon(IconName); }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the foreground color based on method properties.
        /// </summary>
        public Brush ForegroundColor
        {
            get
            {
                Color c = MemberInfo.DeclaringType == MemberInfo.ReflectedType ? Colors.Black : Colors.DarkGray;
                return new SolidColorBrush(c);
            }
        }

        /// <summary>
        ///     Gets an enumerable set of PME information objects.
        /// </summary>
        /// <param name="type">The type to reflect over.</param>
        /// <returns>Returns the set of MemberInfoData objects.</returns>
        public static IEnumerable<MemberInfoData> GetSetForType(Type type)
        {
            MemberInfo[] members = type.GetMembers();
            if (members.Length > 0)
            {
                foreach (MemberInfo member in members)
                {
                    if (member.Name.Contains("get_") || member.Name.Contains("set_") || member.Name.Contains("add_") ||
                        member.Name.Contains("remove_"))
                    {
                        continue;
                    }

                    MemberInfoData pme = new MemberInfoData(member);
                    yield return pme;
                }
            }
            else
            {
            }
        }

        /// <summary>
        ///     Overrides the ToString method to display the name.
        /// </summary>
        /// <returns>Returns the name as a string.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}