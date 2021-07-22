using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    [SerializeField] ParticleSystem BreakingParticles;
    BoxCollider2D bc;
    SpriteRenderer mySprite;
    [SerializeField] Sprite newSprite;
    public static bool isBreaking = false;
    bool canBreak = false;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(canBreak);
    }

    void OnTriggerEnter2D(Collider2D col) // should probably check if it is colliding with player
    {
        mySprite.sprite = newSprite;
        if (canBreak)
        {
            Destroy(mySprite);
            Destroy(bc);
            BreakingParticles.Play();
            Destroy(gameObject, (float)0.5); // use SetActive(false) to preserve gameobject
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        canBreak = true;
    }

    /*IEnumerator ExecuteAfterTime(float time)
    {

    }*/

    /*void OnParticleCollision(GameObject col)
    {
        //Destroy(mySprite);
        //Destroy(bc);
        //BreakingParticles.Play();
        //Destroy(gameObject, (float)0.5);    // use SetActive(false) to preserve gameobject if needed
        mySprite.sprite = newSprite;
        Breaktime = Time.time;

        if (canBreak)
        {
            Destroy(mySprite);
            Destroy(bc);
            BreakingParticles.Play();
            Destroy(gameObject, (float)0.5);    // use SetActive(false) to preserve gameobject if needed
        }

    }*/

}
