using System;
using UnityEngine;
using Model;
using TMPro;

namespace View
{
    public class AnimalIcon:Icon<Animal>
    {
        public GameObject go;
        Animal animal;
        TMP_Text tMP;

        protected override void UpdateUI()
        {
            tMP.text = animal.Name;
        }

        void Awake()
        {
            animal = go.GetComponent<Animal>();
            tMP = GetComponent<TMP_Text>();
            UpdateUI();
        }

    }
}
