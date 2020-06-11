using System;
using UnityEngine;
using Model;
using UnityEditor;
using Manager;
using TMPro;

namespace View
{
    class CommunityView : ListTemplate<Group,Class, ClassView>
    {
        protected override Class[] components { get => value.classes; set => this.value.classes = value; }
        private TMP_InputField _nameInput;

        private void Start()
        {
            _nameInput = GetComponentInChildren<TMP_InputField>();
            UpdateUI();
        }

        public override void AddNewItem()
        {
            var t= AddNewTemplate();
            t.status = Group.Status.Building;
        }


        public void CreateCommunity()
        {
            var classes = new Class[childSection.childCount];
            var i = 0;

            foreach (Transform child in childSection)
            {
                classes[i]= child.gameObject.GetComponent<ClassView>().value;
                i++;
            }

            value = new Group(classes) {name = _nameInput.text, status = Group.Status.Completed};
            GroupManager.Instance.AddNew(value);
        }
        

        protected override void UpdateUI()
        {
            if (value==null) return;
            template.status = value.status;
            base.UpdateUI();
            _nameInput.interactable = value.status==Group.Status.Building;

        }

        public void CompleteCommunity()
        {
            var classes = new Class[childSection.childCount];
            var i = 0;
            foreach (Transform child in childSection)
            {
                classes[i]= child.gameObject.GetComponent<ClassView>().value;
                i++;
            }

            value.classes = classes;
            value.name = _nameInput.text;
            value.status = Group.Status.Completed;
        }
    }
}
