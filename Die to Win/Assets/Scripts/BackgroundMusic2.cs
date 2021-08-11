using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic2 : MonoBehaviour
{
    AudioSource myAudio2;

    private void Awake()
    {
        int numMusic = FindObjectsOfType<BackgroundMusic2>().Length;
        if (numMusic > 1)
        {
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
        myAudio2 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex < 9)
        {
            myAudio2.mute = true;
        }
        else
        {
            myAudio2.mute = false;
        }
    }
}
