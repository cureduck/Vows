using System;
using Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editors
{

    [CustomEditor(typeof(Animal))]
    public class AnimalEditor:Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}