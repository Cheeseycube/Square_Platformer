using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    [SerializeField] float soundVol = 0.25f;

    // Update is called once per frame
   private void Update()
    {
        if (Input.GetButtonDown("Jump") && Player.isGrounded)
        {
            GameObject AudioListener = GameObject.FindWithTag("AudioListener");
            AudioSource.PlayClipAtPoint(coinSound, AudioListener.transform.position, soundVol);
            gameObject.SetActive(false);
        }

        if (Player.isGrounded) // needs to include coyote time
        {
            gameObject.SetActive(true);
        }
    }
}
