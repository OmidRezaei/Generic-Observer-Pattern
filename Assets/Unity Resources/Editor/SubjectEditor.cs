#if UNITY_EDITOR
using ObserverPattern.BuiltIn;

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ObservedInteger))]
[CustomPropertyDrawer(typeof(ObservedFloat))]
[CustomPropertyDrawer(typeof(ObservedDouble))]
[CustomPropertyDrawer(typeof(ObservedString))]
[CustomPropertyDrawer(typeof(ObservedBoolean))]
[CustomPropertyDrawer(typeof(ObservedChar))]
public class SubjectEditor : PropertyDrawer
{
    bool foldOut = false;
    float mainHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        mainHeight = base.GetPropertyHeight(property, label);
        return mainHeight * (foldOut ? 2 : 1) + (foldOut ? 5 : 0);
    }



    //TODO: Create a List under the sub-properties. Or Make this property totally a list with the sub properties in front of it.
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);


        foldOut = EditorGUI.Foldout(new Rect(position.x, position.y, position.width / 3, position.height), foldOut, label);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(" "));

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float booleanWidth = 3;
        float space = 5;
        float propertyWidth = (position.width - booleanWidth) / 2 - 2 * space;


        var x = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = GUI.skin.textField.CalcSize(new GUIContent("Value")).x;

        EditorGUI.PropertyField(new Rect(position.x, position.y, propertyWidth, mainHeight), property.FindPropertyRelative("_value"), new GUIContent("Value"));
        GUI.enabled = false;
        EditorGUI.PropertyField(new Rect(position.x + propertyWidth + space, position.y, propertyWidth, mainHeight), property.FindPropertyRelative("lastValue"), GUIContent.none);
        GUI.enabled = true;
        EditorGUI.PropertyField(new Rect(position.x + propertyWidth * 2 + 2 * space, position.y, booleanWidth, mainHeight), property.FindPropertyRelative("NotifyOnGet"), GUIContent.none);

        if (foldOut && property.FindPropertyRelative("observers") != null)
        {
            EditorGUI.PropertyField(new Rect(position.x, position.y + mainHeight + 2, position.width, mainHeight), property.FindPropertyRelative("observers"), GUIContent.none);
        }

        EditorGUIUtility.labelWidth = x;

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();

    }
}
#endif