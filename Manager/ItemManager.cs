using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Model;
using System.Reflection;

namespace Manager
{
    /// <summary>
    /// 通过反射，将物品基类Item的所有子类都进行注册，绑定图片和文本等内容
    /// </summary>
    class ItemManager:Singleton<ItemManager>
    {
        [SerializeField]
        public Item[] items;

        private void Start()
        {
            RegisterItems();
        }

        public void RegisterItems()
        {
            var resources = Resources.LoadAll<Sprite>("Sprites/");
            var types = Utils.GetSubClasses(typeof(Item));

            items = new Item[types.Length];
            int i = 0;
            foreach (var type in types)
            {
                var sample = (Item)System.Activator.CreateInstance(type);
                var sprite = resources.First((s) => { return s.name == type.Name; });
                type.GetProperty("ItemSprite").SetValue(sample, sprite);
                items[i] = sample;
                i++;
            }
        }

    }


    [CreateAssetMenu(fileName ="asdfsf/asdfsf",menuName ="asdfsdf")]
    public class ItemData : ScriptableObject
    {
        public string _spriteName; 
    }
}
