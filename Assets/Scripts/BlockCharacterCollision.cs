using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    [SerializeField] Collider2D characterCollider;
    [SerializeField] Collider2D blockerCollider;
    void Start()
    {
        Physics2D.IgnoreCollision(blockerCollider, characterCollider);
        GetComponent<Health>().OnDie += ()=>{ blockerCollider.gameObject.SetActive(false);
        };
        
    }

}
