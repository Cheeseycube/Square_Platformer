using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillSquare : MonoBehaviour
{
   [SerializeField]  GameObject Playerobj;
    public static BoxCollider2D squareCol;
    public static SpriteRenderer squareSprite;
    ParticleSystem squareParticles;

    public static bool CanFill = false;
    // Start is called before the first frame update
    void Start()
    {
        squareCol = GetComponent<BoxCollider2D>();
        squareSprite = GetComponent<SpriteRenderer>();
        squareParticles = GetComponent<ParticleSystem>();
        squareCol.enabled = false;
        squareSprite.enabled = false;
    }

    public static void FillinSquare(bool mayFill)
    {
        CanFill = mayFill;
    }
    // Update is called once per frame
    void Update()
    {
        if (CanFill)
        {
            Playerobj.GetComponent<Rigidbody2D>().velocity = Vector2.up * 20f;
            StartCoroutine(DelayFillingIn(0.5f));
            CanFill = false;
        }
    }

    IEnumerator DelayFillingIn(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        squareCol.enabled = true;
        squareSprite.enabled = true;
        Destroy(squareParticles);
    }
}
