using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseQuest : MonoBehaviour
{
    public KillQuest killQuest;

    private Canvas questCanvas;

    public bool killQuestActive;

    public bool mineQuestActive;

    public bool adventureQuestActive;

    public void QuestComplete()
    {
        killQuestActive = false;
        mineQuestActive = false;
        adventureQuestActive = false;
    }
}
