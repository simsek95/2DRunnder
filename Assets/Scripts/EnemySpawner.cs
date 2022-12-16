using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    Transform player;
    [SerializeField] Transform spawnPointL;
    [SerializeField] Transform spawnPointR;
    [SerializeField] GameObject enemyPrefab;

    public static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else return;
    }

    public void Initialize()
    {
        print("initial");
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
        print("spawn");
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Vector2 spawnPoint;
            if (yesNo()) spawnPoint = spawnPointL.position;
            else spawnPoint = spawnPointR.position;

            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            print("enemyspawned");
            yield return new WaitForSeconds(LevelGenerator.instance.SpawnTimeForEnemy());
            StartCoroutine(Spawn());
        }
        else yield return null;
       
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
