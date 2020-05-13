using System;
using UnityEngine;
using UnityEngine.UI;
using Model;

namespace View
{
    class StatusLogger : MonoBehaviour
    {
        Animal animal;
        Slider hpBar;
        Slider spBar;
        Slider proBar;

        private void Start()
        {
            animal = GetComponentInParent<Animal>();
            var Bars = GetComponentsInChildren<Slider>();
            hpBar = Bars[0];
            spBar = Bars[1];
            proBar = Bars[2];

            UpdateView();
            animal.StatusUpdated += UpdateView;
        }

        private void UpdateView()
        {
            var tmp = animal.combatAttr;
            hpBar.value =(float) tmp.curHp / tmp.maxHp;
            spBar.value = (float)tmp.curSp / tmp.maxSp;
        }

        private void UpdateProgress(float progress)
        {
            switch (progress)
            {
                case 0:
                    proBar.gameObject.SetActive(false);
                    break;
                case 1:
                    proBar.gameObject.SetActive(false);
                    break;
                default:
                    proBar.gameObject.SetActive(true);
                    proBar.value = progress;
                    break;
            }
        }

    }
}
