using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySingleton : MonoBehaviour
{
    public static DontDestroySingleton instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this.gameObject);

       
    }
}
