using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner
{
    private int enemyCount;
    private int eEnemyCount;
    private List<float> enemyCountList;
    public int waveCounter = 0;

    private GameObject enemyPrefab;
    private GameObject eEnemyPrefab;
    private List <GameObject> spawnPoints;
    private GameObject player;

    private System.Random randomGen = new System.Random();

    private GameManager gameManager;

    public EnemySpawner(GameManager gameManager, GameObject player, List<GameObject> spawnPoints, List<float> enemyCountList, GameObject enemyPrefab, GameObject eEnemyPrefab)
    {
        this.gameManager = gameManager;
        this.spawnPoints = spawnPoints;
        this.enemyCountList = enemyCountList;
        this.enemyPrefab = enemyPrefab;
        this.eEnemyPrefab = eEnemyPrefab;
        this.player = player;
    }

    public void ReadNextWave()
    {
        float enemysCount = enemyCountList[waveCounter];
        enemyCount = (int)enemysCount;
        eEnemyCount = (int)((enemysCount - enemyCount) * 10);
        Debug.Log("u have spawned: " + enemyCount + " normal enemies");
        Debug.Log("u have spawned: " + eEnemyCount + " elite enemies");
        waveCounter++;
    }

    public void ResetWaves()
    {
        waveCounter = 0;
    }

    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int rndSpawn = randomGen.Next(spawnPoints.Count);
            GameObject newEnemyObject = GameObject.Instantiate(enemyPrefab, spawnPoints[rndSpawn].transform.position, Quaternion.identity);
            Enemy newEnemyLogic = new Enemy(player, newEnemyObject);
            gameManager.aliveEnemies.Add(newEnemyLogic);

            yield return new WaitForSeconds(0.5f);
        }
    }
    public IEnumerator SpawnEEnemies()
    {
        for (int i = 0; i < eEnemyCount; i++)
        {
            int rndSpawn = randomGen.Next(spawnPoints.Count);
            GameObject newEnemyObject = GameObject.Instantiate(eEnemyPrefab, spawnPoints[rndSpawn].transform.position, Quaternion.identity);
            EliteEnemy newEnemyLogic = new EliteEnemy(player, newEnemyObject);
            gameManager.aliveEnemies.Add(newEnemyLogic);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
