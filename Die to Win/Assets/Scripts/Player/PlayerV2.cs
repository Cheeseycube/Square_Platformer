using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerV2 : MonoBehaviour
{

    Rigidbody2D rb;
    BoxCollider2D BodyCollider;
    CapsuleCollider2D FeetCollider;

    // floats
    private float runSpeed = 5f;
    private float jumpSpeed = 10f; // was 10
    private float heldjumpSpeed = 5f;
    private float mayJump = 0.1f; // coyote time for jump.  make bigger for more leeway
    private float timer;
    private float deathTimer;

    // ints
    public static int jumpNumv2 = 1;
    int deathCount = 0;

    // booleans
    private bool CanExplode;
    private bool CanSwim;
    private bool isDead = false;
    private bool isjumping;
    private bool isDoubleJumping;


    [SerializeField] ParticleSystem ExplosionParticles;
    [SerializeField] ParticleSystem WaterParticles;

    // Start is called before the first frame update
    void Start()
    {
        PlayerExplosion.SetCollider(false);
        rb = GetComponent<Rigidbody2D>();
        BodyCollider = GetComponent<BoxCollider2D>();
        FeetCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyPauseMenu.isPaused)
        {
            return;
        }
        Run();
        Jump();
        CheckifGrounded();
        CheckifEnteringWater();
    }

    private void Run()
    {
        if (isDead)
        {
            return;
        }
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // value between -1 and +1
        rb.velocity = new Vector2(horizontalInput * runSpeed, rb.velocity.y);
        //rb.AddForce(new Vector2(horizontalInput * runSpeed, rb.velocity.y));
    }

    private void CheckifGrounded()
    {
        if (FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Player.isGrounded = true;
        }
        else
        {
            Player.isGrounded = false;
        }
    }

    private void CheckifEnteringWater()
    {
        if (BodyCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            PlayerSounds.EnteredWater = true;
            //Physics2D.gravity = new Vector2(0, 0); //give player more control underwater

        }
        else
        {
            PlayerSounds.EnteredWater = false;
            //Physics2D.gravity = new Vector2(0, -38);
        }
    }

    private void Jump()
    {
        if (isDead)
        {
            return;
        }

        mayJump -= Time.deltaTime;

        if (FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //ExplosionCollider.enabled = false; // maybe?
            isDoubleJumping = false;
            mayJump = 0.1f;
            if (CanExplode)
            {
                jumpNumv2 = 2;
            }
            else
            {
                jumpNumv2 = 1;
            }
        }

        if (Input.GetButtonDown("Jump") && (mayJump > 0f) && (jumpNumv2 > 0))
        {
            if (CanSwim)
            {
                WaterParticles.Play();
            }
            timer = Time.time;
            isjumping = true;
            isDoubleJumping = true;
            rb.velocity = new Vector2(0f, jumpSpeed);
            //PlayerSounds.mayPlaySound = true;
        }
        else if (Input.GetButtonDown("Jump") && (jumpNumv2 > 0) && CanExplode)
        {
            isjumping = true; // or rather, is double jumping
            isDoubleJumping = true;
            rb.velocity = new Vector2(0f, 16.3f);
            --jumpNumv2;
            //PlayerSounds.mayPlaySound = true;
        }

        if (Input.GetButton("Jump") && isjumping)
        {
            if (Time.time - timer > 0.15) // make 0.15 bigger for longer time needed to hold down jump
            {
                timer = float.PositiveInfinity;
                rb.velocity += new Vector2(0f, heldjumpSpeed);
                isjumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            --jumpNumv2;
            isjumping = false;
        }

    }


}
