using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : MonoBehaviour
{
    public int creaturesToKill;

    public int creaturesKilled;

    public BaseQuest baseQuest;

    public void LateUpdate()
    {
        if (creaturesKilled == creaturesToKill || creaturesKilled >= creaturesToKill)
        {
            baseQuest.QuestComplete();
        }
    }

    public void CreatureKilled()
    {
        creaturesKilled--;
    }
}
