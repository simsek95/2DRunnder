using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="tableSO", menuName ="tableSO")]
public class TableSO : ScriptableObject
{
    [SerializeField] Dictionary<int, int> table;
    public List<KeyValuePair> keyValuePairs;
   
}


[Serializable]
public struct KeyValuePair
{
    public int key;
    public int value;
}
