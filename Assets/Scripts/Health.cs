using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHP = 2;
    [SerializeField] ExplodingBodyEffect dieEffect;
    public int hp { get; private set; }
    [SerializeField] GameObject bloodEffect;
    FeelManager feelManager;
    bool isPlayer = false;

    public event UnityAction OnDie;

    private void Awake()
    {
        hp = maxHP;
        isPlayer = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        feelManager = FeelManager.instance;
    }

    public void TakeDamage(int amount, Vector2 hitPoint)
    {
        if (hp < 1) return;

        hp -= amount;

        if (isPlayer) feelManager.BigCameraShake();
        else feelManager.SmallCameraShake();

        if (bloodEffect)
        {
            Instantiate(bloodEffect,hitPoint,Quaternion.identity);
        }
        if (hp < 1) Die();
    }

    void Die()
    {
        StartDieEffect();

        if (OnDie != null)
            OnDie();
        else Debug.LogError("no listener to Ondie");
    }

    private void StartDieEffect()
    {
        ExplodingBodyEffect effect = Instantiate(dieEffect, transform.position, Quaternion.identity).GetComponent<ExplodingBodyEffect>();
        effect.Explode();
    }
}
