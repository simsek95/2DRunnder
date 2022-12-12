using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{

    [SerializeField] WeaponHitBox hitBox;
    public string attackAnim_Name = "Slash";
    public WeaponType type = WeaponType.melee;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] int damagePerHit = 1;
    public int ammoPerClip  = 5;
    public int ammoInClip = 5;
    public int totalAmmo  = 100;
    public float reloadDuration  = 1;
    [SerializeField] float shootCoolDown = 0.5f;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject muzzleEffectPrefab;
    [SerializeField] LayerMask whatIsDamagable;


    bool isReloading = false;
    bool hasCooldown = false;

    Health targetHP = null;
    Vector2 hitPoint;

    private void Awake()
    {
        hitBox = GetComponentInChildren<WeaponHitBox>();
        ammoInClip = ammoPerClip;
        if (!isGun())
            GetComponentInChildren<WeaponHitBox>().SetDamage(damagePerHit);


    }
    public void Shoot(Animator anim)
    {
        if (isReloading || hasCooldown) return;

        anim.Play(attackAnim_Name);
        StartCoroutine(StartCoolDown());

        if (isGun())
        {
            targetHP = null;
            if (EnemyInSight())
            {
                DamageEnemy();

            }
            PlayMuzzleFlash();
            ammoInClip--;
        }
    }

    private void DamageEnemy()
    {
        if (targetHP == null) Debug.LogError("NO TARGET");
        else 
        { 
            targetHP.TakeDamage(damagePerHit, hitPoint);
        }
    }

    private void Update()
    {
       // Debug.DrawRay(shootPoint.position, shootPoint.right, Color.green);
    }
    private bool isGun()
    {
        return type != WeaponType.melee;
    }

    bool EnemyInSight()
    {
        bool i = false;
        RaycastHit2D target = (Physics2D.Raycast(shootPoint.position, shootPoint.right, Mathf.Infinity));
        if(target)  target.transform.TryGetComponent<Health>(out targetHP);

        if (targetHP)
        {
            Debug.DrawRay(shootPoint.position, shootPoint.right, Color.green,2);

            hitPoint= target.point;
            i = true;
        }
        else Debug.DrawRay(shootPoint.position, shootPoint.right, Color.red,2);
        return i;
    }

    void PlayMuzzleFlash()
    {
        Instantiate(muzzleEffectPrefab, shootPoint.position, Quaternion.identity);
    }

    public bool HasAmmo()
    {
        return ammoInClip > 0;
    }
    public IEnumerator Reload(Animator anim)
    {
        if (isReloading || hasCooldown) yield return null;
        else
        {

        isReloading = true;
        anim.Play("Reload");
        yield return new WaitForSeconds(reloadDuration);
        isReloading = false;

        ammoInClip = Mathf.Min(ammoPerClip,totalAmmo);
        totalAmmo -= ammoInClip;
        }
    }
    IEnumerator StartCoolDown()
    {
        hasCooldown = true;
        yield return new WaitForSeconds(shootCoolDown);
        hasCooldown = false;
    }
    public void EndCoolDown()
    {
        hasCooldown = false;
    }

    public void ActivateHitBox()
    {
        hitBox.ActivateHitBox();
    }
    public void DeactivateHitBox()
    {
        hitBox.DeactivateHitBox();
    }


    public enum WeaponType
    {
        melee,semiAuto,fullAuto
    }


}
