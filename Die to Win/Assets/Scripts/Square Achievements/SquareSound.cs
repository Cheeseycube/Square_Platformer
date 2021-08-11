using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSound : MonoBehaviour
{
    [SerializeField] AudioClip squareSound;
    [SerializeField] float soundVol = 0.1f;
    public static bool mayPlaySquareSound;
    // Start is called before the first frame update
    void Start()
    {
        mayPlaySquareSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mayPlaySquareSound)
        {
            PlayJumpAudio();
            mayPlaySquareSound = false;
        }
    }

    private void PlayJumpAudio()
    {
        GameObject AudioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(squareSound, AudioListener.transform.position, soundVol);
        mayPlaySquareSound = false;
    }
}
