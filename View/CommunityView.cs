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
        public override Class[] components { get => value.Classes; set => this.value.Classes = value; }
        TMP_InputField nameInput;

        public void Start()
        {
            nameInput = GetComponentInChildren<TMP_InputField>();

            if (value != null)
            {
                UpdateUI();
            }
        }

        public override void AddNewTemplate()
        {
            template.value.rank = ChildSection.childCount;
            base.AddNewTemplate();
        }

        public void CreateCommunity()
        {
            Class[] classes = new Class[ChildSection.childCount];
            int i = 0;

            foreach (Transform child in ChildSection)
            {
                classes[i]= child.gameObject.GetComponent<ClassView>().value;
                i++;
            }

            value = new Community(classes);
            value.name = nameInput.text;
            CommunityManager.Instance.AddNew(value);
        }

        
        public void Log()
        {
            Debug.Log(value);
        }

        protected override void UpdateUI()
        {
            base.UpdateUI();
        }
    }


    [CustomEditor(typeof(CommunityView))]
    public class AnimalEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CommunityView a = (CommunityView)target;
            if (GUILayout.Button("Add New Class"))
            {
                a.AddNewTemplate();
            }

            if (GUILayout.Button("Build New Community"))
            {
                a.CreateCommunity();
            }

            if (GUILayout.Button("Log Community"))
            {
                a.Log();
            }
        }
    }


}
