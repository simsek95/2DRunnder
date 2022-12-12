using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance { get; private set; }
    [SerializeField] GameObject groundPiece, exitPiece, wallPiece;


   [SerializeField] int enemyCount = 0;

   [SerializeField] TableSO groundPieceTable;
    Dictionary<int, int> table;

    int levelBrickAmount = 1;

    List<Transform> groundpieces;
    public static int currentLevel = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else return;
        groundpieces = new List<Transform>();

        InitializeTable();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += GenerateLevel;
    }

    void InitializeTable()
    {
        table = new Dictionary<int, int>();
        for (int i = 0; i < groundPieceTable.keyValuePairs.Count; i++)
        {
            int key = groundPieceTable.keyValuePairs[i].key;
            int value = groundPieceTable.keyValuePairs[i].value;
            table.Add(key, value);
        }
    }


    public void GenerateLevel(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            EnemySpawner.instance.Initialize(0);
            return;
        }
        GenerateEnemyCount();
        GenerateLevelGround();
    }
    void GenerateEnemyCount()
    {
      int oldEnemyCount = enemyCount;
      enemyCount = Random.Range(Mathf.RoundToInt(oldEnemyCount*1.3f)+1, (oldEnemyCount*2)+3);
        oldEnemyCount = enemyCount;
  
        EnemySpawner.instance.Initialize(enemyCount);
    }

    void GenerateLevelGround()
    {
        DestroyOldGround();
        GenerateNewCount();

        Transform connectPoint = null;
        for (int i = 0; i < levelBrickAmount; i++)
        {
            Transform newPiece = Instantiate(groundPiece, transform).transform;

            if (connectPoint != null)
                newPiece.transform.position = connectPoint.position;

            else newPiece.transform.position = new Vector2(-20, -5);

            connectPoint = newPiece.transform.GetChild(0);

            groundpieces.Add(newPiece);
        }
        PlaceExitOnLastGround();
        PlaceWalls();
    }

    private void GenerateNewCount()
    {
        //levelBrickAmount = Random.Range(10, 30);
        levelBrickAmount = table[currentLevel];
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
        for (int i = 1; i < levelBrickAmount-1; i++)
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

public struct LevelDictionary
{
  Dictionary<int, int> goundLengthStages;

}