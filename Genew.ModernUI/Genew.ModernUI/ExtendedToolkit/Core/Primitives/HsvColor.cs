/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： HsvColor.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-23 9:49
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

namespace Genew.ModernUI.ExtendedToolkit.Primitives
{
    internal struct HsvColor
    {
        public double H;
        public double S;
        public double V;

        public HsvColor(double h, double s, double v)
        {
            H = h;
            S = s;
            V = v;
        }
    }
}