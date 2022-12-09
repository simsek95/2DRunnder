using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
 
    Collider2D hitBox;
    [SerializeField] int dmg = 1;

    private void Awake() { 
        hitBox = GetComponent<Collider2D>();
        DeactivateHitBox();
    }
    public void ActivateHitBox()
    {
        hitBox.enabled = true;
    }

    public void DeactivateHitBox()
    {
        hitBox.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<MeleeWeapon>(out MeleeWeapon weapon) && weapon.hitBox.enabled==true) 
        {
            FeelManager.instance.SmallCameraShake();
            EnemyController enemy = weapon.transform.GetComponentInParent<EnemyController>();

            //DETACH
            //if (enemy != null && enemy.GetComponent<Health>().hp < 2)
            //    weapon.Detach();

        }

          else  if ( collision.transform.TryGetComponent<Health>(out Health health))
        
            health.TakeDamage(dmg,transform.position);
        }
    }

