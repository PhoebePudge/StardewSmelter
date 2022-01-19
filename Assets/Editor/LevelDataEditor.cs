using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor {
    LevelData leveldata;
    private void OnEnable() {
        leveldata = (LevelData)target;
    }
    private void DisplayArrayGameobjectAndFloat(string gmPropertyName, string fPropertyName, List<GameObject> gmList, List<float> fList) {
        SerializedProperty NoninteractableObjectProperty = serializedObject.FindProperty(gmPropertyName);
        SerializedProperty NoninteractableObjectChanceProperty = serializedObject.FindProperty(fPropertyName);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(NoninteractableObjectChanceProperty.arraySize.ToString());

        if (GUILayout.Button("Add")) {
            fList.Add(0);
            gmList.Add(null);
        }
        if (GUILayout.Button("Remove")) {
            fList.RemoveAt(fList.Count - 1);
            gmList.RemoveAt(gmList.Count - 1);
        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < gmList.Count; i++) {
            if (i < fList.Count) {
                 
                EditorGUILayout.BeginHorizontal();

                SerializedProperty IOProperty = NoninteractableObjectProperty.GetArrayElementAtIndex(i);
                SerializedProperty ICProperty = NoninteractableObjectChanceProperty.GetArrayElementAtIndex(i);

                EditorGUILayout.PropertyField(IOProperty, GUIContent.none);
                EditorGUILayout.PropertyField(ICProperty, GUIContent.none, GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();
            }
        }
    }

    public static void DrawUILine(Color color, int thickness = 2, int padding = 20) {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2;
        r.x -= 2;
        r.width += 6;
        EditorGUI.DrawRect(r, color);
    }
    public override void OnInspectorGUI() {

        base.OnInspectorGUI();

        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("height"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("width"));
        EditorGUILayout.EndHorizontal();

        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("useRandomSeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("seed")); 
        EditorGUILayout.EndHorizontal();


        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("randomFillPercent")); 
        EditorGUILayout.PropertyField(serializedObject.FindProperty("monsterChance")); 
        EditorGUILayout.PropertyField(serializedObject.FindProperty("InteractableChance")); 
        EditorGUILayout.PropertyField(serializedObject.FindProperty("NoninteractableChance")); 


        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.LabelField("Monster Objects", EditorStyles.boldLabel);
        SerializedProperty monsterTypeChance = serializedObject.FindProperty("monsterTypeChance");

        for (int i = 0; i < leveldata.monsterTypes.Length; i++) {

            EditorGUILayout.BeginHorizontal();
            leveldata.monsterEnabled[i] = EditorGUILayout.Toggle(leveldata.monsterEnabled[i]);

            if (!leveldata.monsterEnabled[i]) {
                 
                EditorGUILayout.LabelField(leveldata.monsterTypes[i].Name); 
                EditorGUILayout.LabelField(leveldata.monsterTypeChance[i].ToString(), GUILayout.Width(50));

            } else {

                EditorGUILayout.TextField(leveldata.monsterTypes[i].Name);
                SerializedProperty MCProperty = monsterTypeChance.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(MCProperty, GUIContent.none, GUILayout.Width(50));

            }
            EditorGUILayout.EndHorizontal();
        }
        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.LabelField("Interactable Objects", EditorStyles.boldLabel); 
        DisplayArrayGameobjectAndFloat("InteractableObject", "InteractableObjectChance", leveldata.InteractableObject, leveldata.InteractableObjectChance);

        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.LabelField("Non-Interactable Objects", EditorStyles.boldLabel); 
        DisplayArrayGameobjectAndFloat("NoninteractableObject", "NoninteractableObjectChance", leveldata.NoninteractableObject, leveldata.NoninteractableObjectChance);

        serializedObject.ApplyModifiedProperties();
    }

}
