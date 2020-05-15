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

            value = new Community(classes);
            value.name = nameInput.text;
            value.status = Community.Status.Completed;
            CommunityManager.Instance.AddNew(value);
        }

        
        public void Log()
        {
            Debug.Log(value);
        }

        protected override void UpdateUI()
        {
            template.status = value.status;
            base.UpdateUI();
            if (value.status==Community.Status.Building)
            {
                nameInput.interactable = true;
            }
            else
            {
                nameInput.interactable = false;
            }

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

            if (GUILayout.Button("Log Community"))
            {
                a.Log();
            }
        }
    }


}
