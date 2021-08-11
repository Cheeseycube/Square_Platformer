using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9Square3 : MonoBehaviour
{
    [SerializeField] GameObject Playerobj;
    private static BoxCollider2D squareCol;
    private static SpriteRenderer squareSprite;
    ParticleSystem squareParticles;
    private bool mayAdd = GameSession.MayFillSquare3;
    // Start is called before the first frame update
    void Start()
    {
        squareCol = GetComponent<BoxCollider2D>();
        squareSprite = GetComponent<SpriteRenderer>();
        squareCol.enabled = GameSession.SquareCol3;
        squareSprite.enabled = GameSession.SquareSprite3;
        squareParticles = GetComponent<ParticleSystem>();
        if (mayAdd)
        {
            squareParticles.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && mayAdd)
        {
            mayAdd = false;
            Destroy(squareParticles);

            Vector3 Playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Playerobj.transform.position = new Vector2((float)4404.425, Playerpos.y);

            squareCol.enabled = true;
            squareSprite.enabled = true;

            GameSession.SquareCol3 = true;
            GameSession.SquareSprite3 = true;
            GameSession.MayFillSquare3 = false;
            FindObjectOfType<GameSession>().AddToScore(1);
            SquarePause.SquareisPaused = true;

        }
    }
}
