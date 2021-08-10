using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    [SerializeField] private GameObject Playerobj;

    public static CircleCollider2D ExplosionCollider; 
    // Start is called before the first frame update
    void Start()
    {
        ExplosionCollider = GetComponent<CircleCollider2D>();
        ExplosionCollider.enabled = false;
        transform.position = Playerobj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetCollider(bool isActive)
    {
        ExplosionCollider.enabled = isActive;
    }

}
