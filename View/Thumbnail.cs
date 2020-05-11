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
            hpSlider.value =  (float) animal.curHp / animal.maxHp;
            Debug.Log(hpSlider.value.GetType());
        }


    }
}
