using CommonUtil.DeepCopy.Interface;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CommonUtil.DeepCopy.Implement
{
    public class BinaryDeepCopyImpl : IDeepCopy
    {
        /// <summary>
        /// 通过二进制序列化实现深拷贝，两个对象互不影响
        /// 需要对象标记为[Serializable]，并且不能有非序列化的字段。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        public T DeepCopy<T>(T obj)
        {
            if (obj == null)
            {
                return default(T);
            }
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}