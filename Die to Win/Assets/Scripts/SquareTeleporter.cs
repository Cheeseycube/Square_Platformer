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
        //print(canTeleport);

        switch (CurrentLevel)
        {
            case 1:
                Level1();
                break;

            case 7:
                Level7();
                break;

            case 9:
                Level9();
                break;

            default:
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            canTeleport = false;
        }
    }

    private void Level1()
    {
        Vector3 Playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if ((Playerpos.x >= -9.7) && (Playerpos.x <= -9.2) && canTeleport && (Playerpos.y <= -2.5)) // make these variables based on level was -9.5 on left
        {
            MovePlayer.transform.position = new Vector2((float)-9.5, Playerpos.y);
        }

        if (Input.GetKeyDown("h")) // fill in block after completing the square. Use a collider to check if square is completed, if not completed enable teleport again after a short delay
        {
            canTeleport = true;
        }
    }

    private void Level7()
    {
        Vector3 Playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if ((Playerpos.x >= 4008.189) && (Playerpos.x <= 4008.367) && canTeleport && (Playerpos.y <= -2.34) && (Playerpos.y >= -2.6))
        {
            MovePlayer.transform.position = new Vector2((float)4008.29, transform.position.y); // may change to playerpos.y
            canTeleport = false;
        }
        if (Input.GetKeyDown("h")) // make sure player is not below the spot when teleporting
        {
            canTeleport = true;
        }

    }

    private void Level9()
    {
        Vector3 Playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if ((Playerpos.x >= 4388.34) && (Playerpos.x <= 4388.772) && canTeleport && (Playerpos.y <= 10.606) && (Playerpos.y >= 8.5))
        {
            MovePlayer.transform.position = new Vector2((float)4388.525, Playerpos.y); // may change to playerpos.y
            //print("teleport");
        }
        if (Input.GetKeyDown("h")) // make sure player is not below the spot when teleporting
        {
            canTeleport = true;
        }
    }

}
