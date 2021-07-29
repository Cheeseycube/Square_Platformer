using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watertip : MonoBehaviour
{
    public GameObject WaterTip;
    private bool MessageDisplayed = false;
    // Start is called before the first frame update
    void Start()
    {
        WaterTip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.CanSwim && !MessageDisplayed)
        {
            WaterTip.SetActive(true);
            MessageDisplayed = true;
        }

        if (Input.GetButtonDown("Jump") && MessageDisplayed)
        {
            WaterTip.SetActive(false);
        }
    }
}
