/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： Photograph.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-31 22:55
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Genew.ModernUI.App.Common;

namespace System.Windows.Controls.Samples
{
    /// <summary>
    ///     Photograph business object used in examples.
    /// </summary>
    public sealed class Photograph
    {
        /// <summary>
        ///     Initializes a new instance of the Photograph class.
        /// </summary>
        /// <param name="resourceName">
        ///     Name of the resource defining the photograph.
        /// </param>
        internal Photograph(string resourceName)
        {
            Name = resourceName;
            Image = SharedResources.GetImage(resourceName);
        }

        /// <summary>
        ///     Gets the name of the Photograph.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets an Image control containing the Photograph.
        /// </summary>
        public Image Image { get; private set; }

        /// <summary>
        ///     Overrides the string to return the name.
        /// </summary>
        /// <returns>Returns the photograph name.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Get all of the photographs defined in the assembly as embedded
        ///     resources.
        /// </summary>
        /// <returns>
        ///     All of the photographs defined in the assembly as embedded
        ///     resources.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Doing more work than a property should")]
        public static IEnumerable<Photograph> GetPhotographs()
        {
            foreach (string resourceName in SharedResources.GetImageNames())
            {
                yield return new Photograph(resourceName);
            }
        }
    }
}