using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            Player.CanExplode = false;
            Player.CanSwim = false;
            GameSession.MayFillSquare = true;
            GameSession.SquareCol = false;
            GameSession.SquareSprite = false;
            StartCoroutine(StartSuccessScene());
        }
    }
    IEnumerator StartSuccessScene()
    {
        yield return new WaitForSecondsRealtime((float)0.5f);
        SceneManager.LoadScene(12);
        Time.timeScale = 1;
    }
}
