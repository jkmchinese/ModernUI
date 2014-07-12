/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： ObjectCollection.cs
* 作   者： chenzhifen
* 创建日期： 2014-03-31 22:55
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace ModernUI.App.Common.SampleDatas
{
    public class ObjectCollection : Collection<Object>
    {
        public ObjectCollection()
        {
        }

        public ObjectCollection(IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }

            foreach (Object item in enumerable)
            {
                Add(item);
            }
        }
    }
}