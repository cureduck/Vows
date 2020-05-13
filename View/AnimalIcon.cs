using System;
using UnityEngine;
using Model;
using TMPro;

namespace View
{
    public class AnimalIcon:Icon<Animal>
    {
        Animal animal;
        TMP_Text tMP;

        void Awake()
        {
            tMP = GetComponent<TMP_Text>();
            tMP.text = animal.name;
        }

    }
}
