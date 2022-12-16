using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] TMP_Text totalEnemyCount, groundLength, SpawnedCount;

    public static DebugInfo instance;

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    public void UpdateTotalCount(int i)
    {
        totalEnemyCount.text="Total Enemy: " + i.ToString();
    }

    public void UpdateGroundLentgh(int i)
    {
        groundLength.text = "Ground Length: " + i.ToString();
    }

    public void UpdateSpawnedCount(int i)
    {
        SpawnedCount.text= "Spawned Enemies: " +i.ToString();
    }

}
