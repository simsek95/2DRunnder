using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Transform player;
    [SerializeField] Transform spawnPointL;
    [SerializeField] Transform spawnPointR;
    [SerializeField] int totalEnemyAmount = 5;
    float alreadySpawnedEnemies = 0;
    [SerializeField] float maxTimeBetweenSpawn = 5;
    [SerializeField] GameObject enemyPrefab;
    private void Start()
    {
        FindPlayer();
        StartCoroutine(Spawn());
    }

    private void FixedUpdate()
    {
        if(player != null)
        transform.position = player.position;
        else FindPlayer();
    }

    IEnumerator Spawn()
    {
        if (alreadySpawnedEnemies >= totalEnemyAmount) yield return null;
        else
        {


        Vector2 spawnPoint;
        if (yesNo()) spawnPoint = spawnPointL.position;
        else spawnPoint = spawnPointR.position;

        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        alreadySpawnedEnemies++;
        
        yield return new WaitForSeconds(maxTimeBetweenSpawn);
        StartCoroutine(Spawn());
        }
    }

    bool yesNo()
    {
        float i = Random.value;
        return (i < 0.5f);
    }

    void FindPlayer()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
}
