/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： Feature.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-31 22:55
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Markup;

namespace ModernUI.App.Common.SampleDatas
{
    /// <summary>
    ///     Represents a feature that can be installed.
    /// </summary>
    [ContentProperty("Subcomponents")]
    public class Feature : INotifyPropertyChanged
    {
        /// <summary>
        ///     Backing variable for the ShouldInstall property.
        /// </summary>
        private bool? _shouldInstall;

        /// <summary>
        ///     Initializes a new instance of the Feature class.
        /// </summary>
        public Feature()
        {
            Subcomponents = new Collection<Feature>();
            ShouldInstall = true;
        }

        /// <summary>
        ///     Gets or sets the name of the feature.
        /// </summary>
        public string FeatureName { get; set; }

        /// <summary>
        ///     Gets or sets the description of the feature.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets a collection of sub-components that make up the feature.
        /// </summary>
        public Collection<Feature> Subcomponents { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the feature has subcomponents.
        /// </summary>
        public bool HasSubcomponents
        {
            get { return Subcomponents.Count > 0; }
        }

        /// <summary>
        ///     Gets or sets whether the feature should be installed.
        /// </summary>
        public bool? ShouldInstall
        {
            get { return _shouldInstall; }
            set
            {
                if (value != _shouldInstall)
                {
                    _shouldInstall = value;
                    OnPropertyChanged("ShouldInstall");
                }
            }
        }

        /// <summary>
        ///     Implements the INotifyPropertyChanged interface.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Fires the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Property that changed.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}