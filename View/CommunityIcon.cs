using System;
using UnityEngine;
using Model;
using TMPro;

namespace View
{
    class CommunityIcon : Icon<Community>
    {

        public CommunityView communityView;
        [SerializeField] private TMP_Text name_text;

        void Awake()
        {
            //name_text = GetComponentInChildren<TMP_Text>();
        }

        protected override void UpdateUI()
        {
            Debug.Log(value);
            name_text.text = value.name;
        }

        public void DisplayThis()
        {
            Debug.Log(value);
            communityView.value = value;
        }
    }
}
