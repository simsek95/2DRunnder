using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject levelDoneMenu;
    public GameObject StartRunConformationMenu;
    public GameObject shopMenu;
    public GameObject shopOpenConfirmation;
    public GameObject HUD;
    List<GameObject> allMenus;

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

        allMenus = new List<GameObject>();

        if(levelDoneMenu != null)
            allMenus.Add(levelDoneMenu);

        if(StartRunConformationMenu != null)
            allMenus.Add (StartRunConformationMenu);

        if (shopMenu != null)
            allMenus.Add(shopMenu);

        if(shopOpenConfirmation != null)
        {
            allMenus.Add (shopOpenConfirmation);

        }
        if (HUD != null)
        {
            allMenus.Add(HUD);

        }


    }

    public void OpenMenu(GameObject menu)
    {
        foreach (GameObject level in allMenus)
        {
            if (level == menu)
                level.SetActive(true);
            else level.SetActive(false);
        }
    }

    public void CloseAllMenus()
    {
        foreach (GameObject level in allMenus)
        {
          level.SetActive(false);
        }
    }

    void OpenLevelFinishedMenu()
    {
        levelDoneMenu.SetActive(true);
    }
}
