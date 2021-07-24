using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]  AudioClip jumpSound;
    [SerializeField] AudioClip ExplosionSound;
    [SerializeField]  float soundVol = 0.1f;

    public static bool mayPlaySound = false;
    private bool mayPlaySoundDouble = true;
    bool SecondSoundPlaying = false;
    public static bool CanPlayExplosion = false;


    // Update is called once per frame
   private void Update()
    {

        if (Input.GetButtonDown("Jump") && mayPlaySound)
        {
            if (Player.jumpNum > 0)
            {
                PlayAudio();
                mayPlaySound = false;
            }
            else if (Player.CanExplode)
            {
                PlayAudio();
                mayPlaySound = false;
            }
        }

        if (CanPlayExplosion)
        {
            PlayExplosionAudio();
            CanPlayExplosion = false;
        }

        
        if (Player.isGrounded) 
        {
            mayPlaySound = true;
            mayPlaySoundDouble = true;
            SecondSoundPlaying = false;
        }
    }


    private void PlayAudio()
    {
        GameObject AudioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(jumpSound, AudioListener.transform.position, soundVol);
        mayPlaySound = false;
    }

    private void PlayExplosionAudio()
    {
        GameObject AudioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(ExplosionSound, AudioListener.transform.position, 0.05f);
        CanPlayExplosion = false;
    }


}
