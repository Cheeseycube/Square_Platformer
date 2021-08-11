using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level10Exit : MonoBehaviour
{
    [SerializeField] private GameObject PortalMessage;
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
        if ((col.gameObject.tag == "Player") && GameSession.AllSquaresFound)
        {
            Time.timeScale = 0;
            Player.CanExplode = false;
            Player.CanSwim = false;
            GameSession.MayFillSquare = true;
            GameSession.SquareCol = false;
            GameSession.SquareSprite = false;
            StartCoroutine(StartNextLevel());
        }
        else
        {
            StartCoroutine(EnablePortalMessage());
        }
    }
    IEnumerator StartNextLevel()
    {
        yield return new WaitForSecondsRealtime((float)0.5f);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        Time.timeScale = 1;
    }

    IEnumerator EnablePortalMessage()
    {
        PortalMessage.SetActive(true);
        yield return new WaitForSecondsRealtime((float)2f);
        PortalMessage.SetActive(false);
        
    }
}