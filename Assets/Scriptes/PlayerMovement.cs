using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpspeed;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float wallJump;
    private float horizontalInput;

    private void Awake()
    {
        //reference to rigid body
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
       
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput*speed, body.velocity.y);
        //flipping 
        if (horizontalInput > 0.01f) {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (wallJump > 0.2f)
        {
            if (Input.GetKey(KeyCode.Space) )
            {
                jump();
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            }

            if (wallCollide() && isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 3.5f;
            }
        }
        wallJump += Time.deltaTime;
        //set Animator
       
        
        
    }
    private void jump()
    {
        if (isGrounded())
        { 
            body.velocity = new Vector2(body.velocity.x, jumpspeed);
           
        }
       else  if (wallCollide() && !isGrounded() )
        {
            wallJump = 0;
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3,6);
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit =Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null;  
    }
    private bool wallCollide()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer) ;
        return raycastHit.collider != null; 
    }
    public bool attackCondition()
    {
        return  isGrounded() && !wallCollide();
    }
}
