﻿using System;
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
            value.className = className.text;
            value.capacity = new RangeInt(int.Parse(minCap.text), int.Parse(maxCap.text));
        }

        public void CheckLegal()
        {
            if (int.TryParse(minCap.text, out var t))
            {
                value.capacity.start = t;
            }
            else
            {
                minCap.text = value.capacity.start.ToString();
            }

            if (int.TryParse(maxCap.text, out t))
            {
                value.capacity.length = t - value.capacity.start;
            }
            else
            {
                maxCap.text = value.capacity.end.ToString();
            }

        }

        protected override void UpdateUI()
        {
            className.text=value.className;
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
