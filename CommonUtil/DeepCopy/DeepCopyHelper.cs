using CommonUtil.DeepCopy.Interface;
using CommonUtil.DeepCopy.Implement;

namespace CommonUtil.DeepCopy
{
    /// <summary>
    /// 深拷贝助手类，提供静态方法简化深拷贝操作
    /// </summary>
    public static class DeepCopyHelper
    {
        private static readonly IDeepCopy _deepCopyHandler = new TextJsonDeepCopyImpl();

        /// <summary>
        /// 深拷贝，两个对象互不影响
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        public static T DeepCopy<T>(T obj)
        {
            return _deepCopyHandler.DeepCopy(obj);
        }
    }
}