using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTeleporter : MonoBehaviour
{
    Vector3 Squarepos = new Vector3((float)-9.67f, (float)-2.51f, 0);
    [SerializeField] GameObject MovePlayer;
    private bool canTeleport = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if ((Playerpos.x >= -9.5) && (Playerpos.x <= -9.2) && canTeleport && (Playerpos.y <= -2.5)) // make these variables based on level
        {
            print("yay!");
            MovePlayer.transform.position = new Vector2((float)-9.494989, (float)-3.500385);
            canTeleport = false;
        }

        if (Input.GetKeyDown("h")) // fill in block after completing the square
        {
            canTeleport = true;
        }
        
    }
}
