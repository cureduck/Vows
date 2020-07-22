using System;
using UnityEngine;
using Model;
using System.Linq;
using System.Collections.Generic;
using Utils;

namespace Manager
{
    class StrucManager:Singleton<StrucManager>
    {
        public Dictionary<string, GameObject> struMap;
        public GameObject[] items;

        private void Awake()
        {
            RegisterItems();
        }

        private void RegisterItems()
        {
            var resources = Resources.LoadAll<GameObject>("Prefabs/Structures");
            var types = Utils.GetSubClasses(typeof(Stru));
            struMap = new Dictionary<string, GameObject>();
            
            items = new GameObject[types.Length];

            var i = 0;
            foreach (var type in types)
            {
                var go = resources.First((s) => { return s.name == type.Name; });
                struMap.Add(go.name, go);
                items[i] = go;
                i++;
            }
        }

    }
}
