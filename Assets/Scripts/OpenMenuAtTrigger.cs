using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuAtTrigger : MonoBehaviour
{
    [SerializeField] GameObject menuToOpen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            MenuManager.instance.OpenMenu(menuToOpen);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            MenuManager.instance.OpenMenu(MenuManager.instance.HUD);
            menuToOpen.SetActive(false);
        }
    }
}
