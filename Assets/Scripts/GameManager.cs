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
    public static event Action OnPaused;
    public static event Action OnStartRun;
    public static bool isPaused = false;
    

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
        StopTime();
    }
    #region START-CONTINUE-END-RUN
    public void StartRun()
    {
        LevelGenerator.currentLevel = 0;
        SceneManager.LoadScene(1);
        MenuManager.instance.CloseAllMenus();
        MenuManager.instance.OpenMenu(MenuManager.instance.HUD);
        StartTime();
        OnStartRun();
    }

    public void ContinueRun()
    {
        LevelGenerator.currentLevel++;
        SceneManager.LoadScene(1);
        MenuManager.instance.CloseAllMenus();
        MenuManager.instance.OpenMenu(MenuManager.instance.HUD);
        StartTime();
    }


    public void GoHome()
    {
        SceneManager.LoadScene(0);
        MenuManager.instance.CloseAllMenus();
        MenuManager.instance.OpenMenu(MenuManager.instance.HUD);
        StartTime();
    }
    #endregion
    #region PAUSE and TIME
    public static void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            if(OnPaused!=null)
            OnPaused();

            StopTime();
        } 
        else if(!isPaused) StartTime();
    }

    public static void TogglePause(bool b)
    {
        isPaused = b;
        if (isPaused)
        {
            if (OnPaused != null)
                OnPaused();

            StopTime();
        }
        else if (!isPaused) StartTime();
    }
    static void StopTime()
    {
        Time.timeScale = 0;
    }

   static void StartTime()
    {
        Time.timeScale = 1;
    }
    #endregion
}
