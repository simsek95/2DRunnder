using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Weapon[] startWeapons;
    AttackScript attackScript;
    int currentWeaponIndex = 0;

    private void Start()
    {
        attackScript = GetComponent<AttackScript>();
    }

    public  void SwitchWeapons()
    {
        Weapon oldWeapon = startWeapons[currentWeaponIndex];
        oldWeapon.gameObject.SetActive(false);
        oldWeapon.EndCoolDown();

        currentWeaponIndex++;
        if(currentWeaponIndex > startWeapons.Length-1)
            currentWeaponIndex = 0;
        print(currentWeaponIndex);


        Weapon newWeapon = startWeapons[currentWeaponIndex];
        newWeapon.gameObject.SetActive(true);


        attackScript.SetWeapon(newWeapon);
    }

}
