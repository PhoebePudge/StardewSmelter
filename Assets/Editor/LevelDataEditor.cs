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
        SerializedProperty ObjectProperty = serializedObject.FindProperty(gmPropertyName);
        SerializedProperty ObjectChance = serializedObject.FindProperty(fPropertyName);

        EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(ObjectChance.arraySize.ToString());

            

            if (GUILayout.Button("Add")) {
                fList.Add(0);
                gmList.Add(null);
            }

            if (GUILayout.Button("Remove")) {
                fList.RemoveAt(fList.Count - 1);
                gmList.RemoveAt(gmList.Count - 1);
            }

        EditorGUILayout.EndHorizontal();


        float totalChance = 0;

        for (int i = 0; i < gmList.Count; i++)
        {
            if (i < fList.Count)
            {
                totalChance += fList[i];
            }
        }

        Texture2D texture = new Texture2D((int)totalChance, 1);

        //list
        for (int i = 0; i < gmList.Count; i++) {
            if (i < fList.Count) {
                float value = (float)i / (float)gmList.Count;

                texture.SetPixel(i, 0, new Color(value,value,value));

                EditorGUILayout.BeginHorizontal();

                SerializedProperty IOProperty = ObjectProperty.GetArrayElementAtIndex(i);
                SerializedProperty ICProperty = ObjectChance.GetArrayElementAtIndex(i);

                EditorGUILayout.PropertyField(IOProperty, GUIContent.none);
                EditorGUILayout.PropertyField(ICProperty, GUIContent.none, GUILayout.Width(50));
                EditorGUILayout.LabelField("/ " + totalChance.ToString(), GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();
            }
        }
        texture.Apply();
        texture.filterMode = FilterMode.Point;
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
         
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

        //height and width
        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("height"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("width"));
        EditorGUILayout.EndHorizontal();


        //seed
        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("useRandomSeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("seed")); 
        EditorGUILayout.EndHorizontal();



        //chances
        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("randomFillPercent")); 
        EditorGUILayout.PropertyField(serializedObject.FindProperty("monsterChance")); 
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("NoninteractableChance")); 


        //monster types
        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.LabelField("Monster Objects", EditorStyles.boldLabel);
        SerializedProperty monsterTypeChance = serializedObject.FindProperty("monsterTypeChance");

        float totalMonsterChance = 0;
        for (int i = 0; i < leveldata.monsterTypes.Length - 1; i++)
        { 
            totalMonsterChance += leveldata.monsterTypeChance[i];
        }

        for (int i = 0; i < leveldata.monsterTypes.Length - 1; i++) {
             
            EditorGUILayout.BeginHorizontal();
            leveldata.monsterEnabled[i] = EditorGUILayout.Toggle(leveldata.monsterEnabled[i]);

            if (!leveldata.monsterEnabled[i]) {
                 
                EditorGUILayout.LabelField(leveldata.monsterTypes[i].Name, GUILayout.Width(100)); 
                EditorGUILayout.LabelField(leveldata.monsterTypeChance[i].ToString(), GUILayout.Width(50));
                EditorGUILayout.LabelField("/ " + totalMonsterChance, GUILayout.Width(50));


            } else {

                EditorGUILayout.TextField(leveldata.monsterTypes[i].Name, GUILayout.Width(100));
                SerializedProperty MCProperty = monsterTypeChance.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(MCProperty, GUIContent.none, GUILayout.Width(50));
                EditorGUILayout.LabelField("/ " + totalMonsterChance, GUILayout.Width(50));

            }
            EditorGUILayout.EndHorizontal();
        }


        //interactable
        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.LabelField("Interactable Objects", EditorStyles.boldLabel); 
        DisplayArrayGameobjectAndFloat("WallObject", "WallChance", leveldata.WallObject, leveldata.WallChance);


        //non interactable
        DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.LabelField("Non-Interactable Objects", EditorStyles.boldLabel); 
        DisplayArrayGameobjectAndFloat("NoninteractableObject", "NoninteractableObjectChance", leveldata.NoninteractableObject, leveldata.NoninteractableObjectChance);

        serializedObject.ApplyModifiedProperties();
    }
}
