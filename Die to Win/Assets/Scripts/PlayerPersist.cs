using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersist : MonoBehaviour
{
    [SerializeField] GameObject ExplodeObj;
    [SerializeField] GameObject WaterObj;
    [SerializeField] GameObject ExplodePart;
    // Start is called before the first frame update
    void Start()
    {
        int numPlayers = FindObjectsOfType<Player>().Length;
        if (numPlayers > 1)
        {
            Destroy(ExplodeObj);
            Destroy(WaterObj);
            Destroy(ExplodePart);

            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(ExplodeObj);
            DontDestroyOnLoad(WaterObj);
            DontDestroyOnLoad(ExplodePart);
            DontDestroyOnLoad(gameObject);
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
