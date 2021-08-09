using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FillSquare : MonoBehaviour
{
   [SerializeField]  GameObject Playerobj;
    public static BoxCollider2D squareCol;
    public static SpriteRenderer squareSprite;
    ParticleSystem squareParticles;

    public static bool CanFill = false;
    private int CurrentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        squareParticles = GetComponent<ParticleSystem>();
        if (!GameSession.SquareCol)
        {
            squareParticles.Play();
        }
        squareCol = GetComponent<BoxCollider2D>();
        squareSprite = GetComponent<SpriteRenderer>();
        squareParticles = GetComponent<ParticleSystem>();
        squareCol.enabled = GameSession.SquareCol;
        squareSprite.enabled = GameSession.SquareSprite;

        CurrentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public static void FillinSquare(bool mayFill)
    {
        CanFill = mayFill;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameSession.Level1Filled && CurrentLevel == 1)
        {
            StartCoroutine(DelayFillingIn(0.5f));
            Destroy(squareParticles);
            return;
        }

        if (GameSession.Level3Filled && CurrentLevel == 3)
        {
            StartCoroutine(DelayFillingIn(0.5f));
            Destroy(squareParticles);
            return;
        }

        if (GameSession.Level7Filled && CurrentLevel == 7)
        {
            StartCoroutine(DelayFillingIn(0.5f));
            Destroy(squareParticles);
            return;
        }

        if (GameSession.Level8Filled && CurrentLevel == 8)
        {
            StartCoroutine(DelayFillingIn(0.5f));
            Destroy(squareParticles);
            return;
        }

        if (CanFill && GameSession.MayFillSquare)  // replace with a method call?
        {
            if (CurrentLevel == 7)
            {
                Vector3 Playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
                Playerobj.transform.position = new Vector2(Playerpos.x + 5, Playerpos.y);
            }
            else
            {
                Playerobj.GetComponent<Rigidbody2D>().velocity = Vector2.up * 20f;
            }
            StartCoroutine(DelayFillingIn(0.5f));
            CanFill = false;
            GameSession.MayFillSquare = false;
            GameSession.SquareCol = true;
            GameSession.SquareSprite = true;

            switch (CurrentLevel)
            {
                case 1:
                    GameSession.Level1Filled = true;
                    break;

                case 2:
                    break;

                case 3:
                    GameSession.Level3Filled = true;
                    break;

                case 7:
                    GameSession.Level7Filled = true;
                    break;

                case 8:
                    GameSession.Level8Filled = true;
                    break;

                default:
                    break;
                    
            }
        }
    }

    IEnumerator DelayFillingIn(float time)
    {
        yield return new WaitForSecondsRealtime(time); // might make this not real time so that pausing the game doesn't screw me over // could also use translate to avoid issue of reappearing gaps
        squareCol.enabled = true;
        squareSprite.enabled = true;
        //squareParticles.Stop();
        Destroy(squareParticles);
    }
}
