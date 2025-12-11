using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.DeepCopy.Interface
{
    public interface IDeepCopy
    {
        /// <summary>
        /// 深拷贝，两个对象互不影响
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        T DeepCopy<T>(T obj);
    }
}
