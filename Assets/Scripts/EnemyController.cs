using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform player;
    [SerializeField] float attackRadius = 3f, attackRatio = 1.5f;
    float timeSinceLastAttack = 0;

    [SerializeField] float walkSpeed = 3f;
    [SerializeField] LayerMask whatIsGround;

    State state;
    [SerializeField] State initialState = State.chasing;
    AttackScript attackScript;


    Rigidbody2D rb;
    Health health;
    bool isGrounded = false;
    private void Awake()
    {
        attackScript = GetComponent<AttackScript>();
        rb = GetComponent<Rigidbody2D>();
        timeSinceLastAttack = attackRatio;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        SetState(initialState);

        health= GetComponent<Health>();
        health.OnDie += () => { Die(); };
   
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.chasing:
                GoToPlayer();
                if (CanAttack()&&PlayerIsInRange() ) SetState(State.attacking); ;
                break;

            case State.attacking:
                if (CanAttack() && PlayerIsInRange()) SetState(State.attacking); ;
                break;
        }
        Flip();

    }

   
   

    void SetState(State newState)
    {
        state = newState;

        switch (state)
        {
            case State.chasing:
                
                break;

            case State.attacking:
                Attack();
                break;
        }

    }

    private void Attack()
    {

        ResetAttackTimer();
        attackScript.Attack();
    }

    private void ResetAttackTimer()
    {
        timeSinceLastAttack = 0;
    }

    private bool CanAttack()
    {
        timeSinceLastAttack += Time.deltaTime;
        return timeSinceLastAttack > attackRatio;
    }


    private void GoToPlayer()
    {
        Vector2 velocity = FindPlayerDirection() * walkSpeed;

        if (!IsGrounded()) velocity.y = -9.8f;
        rb.velocity = velocity;
    }

    private bool PlayerIsInRange()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        return distance < attackRadius;
    }

    private Vector2 FindPlayerDirection()
    {
        Vector2 dir;

        dir = player.position - transform.position;
        dir.Normalize();

        dir.y = 0;
        return dir;
    }


    bool IsGrounded()
    {
        bool i = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f,whatIsGround);
        if (hit.collider == null)
            i = false;
        isGrounded=i;
        return i;
    }

    void Flip()
    {

        if (rb.velocity.x < 0.1f)
        {
            //Look Left
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (rb.velocity.x > 0.1f)
        {
            //Look Right
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }




    enum State
    {
         chasing, attacking
    }

}
