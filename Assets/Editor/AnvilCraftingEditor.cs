using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CraftingPanel))]
public class AnvilCraftingEditor : Editor
{
    CraftingPanel craftingPanel;
    private void OnEnable()
    {
        craftingPanel = (CraftingPanel)target;
    }
    public static void DrawUILine(Color color, int thickness = 2, int padding = 20)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2;
        r.x -= 2;
        r.width += 6;
        EditorGUI.DrawRect(r, color);
    }
    public override void OnInspectorGUI()
    { 
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("offset"));

        SerializedProperty property = serializedObject.FindProperty("CraftingSprites1");
        SerializedProperty property1 = serializedObject.FindProperty("CraftingSprites2");
        SerializedProperty property2 = serializedObject.FindProperty("CraftingSprites3");
        SerializedProperty toolPatterns = serializedObject.FindProperty("patterns");

        GUIStyle myToggleStyle = new GUIStyle(GUI.skin.toggle);
        myToggleStyle.fontStyle = FontStyle.Bold;
        GUIStyle s = new GUIStyle(EditorStyles.textField);
        s.normal.textColor = Color.green;
        s.fixedWidth = 100;
        
        for (int i = 0; i < 11; i++)
        {
            GUI.contentColor = Color.white;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(craftingPanel.patterns[i].type.ToString(), EditorStyles.boldLabel, GUILayout.Width(100));
            EditorGUILayout.LabelField(craftingPanel.patterns[i].parts[0].ToString(), EditorStyles.boldLabel, GUILayout.Width(100));
            EditorGUILayout.LabelField(craftingPanel.patterns[i].parts[1].ToString(), EditorStyles.boldLabel, GUILayout.Width(100));
            EditorGUILayout.LabelField(craftingPanel.patterns[i].parts[2].ToString(), EditorStyles.boldLabel, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(craftingPanel.patterns[i].type.ToString(), GUILayout.Width(100));
            if (craftingPanel.CraftingSprites1[i] == null)
            {
                GUI.contentColor = Color.red;
                EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.Width(100));
            }else
            {
                GUI.contentColor = Color.white;
                EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.Width(100));
            }

            if (craftingPanel.CraftingSprites2[i] == null)
            {
                GUI.contentColor = Color.red;
                EditorGUILayout.PropertyField(property1.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.Width(100));
            }
            else
            {
                GUI.contentColor = Color.white;
                EditorGUILayout.PropertyField(property1.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.Width(100));
            }

            if (craftingPanel.CraftingSprites3[i] == null)
            {
                GUI.contentColor = Color.red;
                EditorGUILayout.PropertyField(property2.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.Width(100));
            }
            else
            {
                GUI.contentColor = Color.white;
                EditorGUILayout.PropertyField(property2.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.Width(100));
            }
            
            

            EditorGUILayout.EndHorizontal();
            LevelDataEditor.DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        } 
        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}
