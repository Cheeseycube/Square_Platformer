using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBarrel : MonoBehaviour
{
    BoxCollider2D BarrelCollider;
    SpriteRenderer mySprite;
    [SerializeField] ParticleSystem ExplosionParticles;
    // Start is called before the first frame update
    void Start()
    {
        BarrelCollider = GetComponent<BoxCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BarrelCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Destroy(mySprite);
            ExplosionParticles.Play();
            Destroy(gameObject, (float)0.5);
        }
    }

    void OnParticleCollision(GameObject col)
    {
        Destroy(mySprite);
        ExplosionParticles.Play();
        Destroy(gameObject, (float)0.5);    // use SetActive(false) to preserve gameobject if needed

    }

    public void CreateParticles()
    {
        ExplosionParticles.Play();
    }

    /*private void OnCollisionEnter(Collision col)
    {
        print("happened");
        if (col.gameObject.name == "Player")
        {
            Destroy(col.collider.gameObject);
            Destroy(gameObject);
        }
    }*/

}
