using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterType thisMonster;

    void Update()
    {
        
    }
}
public class MonsterType {
    public string name;

    public MonsterType(string name) {
        this.name = name;
    }
    public override string ToString() {
        return name;
    }
}