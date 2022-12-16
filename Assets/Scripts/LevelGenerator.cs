using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance { get; private set; }
    [SerializeField] GameObject groundPiece, exitPiece, wallPiece;


   [SerializeField] TableSO groundPieceTableSO, enemySpawnTimeTableSO;

    int levelBrickAmount = 1;

    List<Transform> groundpieces;
    public static int currentLevel = 0;

    float passedTimeSinceRunStart = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else return;
        groundpieces = new List<Transform>();
    }

    private void Update()
    {
        passedTimeSinceRunStart += Time.deltaTime;
        if (Input.GetMouseButtonDown(1)) SpawnTimeForEnemy();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += GenerateLevel;
        GameManager.OnStartRun += () => passedTimeSinceRunStart = 0;
    }


    public float SpawnTimeForEnemy()
    {
        float spawnTime = 0;
        for (int i = 0; i < enemySpawnTimeTableSO.keyValuePairs.Count; i++)
        {
            if (i == 0) spawnTime = enemySpawnTimeTableSO.keyValuePairs[0].value;
            else
            if (passedTimeSinceRunStart > enemySpawnTimeTableSO.keyValuePairs[i].key)
            {
                spawnTime = enemySpawnTimeTableSO.keyValuePairs[i].value;
            }
        }


        print("passed :"+ passedTimeSinceRunStart + "spawnTImeMax: "+spawnTime);
        return spawnTime;
    }

    public void GenerateLevel(Scene scene, LoadSceneMode mode)
    {
        GenerateLevelGround();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            EnemySpawner.instance.Initialize();
            return;
        }
    }

    void GenerateLevelGround()
    {
        DestroyOldGround();
        GenerateNewCount();

        Transform connectPoint = null;
        for (int i = 0; i < levelBrickAmount; i++)
        {
            Transform newPiece = Instantiate(groundPiece, transform).transform;
            int pieceLengthInUnityUnits = 10;

            if (connectPoint != null)
                newPiece.transform.position = connectPoint.position;

            else newPiece.transform.position = new Vector2(-2*pieceLengthInUnityUnits, -5);

            connectPoint = newPiece.transform.GetChild(0);

            groundpieces.Add(newPiece);
        }
        PlaceExitOnLastGround();
        PlaceWalls();
    }

    private void GenerateNewCount()
    {
        //levelBrickAmount = Random.Range(10, 30);
        levelBrickAmount = Mathf.RoundToInt( groundPieceTableSO.keyValuePairs[currentLevel].value);
        DebugInfo.instance.UpdateGroundLentgh(levelBrickAmount);
    }

    void PlaceExitOnLastGround()
    {
        int x = groundpieces.Count-1;
        Vector2 pos = groundpieces[x].GetChild(1).position;
        Instantiate(exitPiece, pos, Quaternion.identity);

    }

    private void DestroyOldGround()
    {
        foreach (Transform piece in groundpieces)
        {
            Destroy(piece.gameObject);
        }
            groundpieces.Clear();
    }

    void PlaceWalls()
    {
        for (int i = 2; i < levelBrickAmount-1; i++)
        {
            int groundPieceScaleX = Mathf.RoundToInt(groundpieces[i].localScale.x);
            for(int k =0; k < groundPieceScaleX; k+=3)
            {
                if (!yesNo()) continue;
                Vector2 pos =new Vector2( groundpieces[i].position.x+k, groundpieces[i].position.y);
                Instantiate(wallPiece, pos, Quaternion.identity);
            }
        }
    }

    bool yesNo()
    {
        float i = Random.value;
        return (i < 0.2f);
    }



}
