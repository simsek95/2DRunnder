using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVisualEffect : MonoBehaviour
{
    [SerializeField] string animationName = "BloodEffect";
    [SerializeField] float lifeTime = 2;
    private void OnEnable()
    {
       if( TryGetComponent<Animator>(out Animator anim))
            anim.Play(animationName);
        Destroy(this.gameObject, lifeTime);
    }
}
