using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="tableSO", menuName ="tableSO")]
public class TableSO : ScriptableObject
{
    public List<KeyValuePair> keyValuePairs;
   
}


[Serializable]
public struct KeyValuePair
{
    public int key;
    public float value;
}
