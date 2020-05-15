using System;
using UnityEngine;
using Model;
using System.Linq;
using System.Collections.Generic;

namespace Manager
{
    class StrucManager:Singleton<StrucManager>
    {
        public Dictionary<string, GameObject> struMap;
        public GameObject[] items;

        private void Start()
        {
            RegisterItems();
        }

        private void RegisterItems()
        {
            var resources = Resources.LoadAll<GameObject>("Prefabs/Structures");
            var types = Utils.GetSubClasses(typeof(Structure));
            struMap = new Dictionary<string, GameObject>();
            
            items = new GameObject[types.Length];

            int i = 0;
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
