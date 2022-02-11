using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SmelteryWindow : EditorWindow {
    Vector2 scrollPos;

    [MenuItem("Game Tools/Smeltery Window")]
    public static void CustomEditorWindow() {
        GetWindow<SmelteryWindow>("Custom Unity Editor Window");
    }
    private void OnGUI() {
        EditorGUI.DrawRect(new Rect(0, 15, 1000, 45), new Color(.15f, .15f, .15f));
        LevelDataEditor.DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
        EditorGUILayout.LabelField("Add items to your inventory", style);
        LevelDataEditor.DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        if (GUILayout.Button("Quick add example ores")) {
            SmelteryController.AddItem("Copper", 5);
            SmelteryController.AddItem("Iron", 7);
            SmelteryController.AddItem("Silver", 1);
            SmelteryController.AddItem("Gold", 2);
        }
        LevelDataEditor.DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));

        foreach (var item in SmelteryController.oreDictionary) { 
            GUILayout.BeginHorizontal();
            GUILayout.Label(item.metalData.itemName, GUILayout.Width(100));

            GUILayout.BeginVertical();
            {
                if (GUILayout.Button("Add 1")) {
                    SmelteryController.AddItem(item.metalData.itemName, 1);
                }
                if (GUILayout.Button("Add 5")) {
                    SmelteryController.AddItem(item.metalData.itemName, 5);
                }
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            {
                if (GUILayout.Button("Rem 1")) {
                    SmelteryController.RemItem(item.metalData.itemName, 1);
                }
                if (GUILayout.Button("Rem 5")) {
                    SmelteryController.RemItem(item.metalData.itemName, 5);
                }
            }
            GUILayout.EndVertical(); 

            GUILayout.EndHorizontal();

            LevelDataEditor.DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        }

        foreach (var item in SmelteryController.Combinations) {
            string combination = item.Alloy + " <- ";
            for (int i = 0; i < item.AlloyParents.Count; i++) {
                combination += item.AlloyParents[i];

                if (i + 1 < item.AlloyParents.Count) {
                    combination += " + ";
                }
            }
            GUILayout.Label(combination);
        }
         EditorGUILayout.EndScrollView();
    }
}