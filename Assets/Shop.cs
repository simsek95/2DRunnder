using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
  ShopItem selectedItem;
    [SerializeField] ShopItem firstSelected;
    public static Shop instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        selectedItem= firstSelected;
    }

    public void SelectItem(ShopItem item)
    {
        selectedItem = item;
    }

    public void Buy()
    {
        print("buy");
        selectedItem.Buy();
        Inventory.instance.ChangeCash(-selectedItem.price);
    }
}
