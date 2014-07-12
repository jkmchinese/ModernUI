/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： PropertyChangedEventHandler.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-11 23:50
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

namespace ModernUI.ExtendedToolkit
{
    public delegate void PropertyChangedEventHandler<T>(object sender, PropertyChangedEventArgs<T> e);
}