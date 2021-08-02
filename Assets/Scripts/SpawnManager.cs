using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private Vector3 randomSpawnPos;
    private float spawnRange = 9f;
    public int enemyCount;
    private int waveNumber = 1;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        spawnPowerup();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        countEnemies();
    }

    private Vector3 GenerateSpawnPosition()
    {
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);
        randomSpawnPos = new Vector3(x, 0, z);

        return randomSpawnPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
        
    }

    void spawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    void countEnemies()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0 && !playerControllerScript.gameOver)
        {
            waveNumber++;
            spawnPowerup();
            SpawnEnemyWave(waveNumber);
        }
    }
}
