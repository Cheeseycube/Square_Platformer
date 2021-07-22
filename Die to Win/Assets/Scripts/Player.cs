using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Class references

    // Components
    Rigidbody2D rb;
    BoxCollider2D BodyCollider;
    CapsuleCollider2D FeetCollider;
    private Renderer rend;
    private Color GreenColor = Color.green;
    private Color RedColor = Color.red;
    private Color YellowColor = Color.yellow;
    private Color BlueColor = Color.blue;

    // Serialized Fields
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float heldjumpSpeed = 5f;
    [SerializeField] ParticleSystem ExplosionParticles;
    [SerializeField] ParticleSystem WaterParticles;

    // integers
    int jumpNum = 1;
    int deathCount = 0;
    int CurrentLevel = 0;

    // floats
    float mayJump = 0.1f; // coyote time for jump.  make bigger for more leeway
    float timer;
    float deathTimer;
    float ExplosionTimer;

    // booleans
    public static bool isjumping = false;
    public static bool isDead = false;
    bool PlayerTouchingSpikes = false;
    bool CanExplode = false; // explosing hazard property
    bool CanDie = true;
    bool isDoubleJumping = false;
    bool CanSwim = false;
    public static bool isTouchingMovingPlatform = false;
    public static bool isGrounded = false;
    //bool isNormal = true;

     
    // Start is called before the first frame update
    void Start()
    {
        //ExplosionCollider.enabled = false;
        PlayerExplosion.SetCollider(false);
        rb = GetComponent<Rigidbody2D>();
        BodyCollider = GetComponent<BoxCollider2D>();
        FeetCollider = GetComponent<CapsuleCollider2D>();
        //ExplosionCollider = GetComponent<CircleCollider2D>();
        rend = GetComponent<Renderer>();
        rend.material.color = GreenColor;
    }

    // Update is called once per frame
    void Update()
    {
        
        CurrentLevel = SceneManager.GetActiveScene().buildIndex;

        if (MyPauseMenu.isPaused)
        {
            return;
        }
        if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0) && CanSwim)
        {
            WaterParticles.Play();
        }

        if (rb.velocity.x > 0 && CanSwim)
        {
            WaterParticles.Play();
        }

        if (!CanSwim)
        {
            WaterParticles.Stop();
        }
        
        Run();
        Jump();
        Die();
        ExplosionProperty();
        SwimProperty();
        ResetLevel();
        CheckifGrounded();
    }

    private void CheckifGrounded()
    {
        if (FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    
   /* void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Breakable1") == true)
        {
            BreakBlock.isBreaking = true;
            //Destroy(GameObject.FindWithTag("Breakable"));
        }
    }*/

    // create list or array.  array length will be number of levels create currentLevel integer and update it for each level


    IEnumerator ExecuteAfterTime(float time) // this is for explosion
    {
        yield return new WaitForSeconds(time);

        // code to be executed after delay
        switch (CurrentLevel)
        {
            case 4:
                transform.position = new Vector2((float)1829.82, (float)-3.33);
                break;
            
            case 5:
                transform.position = new Vector2((float)485.0, (float)-2.5);
                break;
           
            default:
                break;
        }

        ++deathCount;
        CanExplode = true;
        CanSwim = false;
    }

    IEnumerator DieToWater(float time)
    {
        yield return new WaitForSeconds(time);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            transform.position = new Vector2((float)-8.11, (float)-2.51);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            transform.position = new Vector2((float)485.0, (float)-2.5);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            transform.position = new Vector2((float)485.0, (float)-2.5);
        }
        ++deathCount;
        CanSwim = true;
        CanExplode = false;
        //ExplosionCollider.enabled = false;
        PlayerExplosion.SetCollider(false);
    }

    IEnumerator DeathSetDelay(float time)
    {
        yield return new WaitForSeconds(time);

        CanDie = true;
    }

    IEnumerator ExplodeDisable(float time)
    {
        yield return new WaitForSeconds(time);

        //ExplosionCollider.enabled = false;
        PlayerExplosion.SetCollider(false);
    }


    private void SwimProperty()
    {
        if (CanSwim)
        {
            runSpeed = 8f;
        }
        else
        {
            runSpeed = 5f;
        }
    }

    private void ExplosionProperty() // should probably adjust this a bit
    {

        if (!CanExplode)
        {
            //ExplosionCollider.enabled = false;
            PlayerExplosion.SetCollider(false);
        }
        else if (Input.GetButtonUp("Jump")) // and is doublejumping?
        {
            //ExplosionCollider.enabled = true;
            PlayerExplosion.SetCollider(true);
            StartCoroutine(ExplodeDisable(0.2f)); // was 0.5f
        }

        if (CanExplode && isDoubleJumping)
        {
            if(Input.GetButtonDown("Jump"))
            {
                ExplosionParticles.Play();
            }
        }

        if (jumpNum == 0)
        {
            isDoubleJumping = false;
        }
    }

    private void Die()
    {
        if((FeetCollider.IsTouchingLayers(LayerMask.GetMask("LeftSpikes")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("LeftSpikes"))) && CanDie)
        {
            CanDie = false;
            isDead = true;
            rend.material.color = RedColor;
            rb.velocity = new Vector2(-50f, rb.velocity.y + 5f); // use force mode impluse here instead perhaps?
            ++deathCount;
            deathTimer = Time.time;
            jumpNum = 1; // fix for double jump bug
            PlayerExplosion.SetCollider(false);
            CanExplode = false;
            CanSwim = false;
        }

        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("RightSpikes")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("RightSpikes"))) && CanDie)
        {
            CanDie = false;
            isDead = true;
            rend.material.color = RedColor;
            rb.velocity = new Vector2(25f, rb.velocity.y + 5f); // use force mode impluse here instead perhaps?
            ++deathCount;
            deathTimer = Time.time;
            jumpNum = 1; // fix for double jump bug
            PlayerExplosion.SetCollider(false);
            CanExplode = false;
            CanSwim = false;
        }

        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("DownSpikes")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("DownSpikes"))) && CanDie)
        {
            CanDie = false;
            isDead = true;
            rend.material.color = RedColor;
            rb.velocity = new Vector2(rb.velocity.x, -25f); // use force mode impluse here instead perhaps?
            ++deathCount;
            deathTimer = Time.time;
            jumpNum = 1; // fix for double jump bug
            PlayerExplosion.SetCollider(false);
            CanExplode = false;
            CanSwim = false;
        }

        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("Spikes")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("Spikes"))) && CanDie)
        {
            
             CanDie = false;
             isDead = true;
             rend.material.color = RedColor;
            //rb.AddForce(new Vector2(rb.velocity.x, 25f), ForceMode2D.Impulse);
            switch (CanSwim)
            {
                case true:
                    if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        rb.velocity += new Vector2(20f, 0f);
                    }
                    else
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
             jumpNum = 1; // fix for double jump bug
             PlayerExplosion.SetCollider(false);
             CanExplode = false;
             CanSwim = false; // could delay this to allow swim momentum to be more useful
        }

        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("Exploder")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("Exploder"))) && CanDie)
        {
            if (!CanExplode)
            {
                CanDie = false;
                isDead = true;
                rend.material.color = RedColor;
                deathTimer = Time.time;
                rb.velocity = new Vector2(0f, 15f);

                StartCoroutine(ExecuteAfterTime(0.5f));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 15f); // make this an outwards force
            }
        }

        if ((FeetCollider.IsTouchingLayers(LayerMask.GetMask("Water")) || BodyCollider.IsTouchingLayers(LayerMask.GetMask("Water"))) && CanDie)
        {
            if (!CanSwim)
            {
                CanDie = false;
                isDead = true;
                rend.material.color = RedColor;
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
                CanDie = true;
            }
            else
            {
                StartCoroutine(DeathSetDelay(0.5f)); // makes it so you don't die infinitely  // may make this the default in the future perhaps?
            }
            
            if (CanExplode)
            {
                rend.material.color = YellowColor;
            }
            else if (CanSwim)
            {
                rend.material.color = BlueColor;
            }
            else
            {
                rend.material.color = GreenColor;
            }

        }

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

    private void Jump()
    {
        if (isDead)
        {
            return;
        }

        mayJump -= Time.deltaTime;
        

        if(FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //ExplosionCollider.enabled = false; // maybe?
            isDoubleJumping = false;
            mayJump = 0.1f;
            if (CanExplode)
            {
                jumpNum = 2;
            }
            else
            {
                jumpNum = 1;
            }
        }

        if (Input.GetButtonDown("Jump") && (mayJump > 0f) && (jumpNum > 0))
        {
            if (CanSwim)
            {
                WaterParticles.Play();
            }
            timer = Time.time;
            isjumping = true;
            isDoubleJumping = true; 
            rb.velocity = new Vector2(0f, jumpSpeed);
        }
        else if (Input.GetButtonDown("Jump") && (jumpNum > 0) && CanExplode)
        {
            isjumping = true; // or rather, is double jumping
            isDoubleJumping = true;
            rb.velocity = new Vector2(0f, 15f);
            --jumpNum;
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
            --jumpNum;
            isjumping = false;
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
