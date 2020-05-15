using System;
using UnityEngine;
using Model;
using TMPro;

namespace View
{
    class CommunityIcon : Icon<Community>
    {

        public CommunityView communityView;
        [SerializeField] private TMP_Text nameText;

        void Awake()
        {
            //name_text = GetComponentInChildren<TMP_Text>();
        }

        protected override void UpdateUI()
        {
            Debug.Log(value);
            nameText.text = value.name;
        }

        public void DisplayThis()
        {
            communityView.value = value;
        }
    }
}
