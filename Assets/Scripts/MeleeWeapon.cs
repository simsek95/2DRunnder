using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    
    public WeaponHitBox hitBox;

    private void Awake()
    {
        hitBox = GetComponentInChildren<WeaponHitBox>();
    }
    public void Detach()
    {
       transform.parent.DetachChildren();
       Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
    }


}
