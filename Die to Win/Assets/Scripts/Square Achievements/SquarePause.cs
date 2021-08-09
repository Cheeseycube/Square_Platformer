using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquarePause : MonoBehaviour
{
    [SerializeField] private  GameObject squarePause;
    [SerializeField] private GameObject pauseFirstButton;
    public static bool SquareisPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        squarePause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        squarePause.SetActive(SquareisPaused);
        if (SquareisPaused)
        {
            Time.timeScale = 0f;
            // clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            // set a new selected object
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
    }

   public void ResumeGameSquare()
    {
        Time.timeScale = 0f;
        SquareisPaused = false;
        Time.timeScale = 1f;
    }

    /*public void SetPauseTrue()
    {
        squarePause.SetActive(true);
        Time.timeScale = 0f;
    }*/
}
