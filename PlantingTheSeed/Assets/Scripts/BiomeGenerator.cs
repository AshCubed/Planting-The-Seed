using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{
    public List<Biome> AllBiomePrefabs = new List<Biome>();
    private int BiomeNum;
    int rndBiome;

    public Player player;

    public GameObject rightEdge;
    public GameObject leftEdge;
    public GameObject spawnRight;
    public GameObject spawnLeft;

    private float colliderMoveRight;
    private float colliderMoveLeft;

    public GameObject deathCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        colliderMoveRight = 7.95f;
        colliderMoveLeft = -7.95f;

        BiomeNum = 1;

        rndBiome = Random.Range(0, 5);
        GameObject newgo = Instantiate(AllBiomePrefabs[rndBiome].camp.gameObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
        SpawnResources(newgo, rndBiome);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBiomee(string tag)
    {
        if (tag == "RightCampEdge")
        {
            if (BiomeNum != 5)
            {
                int rndTile = Random.Range(0, 10);
                GameObject newgo = Instantiate(AllBiomePrefabs[rndBiome].Prefabs[rndTile], new Vector3(spawnRight.transform.position.x, 0f, 0f), Quaternion.identity);
                SpawnResources(newgo, rndBiome);
                player.PlayerMessageBiomes(rndBiome);
                rightEdge.transform.position += new Vector3(colliderMoveRight, 0, 0);
                spawnRight.transform.position += new Vector3(colliderMoveRight, 0, 0);
                deathCollider.GetComponent<BoxCollider2D>().size += new Vector2(20, 0f);
                BiomeNum++;
            }
            else
            {
                rndBiome = Random.Range(0, 5);
                GameObject newgo = Instantiate(AllBiomePrefabs[rndBiome].camp.gameObject, new Vector3(spawnRight.transform.position.x, 0f, 0f), Quaternion.identity);
                SpawnResources(newgo, rndBiome);
                player.PlayerMessageBiomes(rndBiome);
                rightEdge.transform.position += new Vector3(colliderMoveRight, 0, 0);
                spawnRight.transform.position += new Vector3(colliderMoveRight, 0, 0);
                BiomeNum = 1;
            }
        }
        if (tag == "LeftCampEdge")
        {
            if (BiomeNum != 5)
            {
                int rndTile = Random.Range(0, 10);
                GameObject newgo = Instantiate(AllBiomePrefabs[rndBiome].Prefabs[rndTile], new Vector3(spawnLeft.transform.position.x, 0f, 0f), Quaternion.identity);
                SpawnResources(newgo, rndBiome);
                player.PlayerMessageBiomes(rndBiome);
                leftEdge.transform.position += new Vector3(colliderMoveLeft, 0, 0);
                spawnLeft.transform.position += new Vector3(colliderMoveLeft, 0, 0);
                BiomeNum++;
            }
            else
            {
                rndBiome = Random.Range(0, 5);
                GameObject newgo = Instantiate(AllBiomePrefabs[rndBiome].camp.gameObject, new Vector3(spawnLeft.transform.position.x, 0f, 0f), Quaternion.identity);
                SpawnResources(newgo, rndBiome);
                player.PlayerMessageBiomes(rndBiome);
                leftEdge.transform.position += new Vector3(colliderMoveLeft, 0, 0);
                spawnLeft.transform.position += new Vector3(colliderMoveLeft, 0, 0);
                BiomeNum = 1;
            }
        }
    }

    void SpawnResources(GameObject tile, int biome)
    {
        List<GameObject> resourceSpawnPoints = new List<GameObject>();

        for (int i = 0; i < tile.transform.childCount; i++)
        {
            if (tile.transform.GetChild(i).tag == "ResourceSpawn")
            {
                resourceSpawnPoints.Add(tile.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < resourceSpawnPoints.Count; i++)
        {
            int rnd = Random.Range(0, AllBiomePrefabs[biome].BiomeResources.Count);

            Instantiate(AllBiomePrefabs[biome].BiomeResources[rnd], resourceSpawnPoints[i].transform.position, Quaternion.identity);
        }
    }


}

[System.Serializable]
public class Biome
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public GameObject camp;
    [SerializeField]
    public List<GameObject> Prefabs = new List<GameObject>();
    [SerializeField]
    public List<GameObject> BiomeResources = new List<GameObject>();

}
