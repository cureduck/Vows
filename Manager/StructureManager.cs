using System;
using UnityEngine;
using Model;
using System.Linq;
using System.Collections.Generic;

namespace Manager
{
    class StructureManager:Singleton<StructureManager>
    {
        public Dictionary<string, GameObject> struMap;

        private void Start()
        {
            RegisterItems();
        }

        public void RegisterItems()
        {
            var resources = Resources.LoadAll<GameObject>("Prefabs/Structures");
            var types = Utils.GetSubClasses(typeof(Structure));
            struMap = new Dictionary<string, GameObject>();
            
            var items = new GameObject[types.Length];
            foreach (var type in types)
            {
                var go = resources.First((s) => { return s.name == type.Name; });
                struMap.Add(go.name, go);
            }
        }

    }
}
