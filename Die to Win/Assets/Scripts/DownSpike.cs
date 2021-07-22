using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownSpike : MonoBehaviour
{
    PolygonCollider2D pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.IsTouchingLayers(LayerMask.GetMask("Player")))
        {

        }
    }

    

    // if *col.tag == "Player")
}
