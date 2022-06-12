using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnGroup;

    [SerializeField]
    private GameObject roadPrefab;
    [SerializeField]
    private GameObject grassPrefab;
    [SerializeField]
    private GameObject initialSpawnPoint;
    [SerializeField]
    private GameObject[] obstaclePrefabs;
    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField]
    private float spawnObstacleChance;
    [SerializeField]
    private float spawnCoinChance;
    [SerializeField]
    private int initialCount;

    Vector3 spawnPoint;
    private const int lineCellLenght = 20;
    private float step = 2f;


    void Start()
    {
        spawnPoint = initialSpawnPoint.transform.position;
        //Spawn initial count of lines
        for (int i = 0; i < initialCount; i++)
        {
            SpawnNewLine();
        }
    }

    void SpawnRoadLine(Vector3 spawnPoint)
    {
        Instantiate(roadPrefab, spawnPoint, roadPrefab.transform.rotation, spawnGroup.transform);
    }

    void SpawnGrassLine(Vector3 spawnPoint)
    {
        GameObject line = Instantiate(grassPrefab, spawnPoint, roadPrefab.transform.rotation, spawnGroup.transform);
        //Fill grass line with obstacles and coins
        for(int i = 0; i < lineCellLenght; i++)
        {
            //Go through each cell
            Vector3 currentPoint = new Vector3(spawnPoint.x - lineCellLenght * step / 2 + i * 2, spawnPoint.y, spawnPoint.z);
            if (Random.value < spawnObstacleChance)
            {
                //Spawn obstacle
                Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], currentPoint, Quaternion.identity, line.transform);

            }
            else if(Random.value < spawnCoinChance)
            {
                //Spawn coin
                 Instantiate(coinPrefab, currentPoint, coinPrefab.transform.rotation, line.transform);
            }
        }
    }

    public void SpawnNewLine()
    {
        //Chances of spawn each type of lines are equal
        if(Random.value < 0.5)
        {
            SpawnGrassLine(spawnPoint);
            spawnPoint += Vector3.forward * step;
        }
        else 
        { 
            SpawnRoadLine(spawnPoint);
            spawnPoint += Vector3.forward * step;
        }
    }
}
