using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity{

    bool isAttracking;

    [Header("Move info")]
    [SerializeField] private float moveSpeed;

    [Header("Player dêtction")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        //isPlayerDetected.collider.GetComponent<Player>();


        if (isPlayerDetected)
        {
            if(isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(moveSpeed *1.5f * facingDir, rb.velocity.y);
                Debug.Log("Tao đã nhìn thấy Player ");
            }
            else
            {
                Debug.Log("Attrackkk!" + isPlayerDetected);
                isAttracking = true;
            }
        }

        if (!isGrounded || isWallDetected)
            Flip();

        Movement();

        AnimatorControllers();
    }

    private void Movement()
    {
        if(!isAttracking)
            rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
    }



    protected override void CollisionChecks()
    {
        base.CollisionChecks();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));
    }


    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        

    }
}
