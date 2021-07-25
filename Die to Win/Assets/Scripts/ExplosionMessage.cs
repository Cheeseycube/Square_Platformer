using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMessage : MonoBehaviour
{
    public GameObject ExplosionTip;
    bool MessageDisplayed = false;
    // Start is called before the first frame update
    void Start()
    {
        ExplosionTip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.CanExplode && !MessageDisplayed)
        {
            ExplosionTip.SetActive(true);
            MessageDisplayed = true;
        }

        if (Input.GetButtonDown("Jump") && MessageDisplayed)
        {
            ExplosionTip.SetActive(false);
        }
    }
}
