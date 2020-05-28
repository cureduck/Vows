using System;
using Manager;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class BackpackView : ListTemplate<Animal, Item, ItemIcon>
    {
        protected override Item[] components => value.backpack;

        private void Start()
        {
            value = GameManager.Instance.player;
            value.BackpackChanged += UpdateUI;
        }
    }
}
