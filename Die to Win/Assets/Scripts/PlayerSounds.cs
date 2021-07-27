using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]  AudioClip jumpSound;
    [SerializeField] AudioClip ExplosionSound;
    [SerializeField] AudioClip WaterSound;
    [SerializeField]  float soundVol = 0.1f;

    public static bool mayPlaySound = false;
    private bool mayPlaySoundDouble = true;
    bool SecondSoundPlaying = false;
    public static bool CanPlayExplosion = false;
    private bool CanPlaySplash = false;
    public static bool isTouchingWater = false;


    // Update is called once per frame
   private void Update()
    {

        if (Input.GetButtonDown("Jump") && mayPlaySound)
        {
            if (Player.jumpNum > 0)
            {
                PlayJumpAudio();
                mayPlaySound = false;
            }
            else if (Player.CanExplode)
            {
                PlayJumpAudio();
                mayPlaySound = false;
            }
        }

        if (CanPlayExplosion)
        {
            PlayExplosionAudio();
            CanPlayExplosion = false;
        }

        if (isTouchingWater && CanPlaySplash)
        {
            PlayWaterAudio();
            CanPlaySplash = false;
        }

        if (!isTouchingWater)
        {
            CanPlaySplash = true;
        }
        /*if (CanPlaySplash)
        {
            if (Player.CanDie)
            {
                PlayWaterAudio();
                isTouchingWater = true;
            }
            //CanPlaySplash = false;
        }*/

        
        if (Player.isGrounded) 
        {
            mayPlaySound = true;
            mayPlaySoundDouble = true;
            SecondSoundPlaying = false;
        }
    }


    private void PlayJumpAudio()
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

    public void PlayWaterAudio()
    {
        GameObject AudioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(WaterSound, AudioListener.transform.position, 2f); 
            
    }

}
