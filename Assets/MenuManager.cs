using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject levelDoneMenu;
    public GameObject StartRunConformationMenu;
    public GameObject shopMenu;
    List<GameObject> allLevels;

    public static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else return;
    }
    void Start()
    {
        GameManager.OnLevelDone += OpenLevelFinishedMenu;

        allLevels = new List<GameObject>();

        if(levelDoneMenu != null)
            allLevels.Add(levelDoneMenu);

        if(StartRunConformationMenu != null)
            allLevels.Add (StartRunConformationMenu);

        if (shopMenu != null)
            allLevels.Add(shopMenu);


    }

    public void OpenMenu(GameObject menu)
    {
        foreach (GameObject level in allLevels)
        {
            if (level == menu)
                level.SetActive(true);
            else level.SetActive(false);
        }
    }

    public void CloseAllMenus()
    {
        foreach (GameObject level in allLevels)
        {
          level.SetActive(false);
        }
    }

    void OpenLevelFinishedMenu()
    {
        levelDoneMenu.SetActive(true);
    }
}
