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

        private void Awake()
        {
            nameText = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            nameText.text = value.name;
        }

        protected override void UpdateUI()
        {
            nameText.text = value.name;
        }

        public void DisplayThis()
        {
            communityView.value = value;
        }
    }
}
