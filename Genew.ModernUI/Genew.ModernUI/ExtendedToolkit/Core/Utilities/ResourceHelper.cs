/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ResourceHelper.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-11 23:55
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.IO;
using System.Reflection;
using System.Resources;

namespace Genew.ModernUI.ExtendedToolkit.Utilities
{
    internal class ResourceHelper
    {
        internal static Stream LoadResourceStream(Assembly assembly, string resId)
        {
            string basename = System.IO.Path.GetFileNameWithoutExtension(assembly.ManifestModule.Name) + ".g";
            ResourceManager resourceManager = new ResourceManager(basename, assembly);

            // resource names are lower case and contain only forward slashes
            resId = resId.ToLower();
            resId = resId.Replace('\\', '/');
            return (resourceManager.GetObject(resId) as Stream);
        }
    }
}