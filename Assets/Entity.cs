using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    protected bool facingRight = true;
    protected float facingDir = 1;

    [Header("Collision info")]

    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsRound;
    protected bool isGrounded;



    protected virtual void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CollisionChecks();
    }

    protected virtual void CollisionChecks()
    {
        //check xem có chạm vào ground không (whatIsRound)
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsRound);
    }
    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
