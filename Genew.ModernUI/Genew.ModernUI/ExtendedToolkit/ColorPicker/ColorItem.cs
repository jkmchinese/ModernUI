/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ColorItem.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-23 23:30
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows.Media;

namespace ModernUI.ExtendedToolkit
{
    public class ColorItem
    {
        public ColorItem(Color color, string name)
        {
            Color = color;
            Name = name;
        }

        public Color Color { get; set; }
        public string Name { get; set; }
    }
}