using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaypointNode))]
public class WaypointsEditor : Editor{

    SerializedProperty Parent;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        Parent = serializedObject.FindProperty("Parent");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(Parent, new GUIContent("Waypoint parent"));
        EditorGUILayout.LabelField("Не присваивайте этому значению объекта его самого. ");
        if (GUILayout.Button("Перейти к родителю"))
        {
            ((WaypointNode)target).ShowParent();
        }
        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }

}
