using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    float horizontalMovement;
    Vector2 moveInput;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float jumpForce = 4f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += ResetPosition;
    }

    void ResetPosition(Scene scene, LoadSceneMode mode)
    {
        transform.position = Vector2.zero;
    }
    private void Update()
    {
        
        moveInput.x = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (moveInput != Vector2.zero)
            Flip();
    }

    private void FixedUpdate()
    {

        horizontalMovement = moveInput.x * moveSpeed;
        rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
    }

    void Flip()
    {  
        if (moveInput.x < 0)
        {
            //Look Left
            transform.rotation= new Quaternion(0, 180, 0, 0);
        }
        else if (moveInput.x > 0)
        {
            //Look Right

            transform.rotation = new Quaternion(0, 0, 0, 0);
        } 
    }

    void Jump()
    {
        rb.velocity = Vector2.up*jumpForce;

    }

    void Fall()
    {

    }

    bool IsGrounded()
    {
        bool i = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        if (hit.collider == null)
            i = false;
        return i;
    }
}
