using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Animator anim;
    [SerializeField] MeleeWeapon startWeapon;
    MeleeWeapon weapon;
    [SerializeField] string attackAnim_Name = "Slash_Player";


    private void Awake()
    {
        anim = GetComponent<Animator>();
        weapon = startWeapon;
    }
 

    public void Attack()
    {
        anim.Play(attackAnim_Name);
    }

    public void ActivateHitBox()
    {
        weapon.hitBox.ActivateHitBox();
    }
    public void DeactivateHitBox()
    {
        weapon.hitBox.DeactivateHitBox();
    }

}
