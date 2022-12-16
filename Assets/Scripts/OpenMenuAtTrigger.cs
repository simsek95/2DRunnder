using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuAtTrigger : MonoBehaviour
{
    [SerializeField] Menues menu;
    [SerializeField] bool pauseGame = false;
    GameObject menuToOpen = null;

    private void Start()
    {
     
            switch (menu)
            {
                case Menues.StartGameConfirmation:
                    menuToOpen = MenuManager.instance.StartRunConformationMenu;
                    break;

                case Menues.ShopConfirmation:
                    menuToOpen = MenuManager.instance.shopOpenConfirmation;
                    break;

                case Menues.HUD:
                    menuToOpen = MenuManager.instance.HUD;
                    break;

                default: Debug.LogError("NO MENUE SELECTED"); break;
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            MenuManager.instance.OpenMenu(menuToOpen);
        GameManager.TogglePause(pauseGame);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

            MenuManager.instance.OpenMenu(MenuManager.instance.HUD);
            menuToOpen.SetActive(false);

            if(pauseGame)
            GameManager.TogglePause(false);
        
    }

    public enum Menues
    {
        HUD,ShopConfirmation, StartGameConfirmation
    }

}
