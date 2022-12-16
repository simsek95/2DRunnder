using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeapon : ShopItem
{

    [SerializeField] Weapon weaponToUnlock;
    public override void Buy()
    {
        Inventory.instance.AddWeapon(weaponToUnlock);
    }
}
