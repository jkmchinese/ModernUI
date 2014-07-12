/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： City.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-31 22:55
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows.Controls.Samples;

namespace ModernUI.App.Common.SampleDatas
{
    /// <summary>
    ///     City business object used for charting samples.
    /// </summary>
    public class City
    {
        /// <summary>
        ///     Gets or sets the name of the city.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the population of the city.
        /// </summary>
        public int Population { get; set; }

        /// <summary>
        ///     Gets a collection of cities in the Puget Sound area.
        /// </summary>
        public static ObjectCollection PugetSound
        {
            get
            {
                ObjectCollection pugetSound = new ObjectCollection();
                pugetSound.Add(new City { Name = "Bellevue", Population = 112344 });
                pugetSound.Add(new City { Name = "Issaquah", Population = 11212 });
                pugetSound.Add(new City { Name = "Redmond", Population = 46391 });
                pugetSound.Add(new City { Name = "Seattle", Population = 592800 });
                return pugetSound;
            }
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}