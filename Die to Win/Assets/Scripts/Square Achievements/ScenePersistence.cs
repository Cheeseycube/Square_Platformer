using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersistence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int numSquares = FindObjectsOfType<ScenePersistence>().Length;
        if (numSquares > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
