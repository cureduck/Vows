using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Utils
{
    public static class Tools
    {
        /// <summary>
        /// 获取程序集内所有该父类的子类
        /// </summary>
        /// <param name="basetype"></param>
        /// <returns></returns>
        public static Type[] GetSubClasses(Type basetype)
        {
            var types = Assembly.GetCallingAssembly().GetTypes().Where((type) => (type.IsSubclassOf(basetype) && (!type.IsAbstract)));
            return types.ToArray();
        }

        public static Vector2Int Vec2Int(Vector2 vec)
        {
            return new Vector2Int((int)Math.Floor(vec.x),(int)Math.Floor(vec.y));
        }
    }
}
