using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumbForce;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private float dashCoolDownTimer;

    private float xInput;

    private int facingDir = 1;
    private bool facingRight = true;

    [Header("Collision info")]
    private bool isGrounded;
    [SerializeField] private float groundCheckDistance2;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsRound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        //rb.velocity = new Vector2(5, rb.velocity.y);
        //Debug.Log("Start was called");
    } 

    // Update is called once per frame
    void Update()
    {
        

        Movement();
        CheckInput();
        CollisionChecks();

        //dashTime = dashTime - Time.deltaTime;
        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;
        
        

        AnimatorControllers();
        FlipController();

    }

    private void CollisionChecks()
    {
        //check xem có chạm vào ground không (whatIsRound)
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsRound);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumb();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();

        }

    }
    private void DashAbility()
    {
        if(dashCoolDownTimer < 0)
        {
            dashCoolDownTimer = dashCoolDown;
            dashTime = dashDuration;
        }
    }

    private void Movement()
    {
        if (dashTime > 0)
        {
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }

    }

    private void Jumb()
    {
        if(isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumbForce);

    }


    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isDashing", dashTime > 0);

    }
    private void Flip()
    {
        //facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if(rb.velocity.x > 0 && !facingRight)
            Flip();
        else if(rb.velocity.x<0 && facingRight)
            Flip();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance, 0));
    }
}   
    