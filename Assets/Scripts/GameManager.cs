using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
    public static event Action OnLevelDone;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else return;
    }

    public void LevelDone()
    {
        if (OnLevelDone != null)
            OnLevelDone();
        else Debug.Log("No Listeners to OnLevelDone");
    }

    public void StartRun()
    {
        SceneManager.LoadScene(1);
        MenuManager.instance.CloseAllMenus();
    }

    public void ContinueRun()
    {
        SceneManager.LoadScene(1);
        MenuManager.instance.CloseAllMenus();
    }


    public void GoHome()
    {
        SceneManager.LoadScene(0);
        MenuManager.instance.CloseAllMenus();
    }
}
