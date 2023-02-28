using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddRelativeForce(Vector2.up*10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
