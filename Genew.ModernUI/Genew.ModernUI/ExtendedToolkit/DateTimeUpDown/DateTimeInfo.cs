/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： DateTimeInfo.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-18 10:30
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

namespace Genew.ModernUI.ExtendedToolkit
{
    internal class DateTimeInfo
    {
        public string Content { get; set; }
        public string Format { get; set; }
        public bool IsReadOnly { get; set; }
        public int Length { get; set; }
        public int StartPosition { get; set; }
        public DateTimePart Type { get; set; }
    }
}