using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransitionScene : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Blacksmiths");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
