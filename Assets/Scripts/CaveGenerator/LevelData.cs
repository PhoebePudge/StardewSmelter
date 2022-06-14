using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "Level Data", menuName = "Cave/LevelData")]
public class LevelData : ScriptableObject
{
	[HideInInspector] [Range(0f, 1f)] public float monsterChance;

	[HideInInspector] public bool[] monsterEnabled = new bool[3];
	[HideInInspector] public Type[] monsterTypes = new Type[] { typeof(Monsters.Giant),  typeof(Monsters.Goblin), typeof(Monsters.GoblinThrower) };
	[HideInInspector] public float[] monsterTypeChance = new float[] { 1f, 1f, 1f };

	[HideInInspector] public int width = 70;
	[HideInInspector] public int height = 70;

	[HideInInspector] public string seed;
	[HideInInspector] public bool useRandomSeed;

	[HideInInspector] [Range(0, 100)] public int randomFillPercent;
	 
	[HideInInspector] public List<GameObject> WallObject;
	[HideInInspector] public List<float> WallChance;

	[HideInInspector] [Range(0f, 1f)] public float NoninteractableChance = .2f;

	[HideInInspector] public List<GameObject> NoninteractableObject;
	[HideInInspector] public List<float> NoninteractableObjectChance;

	public Color[] colours = new Color[5];
}
