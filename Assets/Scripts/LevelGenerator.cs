using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance { get; private set; }
    [SerializeField] GameObject groundPiece, exitPiece, wallPiece;


   public int enemyCount { get; private set; } = 0;
   public int levelBrickAmount { get; private set; } = 1;

    List<Transform> groundpieces;

    private void Awake()
    {
        if (instance == null) instance = this;
        else return;
        groundpieces = new List<Transform>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += GenerateLevel;
    }

    private void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Space)) GenerateLevel();
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
            else newPiece.transform.position = new Vector2(-10, -5);

            connectPoint = newPiece.transform.GetChild(0);

            groundpieces.Add(newPiece);
        }
        PlaceExitOnLastGround();
        PlaceWalls();
    }

    private void GenerateNewCount()
    {
        levelBrickAmount = Random.Range(3, 10);
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
