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
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsRound;


    protected bool isGrounded;
    protected bool isWallDetected;



    protected virtual void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        if (wallCheck == null)
            wallCheck = transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CollisionChecks();
    }

    protected virtual void CollisionChecks()
    {
        //check xem có chạm vào ground không (whatIsRound)
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsRound);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsRound);
    }
    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, 0));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+ wallCheckDistance * facingDir, wallCheck.position.y));
    }
}
