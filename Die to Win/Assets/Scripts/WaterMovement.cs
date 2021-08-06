using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isTouchingWater)
        {
            // nothing for now, was trying to get some jump functionality in water
        }
    }


    private void OnTriggerExit2D(Collider2D col) // have an overlay in the water explaining controls a little bit
    {
        if (col.CompareTag("Player"))
        {
            Player.isTouchingWater = false;
        }

        if (col.CompareTag("FloatingBlock")) // will need to use multiple tags to differentiate between floating blocks
        {
            FloatingBlock.BlockinWater = false;
            FloatingBlock.BlockEnteringWater = false;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player.isTouchingWater = true;
        }

        if (col.CompareTag("FloatingBlock"))
        {
            FloatingBlock.BlockinWater = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("FloatingBlock"))
        {
            FloatingBlock.BlockEnteringWater = true;
        }
    }
}
