using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    static AudioSource myAudio;
    //public static bool ToggleAudio = false;
    public static bool muted = false;

    private void Awake()
    {
        int numMusic = FindObjectsOfType<BackgroundMusic>().Length;
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
        myAudio = GetComponent<AudioSource>(); 
    }
    public static void AudioToggle()
    {
        myAudio.mute = !myAudio.mute;
        //AudioListener.pause = !AudioListener.pause;
        //myAudio.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
