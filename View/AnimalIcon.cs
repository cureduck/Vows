using System;
using UnityEngine;
using Model;
using TMPro;

namespace View
{
    public class AnimalIcon:Icon<Animal>
    {
        public GameObject go;
        private Animal _animal;
        [SerializeField] private TMP_Text nameInput;

        protected override void UpdateUI()
        {
            nameInput.text = _animal.Name;
        }

        private void Awake()
        {
            _animal = go.GetComponent<Animal>();
            nameInput = GetComponent<TMP_Text>();
            UpdateUI();
        }

    }
}
