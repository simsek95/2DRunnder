using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Weapon[] startWeaponsInRun;
    [SerializeField] List<Weapon> ownedWeapons;
    
    AttackScript attackScript;
    int currentWeaponIndex = 0;
    int currentCash = 0;

    public static Inventory instance { get; private set; }
    private void Awake()
    {
        if (instance == null) instance = this;
        else return;

        ownedWeapons = new List<Weapon>();
    }

    private void Start()
    {
        attackScript = FindObjectOfType<PlayerController>().GetComponent<AttackScript>();
    }

    public  void SwitchWeapons()
    {
        Weapon oldWeapon = startWeaponsInRun[currentWeaponIndex];
        oldWeapon.gameObject.SetActive(false);
        oldWeapon.EndCoolDown();

        currentWeaponIndex++;
        if(currentWeaponIndex > startWeaponsInRun.Length-1)
            currentWeaponIndex = 0;
        print(currentWeaponIndex);


        Weapon newWeapon = startWeaponsInRun[currentWeaponIndex];
        newWeapon.gameObject.SetActive(true);


        attackScript.SetWeapon(newWeapon);
    }

    public void ChangeCash(int amount)
    {
        currentCash += amount;
        FindObjectOfType<CashText>().GetComponent<TMP_Text>().text = "Cash: "+ currentCash.ToString();
    }

    public void AddWeapon(Weapon newWeapon)
    {
        ownedWeapons.Add(newWeapon);
    }

}
