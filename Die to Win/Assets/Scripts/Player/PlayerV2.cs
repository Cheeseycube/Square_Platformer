using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerV2 : MonoBehaviour
{

    Rigidbody2D rb;
    BoxCollider2D BodyCollider;
    CapsuleCollider2D FeetCollider;
    SpriteRenderer mySprite;
    [SerializeField] Sprite DeathSprite;
    [SerializeField] Sprite NormalSprite;
    [SerializeField] Sprite ExplosionSprite;
    [SerializeField] Sprite WaterSprite;
    [SerializeField] Sprite CombinedSprite;

    // floats
    private float runSpeed = 5f;
    private float jumpSpeed = 13f; // was 10
    private float heldjumpSpeed = 4f;
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
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyPauseMenu.isPaused)
        {
            return;
        }
        if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0) && CanSwim)
        {
            WaterParticles.Play();
        }
        if (((rb.velocity.x > 0) || (Mathf.Abs(rb.velocity.y) > 0)) && CanSwim)
        {
            WaterParticles.Play();
        }

        if (!CanSwim)
        {
            WaterParticles.Stop();
        }
        ResetLevel();
        Run();
        Jump();
        CheckifGrounded();
        CheckifEnteringWater();
        Die();
        ExplosionProperty();
        SwimProperty();
        CombineProperties();
        Swimming();
    }

    private void Run()
    {
        if (isDead)
        {
            return;
        }
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // value between -1 and +1
        rb.velocity = new Vector2(horizontalInput * runSpeed, rb.velocity.y);
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

    private void Die()
    {
        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("LeftSpikes")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("LeftSpikes"))) && Player.CanDie)
        {
            Player.CanDie = false;
            isDead = true;
            mySprite.sprite = DeathSprite;
            rb.velocity = new Vector2(-50f, rb.velocity.y + 5f); // use force mode impluse here instead perhaps?
            ++deathCount;
            deathTimer = Time.time;
            jumpNumv2 = 1; // fix for double jump bug
            PlayerExplosion.SetCollider(false);
            CanExplode = false;
            CanSwim = false;
        }

        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("RightSpikes")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("RightSpikes"))) && Player.CanDie)
        {
            Player.CanDie = false;
            isDead = true;
            mySprite.sprite = DeathSprite;
            rb.velocity = new Vector2(25f, rb.velocity.y + 5f);
            ++deathCount;
            deathTimer = Time.time;
            jumpNumv2 = 1; // fix for double jump bug
            PlayerExplosion.SetCollider(false);
            CanExplode = false;
            CanSwim = false;
        }

        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("DownSpikes")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("DownSpikes"))) && Player.CanDie)
        {
            Player.CanDie = false;
            isDead = true;
            mySprite.sprite = DeathSprite;
            rb.velocity = new Vector2(rb.velocity.x, -25f); // use force mode impluse here instead perhaps?
            ++deathCount;
            deathTimer = Time.time;
            jumpNumv2 = 1; // fix for double jump bug
            PlayerExplosion.SetCollider(false);
            CanExplode = false;
            CanSwim = false;
        }

        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("Spikes")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("Spikes"))) && Player.CanDie)
        {

            Player.CanDie = false;
            isDead = true;
            mySprite.sprite = DeathSprite;
            //rb.AddForce(new Vector2(rb.velocity.x, 25f), ForceMode2D.Impulse);
            switch (CanSwim)
            {
                case true:
                    if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        rb.velocity += new Vector2(20f, 0f);
                    }
                    else if (Input.GetAxisRaw("Horizontal") < 0)
                    {
                        rb.velocity += new Vector2(-20f, 0f);
                    }
                    break;
                default:
                    break;
            }
            rb.velocity = new Vector2(rb.velocity.x, 25f); // use force mode impluse here instead perhaps?
            ++deathCount;
            deathTimer = Time.time;
            jumpNumv2 = 1; // fix for double jump bug
            PlayerExplosion.SetCollider(false);
            CanExplode = false;
            CanSwim = false; // could delay this to allow swim momentum to be more useful
        }

        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("Exploder")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("Exploder"))) && Player.CanDie)
        {
            if (!CanExplode)
            {
                Player.CanDie = false;
                isDead = true;
                mySprite.sprite = DeathSprite;
                deathTimer = Time.time;
                rb.velocity = new Vector2(0f, 15f);
                StartCoroutine(DieToExplosion(0.5f));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 15f); // make this an outwards force
            }
        }

        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("Water")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("Water"))) && Player.CanDie)
        {
            if (!CanSwim)
            {
                Player.CanDie = false;
                isDead = true;
                mySprite.sprite = DeathSprite;
                deathTimer = Time.time;


                StartCoroutine(DieToWater(0.5f));
            }
        }

        if ((Time.time - deathTimer > 0.6) && isDead) // was 0.5
        {
            deathTimer = float.PositiveInfinity;
            isDead = false;
            if ((!FeetCollider.IsTouchingLayers(LayerMask.GetMask("Spikes")) && !BodyCollider.IsTouchingLayers(LayerMask.GetMask("Spikes"))))
            {
                Player.CanDie = true;
            }
            else
            {
                //StartCoroutine(DeathSetDelay(0.5f)); // makes it so you don't die infinitely  // may make this the default in the future perhaps?
            }

            if (CanExplode) // add a check if CanExplode and CanSwim
            {
                //rend.material.color = YellowColor;
            }
            else if (CanSwim)
            {
                //rend.material.color = BlueColor;
                //WaterParticles.Play();
            }
            else
            {
                mySprite.sprite = NormalSprite;
            }

        }

    }

    IEnumerator DieToExplosion(float time)
    {
        yield return new WaitForSeconds(time);
        CanExplode = true;
        mySprite.sprite = ExplosionSprite;

    }

    private void ExplosionProperty() // should probably adjust this a bit
    {

        if (!CanExplode)
        {
            PlayerExplosion.SetCollider(false);
        }
        else if (Input.GetButtonDown("Jump")) // was get button Up
        {
            PlayerExplosion.SetCollider(true);
            StartCoroutine(ExplodeDisable(0.2f)); // was 0.5f
        }

        if (CanExplode && isDoubleJumping)
        {
            //PlayerSounds.mayPlaySound = true;
            if (Input.GetButtonDown("Jump"))
            {
                ExplosionParticles.Play();
                PlayerSounds.mayPlaySound = true;
            }
        }

        if (jumpNumv2 == 0)
        {
            isDoubleJumping = false;
        }
    }

    IEnumerator DieToWater(float time)
    {
        yield return new WaitForSeconds(time);
        CanSwim = true;
        mySprite.sprite = WaterSprite;
    }
    private void SwimProperty()
    {
        if (CanSwim)
        {
            runSpeed = 10f; // was 8f or 10f;
        }
        else
        {
            runSpeed = 5f;
        }
    }

    IEnumerator ExplodeDisable(float time)
    {
        yield return new WaitForSeconds(time);

        PlayerExplosion.SetCollider(false);
    }

    private void CombineProperties()
    {
        if (CanSwim && CanExplode)
        {
            mySprite.sprite = CombinedSprite;
        }
    }

    private void Swimming()
    {
        if (CanSwim && Player.isTouchingWater)
        {
            rb.gravityScale = 0;
            float verticalInput = Input.GetAxisRaw("Vertical"); // value between -1 and +1
            if (Input.GetButton("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, 10f);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalInput * runSpeed); // THIS IS WHY PLAYER CANNOT JUMP IN WATER
            }

        }
        else
        {
            rb.gravityScale = 1;
        }
    }





    private void ResetLevel()
    {
        if (Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
