using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CatSpawnerController))]
public class SpawnnerEditor : Editor
{
    public CatSpawnerController catController;
    private string[] symptomNames = { "Symptom 1", "Symptom 2", "Symptom 3", "Symptom 4" };

    public override void OnInspectorGUI(){

        CatSpawnerController myTarget = target as CatSpawnerController;
        EditorGUI.indentLevel = 0;

        serializedObject.Update();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();

        SerializedProperty sceneNames = this.serializedObject.FindProperty("SceneNames");
        EditorGUILayout.PropertyField(sceneNames.FindPropertyRelative("Array.size"));

        for (int i = 0; i < sceneNames.arraySize; i++)
            EditorGUILayout.PropertyField(sceneNames.GetArrayElementAtIndex(i), GUIContent.none);

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}