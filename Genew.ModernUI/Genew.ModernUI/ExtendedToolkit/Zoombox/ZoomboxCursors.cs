/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ZoomboxCursors.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-11 23:43
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input;
using Genew.ModernUI.ExtendedToolkit.Utilities;

namespace Genew.ModernUI.ExtendedToolkit
{
    public class ZoomboxCursors
    {
        #region Constructors

        static ZoomboxCursors()
        {
            try
            {
                new EnvironmentPermission(PermissionState.Unrestricted).Demand();
                _zoom =
                    new Cursor(ResourceHelper.LoadResourceStream(Assembly.GetExecutingAssembly(),
                        "ExtendedToolkit/Zoombox/Resources/Zoom.cur"));
                _zoomRelative =
                    new Cursor(ResourceHelper.LoadResourceStream(Assembly.GetExecutingAssembly(),
                        "ExtendedToolkit/Zoombox/Resources/ZoomRelative.cur"));
            }
            catch (SecurityException)
            {
                // partial trust, so just use default cursors
            }
        }

        #endregion

        #region Zoom Static Property

        private static readonly Cursor _zoom = Cursors.Arrow;

        public static Cursor Zoom
        {
            get { return _zoom; }
        }

        #endregion

        #region ZoomRelative Static Property

        private static readonly Cursor _zoomRelative = Cursors.Arrow;

        public static Cursor ZoomRelative
        {
            get { return _zoomRelative; }
        }

        #endregion
    }
}