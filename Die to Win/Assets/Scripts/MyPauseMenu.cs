using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MyPauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject pauseFirstButton;
    public static bool isPaused;
    public static bool ControllerPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause")) // replace with getbuttondown at some point.  Also experiment with the layers
        {
            ControllerPaused = false;
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        //print(isPaused);
        if (Input.GetButtonDown("PauseController"))
        {
            ControllerPaused = true;
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGameController();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void PauseGameController()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        // set a new selected object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }


    public void GoToLevelSelect()
    {
        Player.CanExplode = false;
        Player.CanSwim = false;
        Time.timeScale = 1f;  
        pauseMenu.SetActive(false); // maybe
        isPaused = false;
        SceneManager.LoadScene(11);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
