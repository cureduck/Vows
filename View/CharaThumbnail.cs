using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.UI;
using TMPro;

namespace View
{
    public class CharaThumbnail : MonoBehaviour
    {
        public Animal owner;

        private Slider hpSlider;
        private Slider spSlider;
        private TMP_Text hpText;
        private TMP_Text spText;

        private void UpdateUI()
        {
            hpSlider.value = (float)owner.curHp / (float)owner.maxHp;
            spSlider.value = (float)owner.curSp / (float)owner.maxSp;
            hpText.text = owner.curHp + "/" + owner.maxHp;
            spText.text = owner.curSp + "/" + owner.maxSp;
        }
        private void Awake()
        {
            hpSlider = transform.Find("HpBar").GetComponent<Slider>();
            spSlider = transform.Find("SpBar").GetComponent<Slider>();
            hpText = transform.Find("HpText").GetComponent<TMP_Text>();
            spText = transform.Find("SpText").GetComponent<TMP_Text>();
        }

        void Start()
        {
            UpdateUI();
            owner.AttrUpdated += UpdateUI;
        }

        void Update()
        {

        }
    }
}

