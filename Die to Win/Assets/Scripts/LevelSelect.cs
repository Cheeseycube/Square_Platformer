using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelect : MonoBehaviour
{

    public GameObject levelSelect;
    public GameObject pauseFirstButton;
    public GameObject pauseInvisibleButton;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        levelSelect.SetActive(true);
        if (MyPauseMenu.ControllerPaused)
        {
            // clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            // set a new selected object
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseInvisibleButton);
        }
    }



    public void StartLevel1()
    {
        //FindObjectOfType<GameSession>().NewGame();
        SceneManager.LoadScene(1);
    }

    public void StartLevel2()
    {
        //FindObjectOfType<GameSession>().NewGame();
        SceneManager.LoadScene(2);
    }

    public void StartLevel3()
    {
        //FindObjectOfType<GameSession>().NewGame();
        SceneManager.LoadScene(3);
    }

    public void StartLevel4()
    {
        //FindObjectOfType<GameSession>().NewGame();
        SceneManager.LoadScene(4);
    }

    public void StartLevel5()
    {
        //FindObjectOfType<GameSession>().NewGame();
        SceneManager.LoadScene(5);
    }

    public void StartLevel6()
    {
       // FindObjectOfType<GameSession>().NewGame();
        SceneManager.LoadScene(6);
    }

    public void StartLevel7()
    {
        //FindObjectOfType<GameSession>().NewGame();
        SceneManager.LoadScene(7);
    }

    public void StartLevel8()
    {
       // FindObjectOfType<GameSession>().NewGame();
        SceneManager.LoadScene(8);
    }

    public void StartLevel9()
    {
        //FindObjectOfType<GameSession>().NewGame();
        SceneManager.LoadScene(9);
    }


    public void GoToMainMenu()
    {
        //FindObjectOfType<GameSession>().NewGame();
        SceneManager.LoadScene(0);
    }

}
