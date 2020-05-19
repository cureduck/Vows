using Model;
using UnityEditor;
using UnityEngine;

/*
namespace Editors
{
    [CustomPropertyDrawer(typeof(Item))]
    public class ItemDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            

            // Calculate rects
            var amountRect = new Rect(position.x, position.y, 30, position.height);
            var typeRect = new Rect(position.x + 35, position.y, 60, position.height);
            var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
            EditorGUI.LabelField(typeRect,property.type);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
    
}

*/
