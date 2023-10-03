using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Wave
    private List<int> waveEnemies1 = new List<int> { 1, 1, 1 };
    private List<int> waveEnemies2 = new List<int> { 2, 2, 2 };
    private List<int> waveEnemies3 = new List<int> { 3, 3, 3 };
    private List<int> waveEnemies4 = new List<int> { 4, 4, 4 };

    // Prefabs
    public GameObject[] enemyPrefabs = new GameObject[3];
    public List<List<int>> waveDataList = new List<List<int>>(); // Initialize as an empty list

    private List<GameObject> enemiesToSpawn = new List<GameObject>();

    // Timer for enemy spawning
    private float spawnTimer = 0f;
    public float spawnInterval = 2.0f;

    // Index to track the current enemy prefab
    private int enemyIndex = 0;

    public int waveCount = 0;
    private bool waveActive = false;
    private bool startWavesOnButtonPress = true; // Flag to start waves on button press

    private Transform spawnPoint;

    private Coroutine spawnCoroutine; // Coroutine for spawning enemies

    private void Start()
    {
        // Add your wave data lists to waveDataList
        waveDataList.Add(waveEnemies1);
        waveDataList.Add(waveEnemies2);
        waveDataList.Add(waveEnemies3);
        waveDataList.Add(waveEnemies4);

        // Initialize the spawnPoint (you should set it to an appropriate waypoint)
        spawnPoint = waypoints.points[0];

        if (startWavesOnButtonPress)
        {
            // Don't start the first wave immediately, wait for a button press
            waveCount = -1; // Set waveCount to -1 to indicate waiting for button press
        }
        else
        {
            StartNextWave();
        }
    }

    private void Update()
    {
        if (startWavesOnButtonPress && waveCount == -1)
        {
            // Check if the "R" key is pressed to start the first wave
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartNextWave();
            }
        }
        else
        {
            // Check if the "R" key is pressed to start the next wave
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (!waveActive)
                {
                    StartNextWave();
                }
            }
        }

        //spawnTimer += Time.deltaTime;

        //if (waveActive && spawnTimer >= spawnInterval)
        //{
        //    SpawnEnemy();
        //    spawnTimer = 0.0f; // Reset the timer
        //}
    }

    void StartNextWave()
    {
        waveCount++;
        if (waveCount < waveDataList.Count)
        {
            PrepareNextWave();
            // Reset the spawn timer when starting a new wave
            spawnTimer = 0.0f;

            // Start the coroutine to spawn enemies at intervals
            spawnCoroutine = StartCoroutine(SpawnEnemiesAtIntervals());
        }
        else
        {
            Debug.Log("All waves are complete.");
        }
    }

    void PrepareNextWave()
    {
        List<int> enemyCounts = waveDataList[waveCount]; // Access the correct wave's enemy counts

        for (int j = 0; j < enemyCounts.Count; j++)
        {
            if (j < enemyPrefabs.Length)
            {
                for (int k = 0; k < enemyCounts[j]; k++)
                {
                    enemiesToSpawn.Add(enemyPrefabs[j]);
                }
            }
        }
        waveActive = true; // Start the wave
    }

    // Coroutine to spawn enemies at intervals
    private IEnumerator SpawnEnemiesAtIntervals()
    {
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            GameObject enemyPrefab = enemiesToSpawn[i];
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }

        enemiesToSpawn = new List<GameObject>{ };

        // All enemies for the current wave have been spawned.
        waveActive = false;
    }

    //void SpawnEnemy()
    //{
    //    if (enemiesToSpawn.Count > 0)
    //    {
    //        GameObject enemyPrefab = enemiesToSpawn[0];
    //        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    //        enemiesToSpawn.RemoveAt(0);

    //        // Don't increment enemyIndex here
    //    }
    //    else
    //    {
    //        // All enemies for the current wave have been spawned.
    //        waveActive = false;
    //    }
    //}
}
