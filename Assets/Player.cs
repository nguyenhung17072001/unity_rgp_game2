using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speedMove;
    [SerializeField] private float jumbForce;
    
    private float xInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    }
}   
    