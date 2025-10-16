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
        /// 通过TextJSON实现深拷贝，两个对象互不影响
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        T DeepCopyObjectByTextJson<T>(T obj);

        /// <summary>
        /// 使用Newtonsoft.Json实现的深拷贝
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        T DeepCopyObjectByNewtonsoftJson<T>(T obj);

        /// <summary>
        /// 通过二进制序列化实现深拷贝，两个对象互不影响
        /// 需要对象标记为[Serializable]，并且不能有非序列化的字段。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        T DeepCopyObjectByBinary<T>(T obj);
    }
}
