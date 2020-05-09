using System;
using UnityEngine;
using UnityEditor;

namespace Model
{
    [CustomEditor(typeof(Item))]
    class ItemEditor:Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.HelpBox("This is a help box", MessageType.Info);
        }


    }
}
