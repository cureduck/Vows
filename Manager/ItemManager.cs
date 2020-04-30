using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Model;


namespace Manager
{
    class ItemManager:MonoBehaviour
    {
        public ItemBase[] items;

        private void Start()
        {
            var resources = Resources.LoadAll<Sprite>("Texture/");
            BigPotion.Set(resources.First((s)=> { return s.name == "Red Potion";}));
        }
    }


    [CreateAssetMenu(fileName ="asdfsf/asdfsf",menuName ="asdfsdf")]
    public class ItemData : ScriptableObject
    {
        public string _spriteName; 
    }
}
