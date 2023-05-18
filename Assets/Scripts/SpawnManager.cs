using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9;
    public List<GameObject> enemyPrefabs;
    public GameObject[] powerUpPrefabs;

    public int enemyCount;
    public int waveNumber = 1;

    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[randomPowerUp], GenereteSpawnPosition(), powerUpPrefabs[randomPowerUp].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;

            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
            
            int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[randomPowerUp], GenereteSpawnPosition(), powerUpPrefabs[randomPowerUp].transform.rotation);
        }
    }

    private Vector3 GenereteSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpaw;

        if (bossRound != 0)
        {
            miniEnemysToSpaw = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpaw = 1;
        }

        var boss = Instantiate(bossPrefab, GenereteSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpaw;
    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenereteSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int index = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[index], GenereteSpawnPosition(), enemyPrefabs[index].transform.rotation);
        }
    }
}
