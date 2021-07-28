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
        
    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            print("player touched water");
        }
    }*/

    private void OnTriggerExit2D(Collider2D col) // could use composite collider instead, need to test
    {
        if (col.CompareTag("Player"))
        {
            Player.isTouchingWater = false;
        }

        if (col.CompareTag("FloatingBlock"))
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
