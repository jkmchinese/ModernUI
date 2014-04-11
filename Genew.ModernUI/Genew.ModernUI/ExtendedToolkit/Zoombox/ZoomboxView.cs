/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ZoomboxView.cs
* 作   者： chenzhifen
* 创建日期： 2014-04-11 23:43
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.ComponentModel;
using System.Windows;
using Genew.ModernUI.ExtendedToolkit.Utilities;

namespace Genew.ModernUI.ExtendedToolkit
{
    [TypeConverter(typeof(ZoomboxViewConverter))]
    public class ZoomboxView
    {
        #region Constructors

        public ZoomboxView()
        {
        }

        public ZoomboxView(double scale)
        {
            Scale = scale;
        }

        public ZoomboxView(Point position)
        {
            Position = position;
        }

        public ZoomboxView(double scale, Point position)
        {
            Position = position;
            Scale = scale;
        }

        public ZoomboxView(Rect region)
        {
            Region = region;
        }

        public ZoomboxView(double x, double y)
            : this(new Point(x, y))
        {
        }

        public ZoomboxView(double scale, double x, double y)
            : this(scale, new Point(x, y))
        {
        }

        public ZoomboxView(double x, double y, double width, double height)
            : this(new Rect(x, y, width, height))
        {
        }

        #endregion

        #region Empty Static Property

        private static readonly ZoomboxView _empty = new ZoomboxView(ZoomboxViewKind.Empty);

        public static ZoomboxView Empty
        {
            get { return _empty; }
        }

        #endregion

        #region Fill Static Property

        private static readonly ZoomboxView _fill = new ZoomboxView(ZoomboxViewKind.Fill);

        public static ZoomboxView Fill
        {
            get { return _fill; }
        }

        #endregion

        #region Fit Static Property

        private static readonly ZoomboxView _fit = new ZoomboxView(ZoomboxViewKind.Fit);

        public static ZoomboxView Fit
        {
            get { return _fit; }
        }

        #endregion

        #region Center Static Property

        private static readonly ZoomboxView _center = new ZoomboxView(ZoomboxViewKind.Center);

        public static ZoomboxView Center
        {
            get { return _center; }
        }

        #endregion

        #region ViewKind Property

        private double _kindHeight = (int)ZoomboxViewKind.Empty;

        public ZoomboxViewKind ViewKind
        {
            get
            {
                if (_kindHeight > 0)
                {
                    return ZoomboxViewKind.Region;
                }
                return (ZoomboxViewKind)(int)_kindHeight;
            }
        }

        #endregion

        #region Position Property

        private double _x = double.NaN;
        private double _y = double.NaN;

        public Point Position
        {
            get
            {
                if (ViewKind != ZoomboxViewKind.Absolute)
                    throw new InvalidOperationException(ErrorMessages.GetMessage("PositionOnlyAccessibleOnAbsolute"));

                return new Point(_x, _y);
            }
            set
            {
                if (ViewKind != ZoomboxViewKind.Absolute && ViewKind != ZoomboxViewKind.Empty)
                    throw new InvalidOperationException(
                        String.Format(ErrorMessages.GetMessage("ZoomboxViewAlreadyInitialized"), ViewKind.ToString()));

                _x = value.X;
                _y = value.Y;
                _kindHeight = (int)ZoomboxViewKind.Absolute;
            }
        }

        #endregion

        #region Scale Property

        private double _scaleWidth = double.NaN;

        public double Scale
        {
            get
            {
                if (ViewKind != ZoomboxViewKind.Absolute)
                    throw new InvalidOperationException(ErrorMessages.GetMessage("ScaleOnlyAccessibleOnAbsolute"));

                return _scaleWidth;
            }
            set
            {
                if (ViewKind != ZoomboxViewKind.Absolute && ViewKind != ZoomboxViewKind.Empty)
                    throw new InvalidOperationException(
                        String.Format(ErrorMessages.GetMessage("ZoomboxViewAlreadyInitialized"), ViewKind.ToString()));

                _scaleWidth = value;
                _kindHeight = (int)ZoomboxViewKind.Absolute;
            }
        }

        #endregion

        #region Region Property

        public Rect Region
        {
            get
            {
                // a region view has a positive _typeHeight value
                if (_kindHeight < 0)
                    throw new InvalidOperationException(ErrorMessages.GetMessage("RegionOnlyAccessibleOnRegionalView"));

                return new Rect(_x, _y, _scaleWidth, _kindHeight);
            }
            set
            {
                if (ViewKind != ZoomboxViewKind.Region && ViewKind != ZoomboxViewKind.Empty)
                    throw new InvalidOperationException(
                        String.Format(ErrorMessages.GetMessage("ZoomboxViewAlreadyInitialized"), ViewKind.ToString()));

                if (!value.IsEmpty)
                {
                    _x = value.X;
                    _y = value.Y;
                    _scaleWidth = value.Width;
                    _kindHeight = value.Height;
                }
            }
        }

        #endregion

        private ZoomboxView(ZoomboxViewKind viewType)
        {
            _kindHeight = (int)viewType;
        }

        #region Operators Methods

        public static bool operator ==(ZoomboxView v1, ZoomboxView v2)
        {
            if ((object)v1 == null)
                return (object)v2 == null;

            if ((object)v2 == null)
                return (object)v1 == null;

            return v1.Equals(v2);
        }

        public static bool operator !=(ZoomboxView v1, ZoomboxView v2)
        {
            return !(v1 == v2);
        }

        #endregion

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode() ^ _scaleWidth.GetHashCode() ^ _kindHeight.GetHashCode();
        }

        public override bool Equals(object o)
        {
            bool result = false;
            if (o is ZoomboxView)
            {
                ZoomboxView other = (ZoomboxView)o;
                if (ViewKind == other.ViewKind)
                {
                    switch (ViewKind)
                    {
                        case ZoomboxViewKind.Absolute:
                            result = (DoubleHelper.AreVirtuallyEqual(_scaleWidth, other._scaleWidth))
                                     && (DoubleHelper.AreVirtuallyEqual(Position, other.Position));
                            break;

                        case ZoomboxViewKind.Region:
                            result = DoubleHelper.AreVirtuallyEqual(Region, other.Region);
                            break;

                        default:
                            result = true;
                            break;
                    }
                }
            }
            return result;
        }

        public override string ToString()
        {
            switch (ViewKind)
            {
                case ZoomboxViewKind.Empty:
                    return "ZoomboxView: Empty";

                case ZoomboxViewKind.Center:
                    return "ZoomboxView: Center";

                case ZoomboxViewKind.Fill:
                    return "ZoomboxView: Fill";

                case ZoomboxViewKind.Fit:
                    return "ZoomboxView: Fit";

                case ZoomboxViewKind.Absolute:
                    return string.Format("ZoomboxView: Scale = {0}; Position = ({1}, {2})", _scaleWidth.ToString("f"),
                        _x.ToString("f"), _y.ToString("f"));

                case ZoomboxViewKind.Region:
                    return string.Format("ZoomboxView: Region = ({0}, {1}, {2}, {3})", _x.ToString("f"),
                        _y.ToString("f"), _scaleWidth.ToString("f"), _kindHeight.ToString("f"));
            }

            return base.ToString();
        }
    }
}