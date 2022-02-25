using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloudGenerator : MonoBehaviour
{
	//generating noise map
	//https://www.redblobgames.com/maps/terrain-from-noise/
	//<< for island border map
	//set it to spawn as gameobjects under the paster object
	//set it to move with the cloud movement script
	//random sizes
	//set each cloud to be random (previously all used the same perlin noise as it gave same coords each cloud)
	//set to use specified material
	//set noise to output to image for debugging (generation deos not look as should be)
	//reworked to use worly noise obtained online >>
	//https://github.com/ConficturaStudios/noise-generator-unity/blob/master/Assets/Scripts/Noise/Worley.cs#L8


	[SerializeField] Material cloudMaterial;

	[SerializeField] RawImage output;
    void Start()
    {
		Random.InitState(99);
		GenerateCloud();
	}
	void GenerateCloud() {

		int resolution = 40; 

		int[,] cloudMap = new int[resolution, resolution];

		float randomSeed = Random.Range(0f, 1f);
		Texture2D text = new Texture2D(resolution, resolution);
		for (int x = 0; x < resolution; x++) {
			for (int z = 0; z < resolution; z++) {
				float data = fillTexture(new Vector2(x, z), new Vector2(resolution, resolution));
				int rounded = data >= 0.5f ? 1 : 0;

				cloudMap[x, z] = rounded;
				text.SetPixel(x, z, new Color(data, data, data));
				
			}
		}
		text.Apply();
		text.filterMode = FilterMode.Point;
		output.texture = text;

		GameObject cloud = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cloud.transform.position = transform.position + new Vector3(Random.Range(-400f,400f), Random.Range(-2f, 2f) , Random.Range(-200f, 200f));
		cloud.transform.localScale = transform.localScale + new Vector3(Random.Range(-100f, 100f), Random.Range(-50f, 200f), Random.Range(-50f, 50f));
		cloud.AddComponent<CloudMovement>();
		cloud.GetComponent<MeshRenderer>().material = cloudMaterial;
		MeshGenerator meshGen = GetComponent<MeshGenerator>();
		meshGen.cave = cloud.GetComponent<MeshFilter>();
		meshGen.is2D = true;
		meshGen.GenerateMesh(cloudMap, 1);
	}

	float fillTexture(Vector2 coords, Vector2 size) {
		Vector2 position = coords / size;
		Vector2 c = Worley.Generate(position.x, position.y, (int)Random.Range(Random.value, Random.value), 100, 100);
		float noise = c.x;
		float nx = position.x - 0.5f;
		float ny = position.y - 0.5f;

		float d = 2 * Mathf.Max(Mathf.Abs(nx), Mathf.Abs(ny));

		float e = (1 + noise - d) / 2;

		return e;
    }
	// Update is called once per frame
	float time = 0;
	void Update()
    {
		time += Time.deltaTime;
        if (time >= 1) {
			GenerateCloud();
			time = 0;
        }
    }
}
