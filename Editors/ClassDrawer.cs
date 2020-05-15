using Model;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Editors
{
    [CustomPropertyDrawer(typeof(Class))]
    public class ClassDrawer:PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var rankdRect=new Rect(position.x, position.y, 30, position.height);
            var rankRect = new Rect(position.x +30+indent, position.y, 30, position.height);
            
            var namedRect=new Rect(position.x+60+2*indent, position.y, 40, position.height);
            var nameRect = new Rect(position.x +100+3*indent, position.y, position.width-90, position.height);
            
            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.LabelField(rankdRect,"rank");
            EditorGUI.PropertyField(rankRect, property.FindPropertyRelative("rank"), GUIContent.none);
            EditorGUI.LabelField(namedRect,"name");
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}