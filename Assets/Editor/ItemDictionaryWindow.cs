using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDictionaryWindow : EditorWindow {
    Vector2 scrollPos;
    [MenuItem("Game Tools/Item Dictionary")]
    public static void CustomEditorWindow() {
        GetWindow<ItemDictionaryWindow>("Custom Unity Editor Window");
    }
    private void OnGUI() { 
        EditorGUI.DrawRect(new Rect(0,15, 1000, 45), new Color(.15f,.15f,.15f));
        LevelDataEditor.DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
        EditorGUILayout.LabelField("Add items to your inventory", style);
        LevelDataEditor.DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));

        
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        foreach (var item in InventorySystem.itemList) {
            GUILayout.BeginHorizontal();
            {

                Sprite sprite = item.sprite;
                GUILayout.Label(sprite.texture, GUILayout.Width(100));

                GUILayout.BeginVertical();
                {
                    GUILayout.Label(item.itemName);
                    GUILayout.Label(item.itemDescription);
                    GUILayout.Label(item.maxItemQuanity.ToString());
                    GUILayout.Label(item.itemAttribute.ToString());

                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Add 1 to inventory")) {
                            GameObject gm = new GameObject();
                            InventorySystem.AddItem(gm, item);
                        }
                        if (item.maxItemQuanity != 1) { 
                            if (GUILayout.Button("Add max stack of " + item.maxItemQuanity)) {
                                GameObject gm = new GameObject();
                                InventorySystem.AddItem(gm, item, item.maxItemQuanity);
                            }
                        }
                    }
                    GUILayout.EndHorizontal();
                    LevelDataEditor.DrawUILine(new Color(0.5f, 0.5f, 0.5f, 1));
                }
                GUILayout.EndVertical(); 
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
    }
}