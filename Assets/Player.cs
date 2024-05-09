using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float speedMove;
    [SerializeField] private float jumbForce;

    [SerializeField] private bool isMoving;
    private float xInput;
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
        xInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xInput * speedMove, rb.velocity.y);


        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumbForce);
        }

            
        isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving", isMoving);

    }
}   
    