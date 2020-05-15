using System;
using UnityEngine;
using TMPro;
using UnityEditor;
using Model;


namespace View
{
    public class ClassView : ListTemplate<Class, Animal, AnimalIcon>
    {

        [SerializeField] private TMP_InputField className, minCap, maxCap;
        public Community.Status status;

        private void Awake()
        {
            //value = new Class {Capacity = new RangeInt(1, 0)};
            switch (status)
            {
                case Community.Status.Building:
                    className.interactable = true;
                    minCap.interactable = true;
                    maxCap.interactable = true;
                    break;
                case Community.Status.Completed:
                    className.interactable = false;
                    minCap.interactable = false;
                    maxCap.interactable = false;
                    break;
                case Community.Status.Dissolved:
                    className.interactable = false;
                    minCap.interactable = false;
                    maxCap.interactable = false;
                    break;
                default:
                    break;
            }
        }        

        public void UpdateValue()
        {
            value.ClassName = className.text;
            value.Capacity = new RangeInt(int.Parse(minCap.text), int.Parse(maxCap.text));
        }

        public void CheckLegal()
        {
            int t;
            if (int.TryParse(minCap.text, out t))
            {
                value.Capacity.start = t;
            }
            else
            {
                minCap.text = value.Capacity.start.ToString();
            }

            if (int.TryParse(maxCap.text, out t))
            {
                value.Capacity.length = t - value.Capacity.start;
            }
            else
            {
                maxCap.text = value.Capacity.end.ToString();
            }

        }

        protected override void UpdateUI()
        {
            className.text=value.ClassName;
            minCap.text = value.Capacity.start.ToString();
            maxCap.text = value.Capacity.end.ToString();

        }
    }


    [CustomEditor(typeof(CommunityIcon))]
    public class ClassViewEditor:Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var a = (CommunityIcon)target;

            if (a.value==null)
            {
                EditorGUILayout.HelpBox("No Target",MessageType.Warning);
            }
        }
    }
}
