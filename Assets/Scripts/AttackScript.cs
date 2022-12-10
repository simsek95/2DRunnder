using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Animator anim;
    [SerializeField] Weapon startWeapon;
    
    Weapon weapon;
    [SerializeField] TMP_Text currentAmmoText, totalAmmoText;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        weapon = startWeapon;
    }
 

    public void Attack()
    {
        switch (weapon.type)
        {
            case Weapon.WeaponType.melee:
                weapon.Shoot(anim);
                break;

            case Weapon.WeaponType.semiAuto:
                if (weapon.HasAmmo())
                {
                    weapon.Shoot(anim);
                    UpdateAmmoText();
                }
                else
                    StartCoroutine(Reload());
                break;
        }

    }

    IEnumerator Reload()
    {
       yield return StartCoroutine(weapon.Reload(anim));
        UpdateAmmoText();
    }

    void UpdateAmmoText()
    {
        currentAmmoText.text =weapon.ammoInClip.ToString() + " / "+weapon.ammoPerClip.ToString();
        totalAmmoText.text = "MUN: "+ weapon.totalAmmo.ToString();
    }

    public void ActivateHitBox()
    {
        weapon.ActivateHitBox();
    }
    public void DeactivateHitBox()
    {
        weapon.DeactivateHitBox();
    }

    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }

}
