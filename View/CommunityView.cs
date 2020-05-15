using System;
using UnityEngine;
using Model;
using UnityEditor;
using System.Linq;
using Manager;
using TMPro;

namespace View
{
    class CommunityView : ListTemplate<Community,Class, ClassView>
    {
        protected override Class[] components { get => value.Classes; set => this.value.Classes = value; }
        private TMP_InputField _nameInput;

        private void Start()
        {
            _nameInput = GetComponentInChildren<TMP_InputField>();
            UpdateUI();
        }

        public override void AddNewItem()
        {
            var t= AddNewTemplate();
            t.status = Community.Status.Building;
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

            value = new Community(classes) {name = _nameInput.text, status = Community.Status.Completed};
            CommunityManager.Instance.AddNew(value);
        }
        

        protected override void UpdateUI()
        {
            if (value==null) return;
            template.status = value.status;
            base.UpdateUI();
            _nameInput.interactable = value.status==Community.Status.Building;

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

            value.Classes = classes;
            value.name = _nameInput.text;
            value.status = Community.Status.Completed;
        }
    }

    
    [CustomEditor(typeof(CommunityView))]
    public class AnimalEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var a = (CommunityView)target;

            if (GUILayout.Button("Build New Community"))
            {
                a.CreateCommunity();
            }
            
            if (a.value==null)
            {
                EditorGUILayout.HelpBox("No Target",MessageType.Warning);
            }
            
        }
    }


}
