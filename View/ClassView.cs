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
            switch (status)
            {
                case Community.Status.Building:
                    value = new Class {capacity = new RangeInt(1, 0)};
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
            value.name = className.text;
            var r = new RangeInt(int.Parse(minCap.text), int.Parse(maxCap.text) - int.Parse(minCap.text));
            value.capacity = r;
        }

        public void CheckLegal(bool left)
        {
            if (left)
            {
                if (int.TryParse(minCap.text, out var t))
                {
                    if (value.capacity.end<t)
                    {
                        value.capacity.length = 0;
                        maxCap.text = t.ToString();
                    }
                    value.capacity.start = t;
                }
                else
                {
                    minCap.text = value.capacity.start.ToString();
                }
            }
            else
            {
                int t;
                if (int.TryParse(maxCap.text, out t))
                {
                    if (value.capacity.start>t)
                    {
                        value.capacity.start = t;
                        minCap.text = t.ToString();
                    }
                    value.capacity.length = t - value.capacity.start;
                }
                else
                {
                    maxCap.text = value.capacity.end.ToString();
                }
            }
        }

        protected override void UpdateUI()
        {
            className.text=value.name;
            minCap.text = value.capacity.start.ToString();
            maxCap.text = value.capacity.end.ToString();
            base.UpdateUI();

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
