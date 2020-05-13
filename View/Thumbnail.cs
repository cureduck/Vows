using System;
using UnityEngine;
using Model;
using UnityEngine.UI;
using Manager;

namespace View
{
    class Thumbnail:MonoBehaviour
    {

        public Slider hpSlider;
        public Animal animal;

        public void Start()
        {
            animal = GameManager.Instance.player;
            Debug.Log(hpSlider.value.GetType());
            animal.StatusUpdated += UpdateUI;
            hpSlider = GetComponentInChildren<Slider>();
        }

        public void UpdateUI()
        {
            hpSlider.value = (float)animal.combatAttr.curHp / animal.combatAttr.maxHp;
        }

    }
}
