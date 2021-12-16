using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System.Text;

public class CheatConsole : MonoBehaviour {
    [SerializeField] string currentCommand = "";
    [SerializeField] TextMeshProUGUI log;
    private void Start() {
        log.text = "";
        gameObject.GetComponent<TextMeshProUGUI>().text = " ";
    }
    StringBuilder stringBuilder = new StringBuilder();
    float time;
    char[] ending = new char[] { '|', ' ', '|'};
    float lastInput;
    void Update() {
        if (ToggleConsole.displayed) {
            time += Time.deltaTime;
            if (time >= 2) {
                time = 0;
            }
            if (Input.anyKey) {
                lastInput = 0;
            }else { lastInput += Time.deltaTime; }
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                currentCommand = currentCommand.Substring(0, currentCommand.Length - 1);
                gameObject.GetComponent<TextMeshProUGUI>().text = gameObject.GetComponent<TextMeshProUGUI>().text.Substring(0, gameObject.GetComponent<TextMeshProUGUI>().text.Length - 1);
            } else if (Input.anyKey){
                currentCommand += Input.inputString;
                gameObject.GetComponent<TextMeshProUGUI>().text = currentCommand;
            }else if (lastInput > 1f){
                gameObject.GetComponent<TextMeshProUGUI>().text = currentCommand + " <b>" + ending[(int)Mathf.Round(time)] + "</b>";
            }
            if (Input.GetKeyDown(KeyCode.Tab)) {
                bool worked = launchCommand(currentCommand);

                if (!worked) { log.text += "<#9d5151>";
                } else { log.text += "<color=white>"; }

                log.text += " > " + currentCommand + "\n";

                currentCommand = "";
                gameObject.GetComponent<TextMeshProUGUI>().text = "";
            } 
        }
    } 
    private bool launchCommand(string command) {
        if (command.Length >= 8) {
            if (string.Equals(command.Substring(0, 8),"smeltery", System.StringComparison.OrdinalIgnoreCase)) {
                if (command.Length >= 8 + 3) {
                    string function = (command.Substring(9, 3));
                    string remainingString = command.Substring(13, command.Length - 13);

                    string variable = "";
                    foreach (var character in remainingString) {
                        if (character == ' ') {
                            break;
                        }
                        variable += character;
                    }

                    int VariableValue = int.Parse(remainingString.Substring(variable.Length + 1, remainingString.Length - (variable.Length + 1)));

                    if (string.Equals(function, "rem", System.StringComparison.OrdinalIgnoreCase)) {
                        SmelteryController.RemItem(variable, VariableValue);
                        return true;
                    }
                    if (string.Equals(function, "add", System.StringComparison.OrdinalIgnoreCase)) {
                        SmelteryController.AddItem(variable, VariableValue);
                        return true;
                    } 
                }


            }
        }

        return false;
    }

    //would be nice to make a better way of doing this to avoid this many repetitive lines

    [MenuItem("SmelteryTools/AddExampleOres")]
    public static void addExampleOres() {
        SmelteryController.AddItem("Copper", 5);
        SmelteryController.AddItem("Iron", 7);
        SmelteryController.AddItem("Silver", 1);
        SmelteryController.AddItem("Gold", 2);
    }


    #region Add Metals
    [MenuItem("SmelteryTools/Copper/Add5")]
    public static void CopperAdd5() {
        SmelteryController.AddItem("Copper", 5);
    }
    [MenuItem("SmelteryTools/Copper/Add1")]
    public static void CopperAdd1() {
        SmelteryController.AddItem("Copper", 1);
    }

    [MenuItem("SmelteryTools/Gold/Add5")]
    public static void GoldAdd5() {
        SmelteryController.AddItem("Gold", 5);
    }
    [MenuItem("SmelteryTools/Gold/Add1")]
    public static void GoldAdd1() {
        SmelteryController.AddItem("Gold", 1);
    }

    [MenuItem("SmelteryTools/Iron/Add5")]
    public static void IronAdd5() {
        SmelteryController.AddItem("Iron", 5);
    }
    [MenuItem("SmelteryTools/Iron/Add1")]
    public static void IronAdd1() {
        SmelteryController.AddItem("Iron", 1);
    }

    [MenuItem("SmelteryTools/Silver/Add5")]
    public static void SilverAdd5() {
        SmelteryController.AddItem("Silver", 5);
    }
    [MenuItem("SmelteryTools/Silver/Add1")]
    public static void SilverAdd1() {
        SmelteryController.AddItem("Silver", 1);
    }
    #endregion 
    #region Rem Metals
    [MenuItem("SmelteryTools/Copper/Rem5")]
    public static void CopperRem5() {
        SmelteryController.RemItem("Copper", 5);
    }
    [MenuItem("SmelteryTools/Copper/Rem1")]
    public static void CopperRem1() {
        SmelteryController.RemItem("Copper", 1);
    }

    [MenuItem("SmelteryTools/Gold/Rem5")]
    public static void GoldRem5() {
        SmelteryController.RemItem("Gold", 5);
    }
    [MenuItem("SmelteryTools/Gold/Rem1")]
    public static void GoldRem1() {
        SmelteryController.RemItem("Gold", 1);
    }

    [MenuItem("SmelteryTools/Iron/Rem5")]
    public static void IronRem5() {
        SmelteryController.RemItem("Iron", 5);
    }
    [MenuItem("SmelteryTools/Iron/Rem1")]
    public static void IronRem1() {
        SmelteryController.RemItem("Iron", 1);
    }

    [MenuItem("SmelteryTools/Silver/Rem5")]
    public static void SilverRem5() {
        SmelteryController.RemItem("Silver", 5);
    }
    [MenuItem("SmelteryTools/Silver/Rem1")]
    public static void SilverRem1() {
        SmelteryController.RemItem("Silver", 1);
    }

    #endregion 
}
