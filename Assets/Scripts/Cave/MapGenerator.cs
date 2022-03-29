using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine.AI;  
public class MapGenerator : MonoBehaviour {
	//our list of levels
	public List<LevelData> levelData = new List<LevelData>();

	private int width = 100;
	private int height = 100;
	private string seed;
	private bool useRandomSeed;
	private int randomFillPercent;
	GenerateMap generateMap;
	//our map data and our voxel data
	public VoxelData[,,] voxelData;

	//the material for the voxels made
	[SerializeField] Material voxelMaterial;

	//player transform position
	[SerializeField] Transform pTransform;
	//ladder prefab
	[SerializeField] GameObject Ladder;
	int currentLevel = 0;
	void Start() {
		//generate our first level
		GenerateMesh(currentLevel);
	}
	
    private void Update() {
		//increase the current level
        if (Input.GetKeyDown(KeyCode.J)) {
			foreach (Transform child in transform) {
				GameObject.Destroy(child.gameObject);
			}
			currentLevel++; 
			GenerateMesh(currentLevel);
		}
		//decrease the current level
		if (Input.GetKeyDown(KeyCode.H)) {
			foreach (Transform child in transform) {
				GameObject.Destroy(child.gameObject);
			}
			currentLevel--;
			GenerateMesh(currentLevel);
		}
	} 
	private void SpawnEnemy(int x, int y, Transform monsterParent, Transform objectParents, float scale = 10f)
    {
		//calculate our new position
		Vector3 newPosition = new Vector3(x - (width / 2) - .5f, 0, y - (height / 2) - .5f);

		//if a random value is within the chance of a monster spawning
		if (Random.value < levelData[0].monsterChance)
		{ 
			//check that this is a valid position in navmesh (this is obsolute, prob can remove now)
			if (ValidPosition(newPosition))
			{

				//create our new enemy
				GameObject enemy = new GameObject("Monster", typeof(MeshFilter), typeof(MeshRenderer), typeof(CapsuleCollider), typeof(Rigidbody));
				enemy.GetComponent<CapsuleCollider>().height = 3;
				enemy.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
				enemy.transform.SetParent(monsterParent);
				enemy.transform.position = newPosition;

				//select the type of monster from a array
				Type newComponent = levelData[0].monsterTypes[Random.Range(0, levelData[0].monsterTypes.Length)];
				//get our assembly address of the class
				string address = "Monsters." + newComponent.Name + ", " + typeof(MonsterType).Assembly;
				//add the chosen component in
				enemy.AddComponent(Type.GetType(address));
			}

			//else check if perlin noise at that position is within the non interactable object chance
		}
		else if (Mathf.PerlinNoise(((float)x / (float)width) * (scale / 2), ((float)y / (float)height) * (scale / 2)) < levelData[0].NoninteractableChance)
		{

			float rand = Random.value;
			int index = Random.Range(0, levelData[0].NoninteractableObject.Count);

			//using a random value, loop through the objects and choose what index we use by comparing to their chance of spawning
			for (int i = 0; i < levelData[0].NoninteractableObject.Count; i++)
			{
				if (rand <= levelData[0].NoninteractableObjectChance[i])
				{
					index = i;
					break;
				}
			}

			//create our gameobject using the prefab of the chosen object
			GameObject ambientItem = GameObject.Instantiate(levelData[0].NoninteractableObject[index]);
			ambientItem.transform.rotation = Random.value > .5f ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
			ambientItem.transform.SetParent(objectParents);
			ambientItem.transform.position = newPosition;

			//else check if perlin noise at that position is within the non interactable object chance
		}
		else if (Mathf.PerlinNoise(((float)x / (float)width) * scale, ((float)y / (float)height) * scale) < levelData[0].InteractableChance)
		{

			float rand = Random.value;
			int index = Random.Range(0, levelData[0].NoninteractableObject.Count);

			//using a random value, loop through the objects and choose what index we use by comparing to their chance of spawning
			for (int i = 0; i < levelData[0].NoninteractableObject.Count; i++)
			{
				if (rand <= levelData[0].InteractableObjectChance[i])
				{
					index = i;
					break;
				}
			}

			//create our gameobject using the prefab of the chosen object
			GameObject ambientItem = GameObject.Instantiate(levelData[0].InteractableObject[index]);
			ambientItem.AddComponent<NavMeshObstacle>();
			ambientItem.transform.rotation = Random.value > .5f ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

			//give it a mineable tag and a collider so our tools can detect it
			ambientItem.transform.tag = "Mineable";
			ambientItem.AddComponent<BoxCollider>().isTrigger = true;
			ambientItem.transform.SetParent(objectParents.transform);
			ambientItem.transform.position = newPosition;
		}
	}
    private void GenerateEnemiesAndPlayer() {
		//create a parent for our monsters created
		GameObject monsterParent = new GameObject("Monster Parent");
		monsterParent.transform.SetParent(gameObject.transform);
		//create a parent for our objects created
		GameObject objectParents = new GameObject("Object Parent");
		objectParents.transform.SetParent(gameObject.transform);

		//loop though our map
		for (int x = 0; x < generateMap.map.GetLength(0); x++) {
			for (int y = 0; y < generateMap.map.GetLength(1); y++) { 
				//if there is not a wall here
				if (generateMap.map[x, y] == 0) { 
					SpawnEnemy(x,y, monsterParent.transform, objectParents.transform);
				}
			}
        }

		//set our default position to the current position
		Vector3 newPlayerPosition = pTransform.position; 
		//attempt choosing a position 30 times, if not the default position is used.
        for (int i = 0; i < 30; i++) {

			//choose a random x and y position on the map
			int xPos = Random.Range(1, generateMap.map.GetLength(0) - 1);
			int yPos = Random.Range(1, generateMap.map.GetLength(1) - 1);

			//check if this position is empty
            if (generateMap.map[xPos, yPos] == 0) {

				//if it is, we will select this position as out spawn position, and then break out of this loop.
				Vector3 newPosition = new Vector3(xPos - (width / 2) - .5f, 0, yPos - (height / 2) - .5f);
				newPlayerPosition = newPosition;
				break;	
			}
        }
		//update our position
		pTransform.position = newPlayerPosition;

		//set our default position to the current position
		Vector3 newLadderPosition = pTransform.position;
		for (int i = 0; i < 30; i++) {

			//choose a random x and y position on the map
			int xPos = Random.Range(1, generateMap.map.GetLength(0) - 1);
			int yPos = Random.Range(1, generateMap.map.GetLength(1) - 1);

			Debug.Log(generateMap.GetSurroundingWallCount(xPos,yPos));
			//check if this position is empty and is not the player position
			if (generateMap.map[xPos, yPos] == 0 & newPlayerPosition != new Vector3(xPos - (width / 2) - .5f, 0, yPos - (height / 2) - .5f)) {

				//if it is, we will select this position as out spawn position, and then break out of this loop.
				Vector3 newPosition = new Vector3(xPos - (width / 2) - .5f, 0.5f, yPos - (height / 2) - .5f);
				newLadderPosition = newPosition;
				break;
			}
		}

		//create our ladder gameobject from the prefab and set its position and parent
		GameObject ladder = GameObject.Instantiate(Ladder);
		ladder.transform.SetParent(transform);
		ladder.transform.position = newLadderPosition;
	}
	//obsolute method, probably can remove
	private bool ValidPosition(Vector3 position) {
		NavMeshHit hit;
		if (NavMesh.SamplePosition(position, out hit, 1f, NavMesh.AllAreas)) {
			return true;
		} else {
			return false;
        } 
    } 
	public void GenerateMesh(int index) {
		//generate a mesh with a specific index 
		//set the width and height, and the seed and useRandomSeed to the one found using level data.
		width = levelData[index].width;
		height = levelData[index].height;
		seed = levelData[index].seed;
		useRandomSeed = levelData[index].useRandomSeed;
		randomFillPercent = levelData[index].randomFillPercent;

		//generate our new map, update our navmesh and generate our objects, enemies and player
		GenerateMap();
		gameObject.GetComponent<NavMeshGenerator>().UpdateNavMesh();
		GenerateEnemiesAndPlayer();
	}
	
	void GenerateMap() {
		generateMap = new GenerateMap(width, height, seed, useRandomSeed, randomFillPercent);

		//declare our voxel data map and our map storing our wall position
		var voxelMap = new VoxelData[width + 1, height + 1, 5];
		generateMap.map = new int[width,height];

		//full our map up
		generateMap.RandomFillMap();

		//apply smoothing
		for (int i = 0; i < 5; i ++) {
			generateMap.SmoothMap();
		}

		//process the map
		generateMap.ProcessMap(); 

		//loop through our map
        for (int x = 0; x < generateMap.map.GetLength(0); x++) {
            for (int y = 0; y < generateMap.map.GetLength(1); y++) {

				//overhall needed for the voxel visuals
				//set our voxel data (this needs to be update to make different types of voxel diaplay)
				var topVoxelData = new VoxelData();
				topVoxelData.Type = VoxelType.Dirt; 
				
				//if surrounded by 8 blocks, that block will be 2 blocks tall
				if (generateMap.GetSurroundingWallCount(x, y) == 8) { 
					voxelMap[x, y, 2] = topVoxelData;

                } else {
					//if this is on the edge, we can set this to a height of one
					if (x == 0 | y == 0 | x == width | y == height) {
						voxelMap[x, y, 1] = topVoxelData;
					} else {
						//if not, we just set it to the corrosponding map height
						voxelMap[x, y, generateMap.map[x, y]] = topVoxelData;
					}
				}
			}
        }

		voxelData = voxelMap;



		//create a gameobject to display the voxel mesh
		GameObject gm = new GameObject("Voxel Mesh");

		//add components
		VoxelMesh chunkEntity = gm.AddComponent<VoxelMesh>();
		gm.AddComponent<MeshRenderer>().material = voxelMaterial;
		gm.AddComponent<MeshFilter>();
		gm.AddComponent<MeshCollider>();

		//generate terrain data
		chunkEntity.GenerateTerrainData(5,width, voxelData);
		chunkEntity.UpdateMesh();

		//set its position, scale and parent
		chunkEntity.transform.localScale = new Vector3(1, 2, 1);
		chunkEntity.transform.position = new Vector3(-width / 2 - 1,-2.1f,-height / 2 - 1);
		chunkEntity.transform.SetParent(gameObject.transform);
	}

	
}
