/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： TimeItem.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-18 10:31
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;

namespace Genew.ModernUI.ExtendedToolkit
{
    public class TimeItem
    {
        public TimeItem(string display, TimeSpan time)
        {
            Display = display;
            Time = time;
        }

        #region Base Class Overrides

        public override bool Equals(object obj)
        {
            var item = obj as TimeItem;
            if (item != null)
                return Time == item.Time;
            return false;
        }

        public override int GetHashCode()
        {
            return Time.GetHashCode();
        }

        #endregion //Base Class Overrides

        public string Display { get; set; }
        public TimeSpan Time { get; set; }
    }
}