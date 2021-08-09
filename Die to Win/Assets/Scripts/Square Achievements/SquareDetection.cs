using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareDetection : MonoBehaviour
{
    private bool MayAdd = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && MayAdd)
        {
            MayAdd = false;
            FindObjectOfType<GameSession>().AddToScore(1);
            FillSquare.FillinSquare(true);
           // Destroy(gameObject); // could disable collider instead if need be
        }
    }

}
