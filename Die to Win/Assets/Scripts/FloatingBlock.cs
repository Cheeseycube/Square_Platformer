using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBlock : MonoBehaviour
{
    public static bool BlockinWater = true;
    public static bool BlockEnteringWater = false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
       // rb.gravityScale = 0;
       if (BlockinWater)
        {
            //Physics2D.gravity = new Vector2(0, 0);
            rb.gravityScale = 0;
        } 
       else
        {
            //Physics2D.gravity = new Vector2(0, -38);
            rb.gravityScale = 1;
            //Physics2D.gravity = new Vector2(0, -38);
        }

       if (BlockEnteringWater)
        {
            rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y / 2);
        }
    }
}
