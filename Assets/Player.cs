using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    [Header("Move info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumbForce;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private float dashCoolDownTimer;

    [Header("Attrack info")]
    [SerializeField] private float comboTime = .3f;
    private float comboTimeWindow;
    private bool isAttracking;
    private int comboCounter;
    
        
    


    private float xInput;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Movement();
        CheckInput();

        //dashTime = dashTime - Time.deltaTime;
        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;
        

        AnimatorControllers();
        FlipController();

    }


    public void AttrackOver()
    {
        isAttracking = false;

        comboCounter++;
        //Debug.Log(comboCounter);
        if(comboCounter>2)
        {
            comboCounter = 0;
        }
        
        
    }

    

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttrack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumb();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();

        }

    }

    private void StartAttrack()
    {
        if (!isGrounded)
            return;
        //smoth combo
        if (comboTimeWindow < 0)
            comboCounter = 0;

        isAttracking = true;
        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if(dashCoolDownTimer < 0 && !isAttracking)
        {
            dashCoolDownTimer = dashCoolDown;
            dashTime = dashDuration;
        }
    }
    
    private void Movement()
    {
        if(isAttracking)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            rb.velocity = new Vector2(facingDir * dashSpeed, 0);
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
        anim.SetBool("isAttracking", isAttracking);
        anim.SetInteger("comboCounter", comboCounter);

    }
    

    private void FlipController()
    {
        if(rb.velocity.x > 0 && !facingRight)
            Flip();
        else if(rb.velocity.x<0 && facingRight)
            Flip();
        
    }

    
}   
    