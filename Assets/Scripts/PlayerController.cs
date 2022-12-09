using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    AttackScript attackScript;
    private void Awake()
    {
        attackScript = GetComponent<AttackScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attackScript.Attack();
        }
    }
}
