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

    //flashing character
    char[] ending = new char[] { '|', ' ', '|' };
    float lastInput;

    void Update() {
        //toggle displayer
        if (ToggleConsole.displayed) {
            time += Time.deltaTime;
            if (time >= 2) {
                time = 0;
            }
            if (Input.anyKey) {
                lastInput = 0;
            } else { lastInput += Time.deltaTime; }

            //remove words with backspace
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                currentCommand = currentCommand.Substring(0, currentCommand.Length - 1);
                gameObject.GetComponent<TextMeshProUGUI>().text = gameObject.GetComponent<TextMeshProUGUI>().text.Substring(0, gameObject.GetComponent<TextMeshProUGUI>().text.Length - 1);
            } else if (Input.anyKey) {
                //add characters to current command
                currentCommand += Input.inputString;
                gameObject.GetComponent<TextMeshProUGUI>().text = currentCommand;
            } else if (lastInput > 1f) {
                gameObject.GetComponent<TextMeshProUGUI>().text = currentCommand + " <b>" + ending[(int)Mathf.Round(time)] + "</b>";
            }

            //tab to launch command
            if (Input.GetKeyDown(KeyCode.Tab)) {
                bool worked = launchCommand(currentCommand);

                if (!worked) {
                    log.text += "<#9d5151>";
                } else { log.text += "<color=white>"; }

                log.text += " > " + currentCommand + "\n";

                currentCommand = "";
                gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
    private bool launchCommand(string command) {
        if (command.Length >= 8) { 
            if (string.Equals(command.Substring(0, 8), "smeltery", System.StringComparison.OrdinalIgnoreCase)) {
                if (command.Length >= 8 + 3) {
                    //get parameters from string
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

                    //if this is rem function, call it
                    if (string.Equals(function, "rem", System.StringComparison.OrdinalIgnoreCase)) {
                        SmelteryController.RemItem(variable, VariableValue);
                        return true;
                    }

                    //if this is add function, call it
                    if (string.Equals(function, "add", System.StringComparison.OrdinalIgnoreCase)) {
                        SmelteryController.AddItem(variable, VariableValue);
                        return true;
                    }
                }


            }
        }
        return false;
    }
}
