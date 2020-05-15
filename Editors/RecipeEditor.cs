using Model;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Editors
{
    [CustomEditor(typeof(Recipe))]
    public class RecipeEditor:Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginVertical();
            GUILayout.Box("sdfsdfsdf");
            GUILayout.Box("sdfsdf");
            GUILayout.EndHorizontal();
        }
    }
}