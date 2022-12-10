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


    bool isReloading = false;
    bool hasCooldown = false;

    private void Awake()
    {
        hitBox = GetComponentInChildren<WeaponHitBox>();
        ammoInClip = ammoPerClip;
    }
    public void Shoot(Animator anim)
    {
        if (isReloading || hasCooldown) return;

        anim.Play(attackAnim_Name);
        StartCoroutine(StartCoolDown());
        if (type != WeaponType.melee)
            ammoInClip--;
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
