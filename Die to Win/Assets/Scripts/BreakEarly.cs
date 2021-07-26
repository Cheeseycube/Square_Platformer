using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakEarly : MonoBehaviour
{
    [SerializeField] ParticleSystem BreakingParticles;
    BoxCollider2D bc;
    SpriteRenderer mySprite;
    public static bool isBreaking = false;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) // should probably check if it is colliding with player
    {
        Destroy(mySprite);
        Destroy(bc);
        BreakingParticles.Play();
        Destroy(gameObject, (float)0.5); // use SetActive(false) to preserve gameobject
        
    }

}
