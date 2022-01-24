using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine.AI;  
public class MapGenerator : MonoBehaviour {
	//our list of levels
	public List<LevelData> levelData = new List<LevelData>();

	//some variables that are used a lot
	private int width = 100;
	private int height = 100;
	private string seed;
	private bool useRandomSeed;
	 
	//our map data and our voxel data
	public int[,] map;
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
    private void GenerateEnemiesAndPlayer() {
		//create a parent for our monsters created
		GameObject monsterParent = new GameObject("Monster Parent");
		monsterParent.transform.SetParent(gameObject.transform);
		//create a parent for our objects created
		GameObject objectParents = new GameObject("Object Parent");
		objectParents.transform.SetParent(gameObject.transform);

		//loop though our map
		for (int x = 0; x < map.GetLength(0); x++) {
			for (int y = 0; y < map.GetLength(1); y++) {
				 
				float scale = 10f; 

				//if there is not a wall here
				if (map[x, y] == 0) {

					//calculate our new position
					Vector3 newPosition = new Vector3(x - (width / 2) - .5f, 0, y - (height / 2) - .5f);

					//if a random value is within the chance of a monster spawning
					if (Random.value < levelData[0].monsterChance) {

						//check that this is a valid position in navmesh (this is obsolute, prob can remove now)
						if (ValidPosition(newPosition)) {

							//create our new enemy
							GameObject enemy = new GameObject("Monster", typeof(MeshFilter),  typeof(MeshRenderer),  typeof(CapsuleCollider),  typeof(Rigidbody));
							enemy.GetComponent<CapsuleCollider>().height = 3;
							enemy.GetComponent<CapsuleCollider>().center = new Vector3(0,1,0); 
							enemy.transform.SetParent(monsterParent.transform);
							enemy.transform.position = newPosition;

							//select the type of monster from a array
							Type newComponent = levelData[0].monsterTypes[Random.Range(0, levelData[0].monsterTypes.Length)];
							//get our assembly address of the class
							string address = "Monsters." + newComponent.Name + ", " + typeof(MonsterType).Assembly;
							//add the chosen component in
							enemy.AddComponent(Type.GetType(address));  
						}

						//else check if perlin noise at that position is within the non interactable object chance
					} else if (Mathf.PerlinNoise(((float)x / (float)width) * (scale / 2), ((float)y / (float)height) * (scale / 2)) < levelData[0].NoninteractableChance) {
						
						float rand = Random.value;
						int index = Random.Range(0, levelData[0].NoninteractableObject.Count); 

						//using a random value, loop through the objects and choose what index we use by comparing to their chance of spawning
						for (int i = 0; i < levelData[0].NoninteractableObject.Count; i++) {
							if (rand <= levelData[0].NoninteractableObjectChance[i]) {
								index = i;
								break;
							}
						}

						//create our gameobject using the prefab of the chosen object
						GameObject ambientItem = GameObject.Instantiate(levelData[0].NoninteractableObject[index]);
                        ambientItem.transform.rotation = Random.value > .5f ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
						ambientItem.transform.SetParent(objectParents.transform);
						ambientItem.transform.position = newPosition;

						//else check if perlin noise at that position is within the non interactable object chance
					} else if (Mathf.PerlinNoise(((float)x / (float)width) * scale, ((float)y / (float)height) * scale) < levelData[0].InteractableChance) {

						float rand = Random.value;
						int index = Random.Range(0, levelData[0].NoninteractableObject.Count);

						//using a random value, loop through the objects and choose what index we use by comparing to their chance of spawning
						for (int i = 0; i < levelData[0].NoninteractableObject.Count; i++) {
							if (rand <= levelData[0].InteractableObjectChance[i]) {
								index = i;
								break;
							}
						}

						//create our gameobject using the prefab of the chosen object
						GameObject ambientItem = GameObject.Instantiate(levelData[0].InteractableObject[index]); 
						ambientItem.AddComponent<NavMeshObstacle>();  
						ambientItem.transform.rotation = Random.value > .5f ? Quaternion.Euler(0,0,0) : Quaternion.Euler(0, 180, 0);

						//give it a mineable tag and a collider so our tools can detect it
						ambientItem.transform.tag = "Mineable"; 
						ambientItem.AddComponent<BoxCollider>().isTrigger = true;
						ambientItem.transform.SetParent(objectParents.transform);
						ambientItem.transform.position = newPosition;
					}
				}
			}
        }

		//set our default position to the current position
		Vector3 newPlayerPosition = pTransform.position;

		//attempt choosing a position 30 times, if not the default position is used.
        for (int i = 0; i < 30; i++) {

			//choose a random x and y position on the map
			int xPos = Random.Range(1, map.GetLength(0) - 1);
			int yPos = Random.Range(1, map.GetLength(1) - 1);

			//check if this position is empty
            if (map[xPos, yPos] == 0) {

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
			int xPos = Random.Range(1, map.GetLength(0) - 1);
			int yPos = Random.Range(1, map.GetLength(1) - 1);

			//check if this position is empty and is not the player position
			if (map[xPos, yPos] == 0 & newPlayerPosition != new Vector3(xPos - (width / 2) - .5f, 0, yPos - (height / 2) - .5f)) {

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

		//generate our new map, update our navmesh and generate our objects, enemies and player
		GenerateMap();
		gameObject.GetComponent<NavMeshGenerator>().UpdateNavMesh();
		GenerateEnemiesAndPlayer();
	}
	
	void GenerateMap() {
		//declare our voxel data map and our map storing our wall position
		var voxelMap = new VoxelData[width + 1, height + 1, 5];
		map = new int[width,height];

		//full our map up
		RandomFillMap();

		//apply smoothing
		for (int i = 0; i < 5; i ++) {
			SmoothMap();
		}

		//process the map
		ProcessMap (); 

		//loop through our map
        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {

				//overhall needed for the voxel visuals
				//set our voxel data (this needs to be update to make different types of voxel diaplay)
				var topVoxelData = new VoxelData();
				topVoxelData.Type = VoxelType.Dirt; 
				
				//if surrounded by 8 blocks, that block will be 2 blocks tall
				if (GetSurroundingWallCount(x, y) == 8) { 
					voxelMap[x, y, 2] = topVoxelData;

                } else {
					//if this is on the edge, we can set this to a height of one
					if (x == 0 | y == 0 | x == width | y == height) {
						voxelMap[x, y, 1] = topVoxelData;
					} else {
						//if not, we just set it to the corrosponding map height
						voxelMap[x, y, map[x, y]] = topVoxelData;
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

	 //used to generate our map ----------------------------------------------
	void ProcessMap() {
		List<List<Coord>> wallRegions = GetRegions (1);
		int wallThresholdSize = 50;

		foreach (List<Coord> wallRegion in wallRegions) {
			if (wallRegion.Count < wallThresholdSize) {
				foreach (Coord tile in wallRegion) {
					map[tile.tileX,tile.tileY] = 0;
				}
			}
		}

		List<List<Coord>> roomRegions = GetRegions (0);
		int roomThresholdSize = 50;
		List<Room> survivingRooms = new List<Room> ();
		
		foreach (List<Coord> roomRegion in roomRegions) {
			if (roomRegion.Count < roomThresholdSize) {
				foreach (Coord tile in roomRegion) {
					map[tile.tileX,tile.tileY] = 1;
				}
			}
			else {
				survivingRooms.Add(new Room(roomRegion, map));
			}
		}
		survivingRooms.Sort ();
		survivingRooms [0].isMainRoom = true;
		survivingRooms [0].isAccessibleFromMainRoom = true;

		ConnectClosestRooms (survivingRooms);
	}

	void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false) {

		List<Room> roomListA = new List<Room> ();
		List<Room> roomListB = new List<Room> ();

		if (forceAccessibilityFromMainRoom) {
			foreach (Room room in allRooms) {
				if (room.isAccessibleFromMainRoom) {
					roomListB.Add (room);
				} else {
					roomListA.Add (room);
				}
			}
		} else {
			roomListA = allRooms;
			roomListB = allRooms;
		}

		int bestDistance = 0;
		Coord bestTileA = new Coord ();
		Coord bestTileB = new Coord ();
		Room bestRoomA = new Room ();
		Room bestRoomB = new Room ();
		bool possibleConnectionFound = false;

		foreach (Room roomA in roomListA) {
			if (!forceAccessibilityFromMainRoom) {
				possibleConnectionFound = false;
				if (roomA.connectedRooms.Count > 0) {
					continue;
				}
			}

			foreach (Room roomB in roomListB) {
				if (roomA == roomB || roomA.IsConnected(roomB)) {
					continue;
				}
			
				for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA ++) {
					for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB ++) {
						Coord tileA = roomA.edgeTiles[tileIndexA];
						Coord tileB = roomB.edgeTiles[tileIndexB];
						int distanceBetweenRooms = (int)(Mathf.Pow (tileA.tileX-tileB.tileX,2) + Mathf.Pow (tileA.tileY-tileB.tileY,2));

						if (distanceBetweenRooms < bestDistance || !possibleConnectionFound) {
							bestDistance = distanceBetweenRooms;
							possibleConnectionFound = true;
							bestTileA = tileA;
							bestTileB = tileB;
							bestRoomA = roomA;
							bestRoomB = roomB;
						}
					}
				}
			}
			if (possibleConnectionFound && !forceAccessibilityFromMainRoom) {
				CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
			}
		}

		if (possibleConnectionFound && forceAccessibilityFromMainRoom) {
			CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
			ConnectClosestRooms(allRooms, true);
		}

		if (!forceAccessibilityFromMainRoom) {
			ConnectClosestRooms(allRooms, true);
		}
	}

	void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB) {
		Room.ConnectRooms (roomA, roomB);
		//Debug.DrawLine (CoordToWorldPoint (tileA), CoordToWorldPoint (tileB), Color.green, 100);

		List<Coord> line = GetLine (tileA, tileB);
		foreach (Coord c in line) {
			DrawCircle(c,5);
		}
	}

	void DrawCircle(Coord c, int r) {
		for (int x = -r; x <= r; x++) {
			for (int y = -r; y <= r; y++) {
				if (x*x + y*y <= r*r) {
					int drawX = c.tileX + x;
					int drawY = c.tileY + y;
					if (IsInMapRange(drawX, drawY)) {
						map[drawX,drawY] = 0;
					}
				}
			}
		}
	}

	List<Coord> GetLine(Coord from, Coord to) {
		List<Coord> line = new List<Coord> ();

		int x = from.tileX;
		int y = from.tileY;

		int dx = to.tileX - from.tileX;
		int dy = to.tileY - from.tileY;

		bool inverted = false;
		int step = Math.Sign (dx);
		int gradientStep = Math.Sign (dy);

		int longest = Mathf.Abs (dx);
		int shortest = Mathf.Abs (dy);

		if (longest < shortest) {
			inverted = true;
			longest = Mathf.Abs(dy);
			shortest = Mathf.Abs(dx);

			step = Math.Sign (dy);
			gradientStep = Math.Sign (dx);
		}

		int gradientAccumulation = longest / 2;
		for (int i =0; i < longest; i ++) {
			line.Add(new Coord(x,y));

			if (inverted) {
				y += step;
			}
			else {
				x += step;
			}

			gradientAccumulation += shortest;
			if (gradientAccumulation >= longest) {
				if (inverted) {
					x += gradientStep;
				}
				else {
					y += gradientStep;
				}
				gradientAccumulation -= longest;
			}
		}

		return line;
	}

	Vector3 CoordToWorldPoint(Coord tile) {
		return new Vector3 (-width / 2 + .5f + tile.tileX, 2, -height / 2 + .5f + tile.tileY);
	}

	List<List<Coord>> GetRegions(int tileType) {
		List<List<Coord>> regions = new List<List<Coord>> ();
		int[,] mapFlags = new int[width,height];

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
					List<Coord> newRegion = GetRegionTiles(x,y);
					regions.Add(newRegion);

					foreach (Coord tile in newRegion) {
						mapFlags[tile.tileX, tile.tileY] = 1;
					}
				}
			}
		}

		return regions;
	}

	List<Coord> GetRegionTiles(int startX, int startY) {
		List<Coord> tiles = new List<Coord> ();
		int[,] mapFlags = new int[width,height];
		int tileType = map [startX, startY];

		Queue<Coord> queue = new Queue<Coord> ();
		queue.Enqueue (new Coord (startX, startY));
		mapFlags [startX, startY] = 1;

		while (queue.Count > 0) {
			Coord tile = queue.Dequeue();
			tiles.Add(tile);

			for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++) {
				for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++) {
					if (IsInMapRange(x,y) && (y == tile.tileY || x == tile.tileX)) {
						if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
							mapFlags[x,y] = 1;
							queue.Enqueue(new Coord(x,y));
						}
					}
				}
			}
		}
		return tiles;
	}

	bool IsInMapRange(int x, int y) {
		return x >= 0 && x < width && y >= 0 && y < height;
	}


	void RandomFillMap() {
		if (useRandomSeed) {
			seed = Time.time.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (x == 0 || x == width-1 || y == 0 || y == height -1) {
					map[x,y] = 1;
				}
				else {
					map[x,y] = (pseudoRandom.Next(0,100) < levelData[0].randomFillPercent)? 1: 0;
				}
			}
		}
	}

	void SmoothMap() {
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				int neighbourWallTiles = GetSurroundingWallCount(x,y);

				if (neighbourWallTiles > 4)
					map[x,y] = 1;
				else if (neighbourWallTiles < 4)
					map[x,y] = 0;

			}
		}
	}

	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (IsInMapRange(neighbourX,neighbourY)) {
					if (neighbourX != gridX || neighbourY != gridY) {
						wallCount += map[neighbourX,neighbourY];
					}
				}
				else {
					wallCount ++;
				}
			}
		}

		return wallCount;
	}

	struct Coord {
		public int tileX;
		public int tileY;

		public Coord(int x, int y) {
			tileX = x;
			tileY = y;
		}
	}


	class Room : IComparable<Room> {
		public List<Coord> tiles;
		public List<Coord> edgeTiles;
		public List<Room> connectedRooms;
		public int roomSize;
		public bool isAccessibleFromMainRoom;
		public bool isMainRoom;

		public Room() {
		}

		public Room(List<Coord> roomTiles, int[,] map) {
			tiles = roomTiles;
			roomSize = tiles.Count;
			connectedRooms = new List<Room>();

			edgeTiles = new List<Coord>();
			foreach (Coord tile in tiles) {
				for (int x = tile.tileX-1; x <= tile.tileX+1; x++) {
					for (int y = tile.tileY-1; y <= tile.tileY+1; y++) {
						if (x == tile.tileX || y == tile.tileY) {
							if (map[x,y] == 1) {
								edgeTiles.Add(tile);
							}
						}
					}
				}
			}
		}

		public void SetAccessibleFromMainRoom() {
			if (!isAccessibleFromMainRoom) {
				isAccessibleFromMainRoom = true;
				foreach (Room connectedRoom in connectedRooms) {
					connectedRoom.SetAccessibleFromMainRoom();
				}
			}
		}

		public static void ConnectRooms(Room roomA, Room roomB) {
			if (roomA.isAccessibleFromMainRoom) {
				roomB.SetAccessibleFromMainRoom ();
			} else if (roomB.isAccessibleFromMainRoom) {
				roomA.SetAccessibleFromMainRoom();
			}
			roomA.connectedRooms.Add (roomB);
			roomB.connectedRooms.Add (roomA);
		}

		public bool IsConnected(Room otherRoom) {
			return connectedRooms.Contains(otherRoom);
		}

		public int CompareTo(Room otherRoom) {
			return otherRoom.roomSize.CompareTo (roomSize);
		}
	}

}
