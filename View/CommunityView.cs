using System;
using UnityEngine;
using Model;
using UnityEditor;
using System.Linq;
using Manager;

namespace View
{
    public class CommunityView : PanelTemplate<Community, Class>
    {
        public ClassView classViewTemplate;

        public void AddNewClass()
        {
            classViewTemplate.value.rank= transform.childCount;
            Instantiate(classViewTemplate, parent: this.transform);
        }

        public void CreateCommunity()
        {
            Class[] classes = new Class[transform.childCount];
            int i = 0;

            foreach (Transform child in transform)
            {
                classes[i]= child.gameObject.GetComponent<ClassView>().value;
                i++;
            }

            value = new Community(classes);
            CommunityManager.Instance.AddNew(value);
        }

        
        public void Log()
        {
            Debug.Log(value);
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
                a.AddNewClass();
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
