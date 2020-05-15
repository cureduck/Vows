using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Model;
using System.Reflection;
using UnityEngine.Serialization;

namespace Manager
{
    /// <summary>
    /// 通过反射，将物品基类Item的所有子类都进行注册，绑定图片和文本等内容
    /// </summary>
    internal class ItemManager:Singleton<ItemManager>
    {
        [SerializeField]
        public Item[] items;

        private void Start()
        {
            RegisterItems();
        }

        private void RegisterItems()
        {
            var resources = Resources.LoadAll<Sprite>("Sprites/");
            var types = Utils.GetSubClasses(typeof(Item));

            items = new Item[types.Length];
            var i = 0;
            foreach (var type in types)
            {
                var sample = (Item)System.Activator.CreateInstance(type);
                var sprite = resources.First((s) => s.name == type.Name);
                type.GetProperty("ItemSprite")?.SetValue(sample, sprite);
                items[i] = sample;
                i++;
            }
        }

    }
}
