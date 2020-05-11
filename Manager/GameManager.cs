using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using System;
using System.Reflection;
using System.Linq;

namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public Animal player { get; private set; }

        private void Awake()
        {
            player = GameObject.Find("player").GetComponent<Animal>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public static class Utils
    {
        /// <summary>
        /// 获取程序集内所有该父类的子类
        /// </summary>
        /// <param name="basetype"></param>
        /// <returns></returns>
        public static Type[] GetSubClasses(Type basetype)
        {
            var types = Assembly.GetCallingAssembly().GetTypes().Where((type) =>
            {
                return (type.IsSubclassOf(basetype) && (!type.IsAbstract));
            });
            return types.ToArray();
        }
    }

}
