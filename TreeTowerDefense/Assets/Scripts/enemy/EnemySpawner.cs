using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs = new GameObject[3]; // Reference to the enemy prefab you want to spawn
    public int enemyIndex = 0;
    public float spawnInterval = 3.0f; // Time interval between enemy spawns
    private float timer = 0.0f;
    public int enemyCount;

    //wave

    


    private Transform spawnPoint; //waypoint


    private void Start()
    {
        spawnPoint = waypoints.points[0];

    }

    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0.0f; // Reset the timer
        }
    }

    void SpawnEnemy()
    {
        // Instantiate the enemy prefab at the spawner's position
        Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.identity);
        if(enemyIndex >= enemyPrefabs.Length - 1) { enemyIndex = 0; }
        else { enemyIndex++; }

    }
}
