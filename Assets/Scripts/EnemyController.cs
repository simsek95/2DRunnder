using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform target;
    [SerializeField] float attackRadius = 3f, attackRatio = 1.5f;
    float timeSinceLastAttack = 0;

    [SerializeField] float walkSpeed = 3f;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsEnemy;

    State state;
    [SerializeField] State initialState = State.chasing;
    AttackScript attackScript;


    Rigidbody2D rb;
    Health health;
    private void Awake()
    {
        attackScript = GetComponent<AttackScript>();
        rb = GetComponent<Rigidbody2D>();
        timeSinceLastAttack = attackRatio;
    }

    private void Start()
    {
        target = FindObjectOfType<PlayerMove>().transform;
        SetState(initialState);

        health= GetComponent<Health>();
        health.OnDie += () => { Die(); };
   
    }

    private void FixedUpdate()
    {

        switch (state)
        {
            case State.chasing:
                if(!EnemyIsBetweenPlayerAndTooClose())

                    if(!PlayerIsInRange()) GoToPlayer();
                    else rb.velocity = Vector3.zero;

                if (CanAttack()&&PlayerIsInRange() ) SetState(State.attacking); 

                break;

            case State.attacking:
                if (CanAttack() && PlayerIsInRange()) SetState(State.attacking);
                else if(!PlayerIsInRange()) SetState(State.chasing);
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

    private bool EnemyIsBetweenPlayerAndTooClose()
    {
        Vector2 rayOrigin =  (Vector2)transform.position + (Vector2)Vector2.up;
        Vector2 rayDirection = (Vector2)target.transform.position - rayOrigin;
        rayDirection.y = 0;

        RaycastHit2D[] hit = Physics2D.RaycastAll(rayOrigin, rayDirection,Mathf.Infinity,whatIsEnemy);

        if (hit != null && hit.Length > 1)
        {
            Debug.DrawLine(hit[1].point, rayOrigin, Color.red);
            if (StopIfTooClose(hit[1].transform))
                return true;
            else return false;

        }
        else return false;

        bool StopIfTooClose(Transform hit)
        {
            float distance = Vector2.Distance(hit.position, transform.position);
            if (distance < 2)
            {
                rb.velocity = Vector2.zero;
                return true;
            }
            else return false;
        }

    }


    private bool PlayerIsInRange()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        return distance < attackRadius;
    }

    private Vector2 FindPlayerDirection()
    {
        Vector2 dir;

        dir = target.position - transform.position;
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
        return i;
    }

    void Flip()
    {

        if (FindPlayerDirection().x < 0.1f)
        {
            //Look Left
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (FindPlayerDirection().x > 0.1f)
        {
            //Look Right
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    void Die()
    {
        DropCash();
        Destroy(this.gameObject);
    }

    private static void DropCash()
    {
        FindObjectOfType<Inventory>().ChangeCash(+1);
    }

    enum State
    {
         chasing, attacking
    }

}
