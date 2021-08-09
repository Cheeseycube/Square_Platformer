using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9Square1 : MonoBehaviour
{
    [SerializeField] GameObject Playerobj;
    ParticleSystem squareParticles;

    private bool mayAdd = GameSession.MayFillSquare1;
    // Start is called before the first frame update
    void Start()
    {
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("FloatingBlock") && mayAdd)
        {
            mayAdd = false;
            squareParticles.Stop();
            GameSession.MayFillSquare1 = false;
            FindObjectOfType<GameSession>().AddToScore(1);
        }
    }
}
