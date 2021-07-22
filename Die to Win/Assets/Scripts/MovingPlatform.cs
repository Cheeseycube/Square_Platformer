using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] float platformSpeed = 1f;
    [SerializeField] float waitTime = 5.0f;

    float timer = 0.0f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            rb.velocity = new Vector2(platformSpeed, 0f);
            timer += Time.deltaTime;  // could maybe replace with = Time.time
            if (timer > waitTime)
            {
                FlipDirection();
                timer = timer - waitTime;
            }
        }
        else
        {
            rb.velocity = new Vector2(-platformSpeed, 0f);
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                FlipDirection();
                timer = timer - waitTime;
            }
        }
    }

    public bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void FlipDirection()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x) * 1.980238f), 1f); // to make this not wonky, use Aseprite to make a wide tile
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //kldsjfklsdjfslk
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //dkljfdslkj
        }
    }

}
