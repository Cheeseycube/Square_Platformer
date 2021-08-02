using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SquareTeleporter : MonoBehaviour
{
    Vector3 Squarepos = new Vector3((float)-9.67f, (float)-2.51f, 0);
    [SerializeField] GameObject MovePlayer;
    private bool canTeleport = true;
    private int CurrentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CurrentLevel = SceneManager.GetActiveScene().buildIndex;

        switch (CurrentLevel)
        {
            case 1:
                Level1();
                break;

            case 7:
                Level7();
                break;

            default:
                break;
        }
        
    }

    private void Level1()
    {
        Vector3 Playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if ((Playerpos.x >= -9.5) && (Playerpos.x <= -9.2) && canTeleport && (Playerpos.y <= -2.5)) // make these variables based on level
        {
            MovePlayer.transform.position = new Vector2((float)-9.5, transform.position.y);
            print("dslkjd");
            canTeleport = false;
        }

        if (Input.GetKeyDown("h")) // fill in block after completing the square. Use a collider to check if square is completed, if not completed enable teleport again after a short delay
        {
            canTeleport = true;
        }
    }

    private void Level7()
    {
        Vector3 Playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if ((Playerpos.x >= 4008.189) && (Playerpos.x <= 4008.367) && canTeleport && (Playerpos.y <= -2.34))
        {
            MovePlayer.transform.position = new Vector2((float)4008.29, transform.position.y);
            canTeleport = false;
        }
        if (Input.GetKeyDown("h")) // make sure player is not below the spot when teleporting
        {
            canTeleport = true;
        }

    }

}
