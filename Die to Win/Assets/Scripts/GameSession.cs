using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    // new gamesession
    //[SerializeField] int playerLives = 3;
    [SerializeField] float LevelLoadDelay = 1f;
    [SerializeField] Text deathsText;
    [SerializeField] Text scoreText;
    [SerializeField] int score = 0;
    [SerializeField] int numDeaths = 0; // may be able to unserialize

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        deathsText.text = numDeaths.ToString();
        scoreText.text = score.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        numDeaths += 1;
        deathsText.text = numDeaths.ToString();
        StartCoroutine(RestartLevel());
        RestartLevel();
    }

    public void NewGame()
    {
        numDeaths = 0;
        deathsText.text = numDeaths.ToString();
        score = 0;
        scoreText.text = score.ToString();

    }


    /*private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }*/

    IEnumerator RestartLevel() // was called TakeLife()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
