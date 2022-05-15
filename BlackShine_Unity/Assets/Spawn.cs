using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] GameObject enemy;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", 0.6f, 1f);
    }

    void SpawnEnemies()
    {
        int index = Random.Range(0, SpawnPoints.Length);
        Instantiate(enemy, SpawnPoints[index].position, Quaternion.identity);
    }
}
