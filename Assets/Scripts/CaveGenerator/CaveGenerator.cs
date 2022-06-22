using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine.AI;  
public class CaveGenerator : MonoBehaviour {
	//our list of levels
	public List<LevelData> levelData = new List<LevelData>();

	private int width = 100;
	private int height = 100;
	private string seed;
	private bool useRandomSeed;
	private int randomFillPercent;
	MapGenerator generateMap;
	//our map data and our voxel data
	public VoxelData[,,] voxelData;

	//the material for the voxels made
	[SerializeField] Material voxelMaterial;
	[SerializeField] Material floorMaterial;

	//player transform position
	private Transform pTransform;
	//ladder prefab
	[SerializeField] GameObject Ladder;
	public int currentLevel = 0;
	public Texture2D floorTexture;

	[SerializeField] GameObject Instance; 
	Color[] TextureColour = new Color[5];

	void Start() {
		pTransform = GameObject.FindGameObjectWithTag("Player").transform; 
		GenerateMesh(); 
	} 
	public void IncreaseLevel()
    {
		foreach (Transform child in transform)
		{
			GameObject.Destroy(child.gameObject);
		}
		currentLevel++;
		GenerateMesh();
	}
	private int SelectFromListChance(float[] chances)
    {
		float total = 0;
        foreach (var item in chances)
        {
			total += item;
        }

		float index = Random.Range(0, total);
		float currentTotal = 0;
		int counter = 0;
        foreach (var item in chances)
        {
			counter++;
			currentTotal += item;

			if (index <= currentTotal)
            {
				return counter;

			}

		}

		return 0;
    }
	private void spawnEnemy(Transform monsterParent, Vector3 newPosition)
    { 
		//create our new enemy
		GameObject enemy = new GameObject("Monster", typeof(MeshFilter), typeof(MeshRenderer), typeof(CapsuleCollider), typeof(Rigidbody));
		enemy.GetComponent<CapsuleCollider>().height = 3;
		enemy.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
		enemy.transform.SetParent(monsterParent);
		enemy.transform.position = newPosition;

		//select the type of monster from a array
		int iterations = 0;
		string address = "";
		bool monsterEnabled = false;
		while (monsterEnabled == false & iterations < 20)
		{
			iterations++;
			int index = Random.Range(0, levelData[0].monsterTypes.Length);
			int a = SelectFromListChance(levelData[0].monsterTypeChance);
			Debug.LogError(a);
			monsterEnabled = levelData[0].monsterEnabled[index];
			Type newComponent = levelData[0].monsterTypes[index];

			//get our assembly address of the class
			address = "Monsters." + newComponent.Name + ", " + typeof(MonsterType).Assembly;

			Debug.Log("We spawned a " + newComponent.Name + " is " + monsterEnabled);
		} 

		//add the chosen component in
		enemy.AddComponent(Type.GetType(address));
	}
	private void spawnObject(Transform objectParents, Vector3 newPosition, Vector2 texturePosition)
    {
		//float rand = Random.value;
		//int index = Random.Range(0, levelData[currentLevel].NoninteractableObject.Count);

		////using a random value, loop through the objects and choose what index we use by comparing to their chance of spawning
		//for (int i = 0; i < levelData[currentLevel].NoninteractableObject.Count; i++)
		//{
		//	if (rand <= levelData[currentLevel].NoninteractableObjectChance[i])
		//	{
		//		index = i;
		//		break;
		//	}
		//}
		int chosenIndex = SelectFromListChance(levelData[currentLevel].NoninteractableObjectChance.ToArray());

		//Debug.LogError("ss "+ levelData[currentLevel].NoninteractableObjectChance.Count);
		//Debug.LogError("aa " + levelData[currentLevel].NoninteractableObject.Count); 
		//Debug.LogError("qq " + chosenIndex); 

		//create our gameobject using the prefab of the chosen object
		GameObject ambientItem = GameObject.Instantiate(levelData[currentLevel].NoninteractableObject[chosenIndex - 1]);
		//ambientItem.transform.rotation = Random.value > .5f ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
		ambientItem.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0) * ambientItem.transform.rotation;
		ambientItem.transform.SetParent(objectParents);
		ambientItem.transform.position = newPosition;

		//else check if perlin noise at that position is within the non interactable object chance  
		floorTexture.SetPixel((int)texturePosition.x, (int)texturePosition.y, TextureColour[0]);
	}  
	private Vector3 spawnPlayer()
    {
		//set our default position to the current position
		Vector3 position = pTransform.position;

		//attempt choosing a position 30 times, if not the default position is used.
		for (int i = 0; i < 30; i++)
		{ 
			//choose a random x and y position on the map
			int xPos = Random.Range(1, generateMap.map.GetLength(0) - 1);
			int yPos = Random.Range(1, generateMap.map.GetLength(1) - 1);

			//check if this position is empty
			if (generateMap.map[xPos, yPos] == 0)
			{ 
				//if it is, we will select this position as out spawn position, and then break out of this loop.
				Vector3 newPosition = new Vector3(xPos - (width / 2) - .5f, 0, yPos - (height / 2) - .5f);
				position = newPosition;
				break;
			}
		}

		//update our position
		pTransform.position = position;
		GameObject.FindGameObjectWithTag("Player").transform.position = position; 
		return position;
	}
    private Vector3 spawnLadder(Vector3 playerPosition)
    {
		Vector3 position = pTransform.position;
		for (int i = 0; i < 30; i++)
		{

			//choose a random x and y position on the map
			int xPos = Random.Range(1, generateMap.map.GetLength(0) - 1);
			int yPos = Random.Range(1, generateMap.map.GetLength(1) - 1);

			//check if this position is empty and is not the player position
			if (generateMap.GetSurroundingWallCount(xPos, yPos) == 0 & playerPosition != new Vector3(xPos - (width / 2) - .5f, 0, yPos - (height / 2) - .5f))
			{ 
				//if it is, we will select this position as out spawn position, and then break out of this loop.
				Vector3 newPosition = new Vector3(xPos - (width / 2) - .5f, 0.5f, yPos - (height / 2) - .5f);
				position = newPosition;
				break;
			}
		}

		//create our ladder gameobject from the prefab and set its position and parent
		GameObject ladderObject = GameObject.Instantiate(Ladder);
		ladderObject.transform.SetParent(transform);
		position = new Vector3(position.x, 0f, position.z);
		ladderObject.transform.position = position;

		return position;
	} 
	private bool ValidPosition(Vector3 position) {
		NavMeshHit hit;
		if (NavMesh.SamplePosition(position, out hit, 1f, NavMesh.AllAreas)) {
			return true;
		} else {
			return false;
        } 
    } 
	private void spawnWalls()
    {
		GameObject parent = new GameObject("Wall Parent");
		parent.transform.SetParent(gameObject.transform);

		string t = "";
		for (int x = 0; x < generateMap.map.GetLength(0); x++)
		{ 
			for (int y = 0; y < generateMap.map.GetLength(1); y++)
			{
				t += generateMap.map[x, y].ToString();
				if (generateMap.map[x, y] == 1)
				{ 
					float rand = Random.value;
					int Itemindex = Random.Range(0, levelData[currentLevel].WallObject.Count);
					 
					//using a random value, loop through the objects and choose what index we use by comparing to their chance of spawning
					for (int i = 0; i < levelData[currentLevel].WallObject.Count; i++)
					{
						if (rand <= levelData[currentLevel].WallChance[i])
						{
							Itemindex = i;
							break;
						}
					}

					//create our gameobject using the prefab of the chosen object
					GameObject WallObject = GameObject.Instantiate(levelData[currentLevel].WallObject[Itemindex]);
					//ambientItem.transform.rotation = Random.value > .5f ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
					WallObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0) * WallObject.transform.rotation;
					WallObject.GetComponent<MeshRenderer>().material.color = levelData[currentLevel].colours[4];
					WallObject.transform.SetParent(parent.transform);
					WallObject.transform.position = new Vector3(x - (width / 2) - .5f, 0, y - (height / 2) - .5f);
				}
			}
		}
	}
	public void GenerateMesh() {
		 
		//generate a mesh with a specific index 
		//set the width and height, and the seed and useRandomSeed to the one found using level data.
		width = levelData[currentLevel].width;
		height = levelData[currentLevel].height;
		seed = levelData[currentLevel].seed;
		useRandomSeed = levelData[currentLevel].useRandomSeed;
		randomFillPercent = levelData[currentLevel].randomFillPercent;
		TextureColour = levelData[currentLevel].colours;
		floorTexture = new Texture2D(width, height);
		floorTexture.Apply();

		//generate our new map, update our navmesh and generate our objects, enemies and player
		GenerateMap();
		gameObject.GetComponent<NavMeshGenerator>().UpdateNavMesh();

		for (int x = 0; x < generateMap.map.GetLength(0); x++) {
			for (int y = 0; y < generateMap.map.GetLength(1); y++) {
                if (generateMap.map[x,y] == 1) {
					floorTexture.SetPixel(x, y, TextureColour[4]);

				}else if (generateMap.GetSurroundingWallCount(x, y) > 1) {
					floorTexture.SetPixel(x, y, TextureColour[3]);

				} else if (generateMap.GetSurroundingWallCount(x, y) > 0) {
					floorTexture.SetPixel(x, y, TextureColour[2]);

				} else {
					floorTexture.SetPixel(x, y, TextureColour[1]);

				}

			};
		};

		//create a parent for our monsters created
		GameObject monsterParent = new GameObject("Monster Parent");
		monsterParent.transform.SetParent(gameObject.transform);

		//create a parent for our objects created
		GameObject objectParents = new GameObject("Object Parent");
		objectParents.transform.SetParent(gameObject.transform);

		//loop though our map
		for (int x = 0; x < generateMap.map.GetLength(0); x++)
		{
			Debug.LogError(x);
			for (int y = 0; y < generateMap.map.GetLength(1); y++)
			{ 
				//if there is not a wall here
				if (generateMap.map[x, y] == 0)
				{
					//calculate our new position
					float scale = 10f;
					Vector3 newPosition = new Vector3(x - (width / 2) - .5f, 0, y - (height / 2) - .5f);

					//if a random value is within the chance of a monster spawning
					if (Random.value < levelData[0].monsterChance)
					{
						//check that this is a valid position in navmesh (this is obsolute, prob can remove now)
						if (ValidPosition(newPosition))
						{
							spawnEnemy(monsterParent.transform, newPosition);
						}
					}
					else if (Mathf.PerlinNoise(((float)x / (float)width) * (scale / 2), ((float)y / (float)height) * (scale / 2)) < levelData[0].NoninteractableChance)
					{
						spawnObject(objectParents.transform, newPosition, new Vector2(x, y));
					}
				}
			}
		}
		 
		Vector3 newPlayerPosition = spawnPlayer();  
		spawnLadder(newPlayerPosition);


		floorTexture.Apply();
		floorTexture.filterMode = FilterMode.Point;
		floorMaterial.SetTexture("_BaseMap", floorTexture);

		Minimap.floorTexture = floorTexture;

		spawnWalls();




	}
	
	void GenerateMap() { 
		generateMap = new MapGenerator(width, height, seed, useRandomSeed, randomFillPercent);

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
				if (generateMap.map[x, y] == 1)
				{  
					float rand = Random.value;
					int index = Random.Range(0, levelData[0].WallObject.Count);


					//using a random value, loop through the objects and choose what index we use by comparing to their chance of spawning
					for (int i = 0; i < levelData[0].WallObject.Count; i++)
					{
						if (rand <= levelData[0].WallChance[i])
						{
							index = i;
							break;
						}
					} 
				}
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
	} 
}
