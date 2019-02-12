using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEdge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Detecting the Grid Position of Player
        if (collision.gameObject.name == "TestPlayer")
        {        
            collision.gameObject.transform.position = new Vector3(-1.38f, 0.0f, 0.0f);
        }
 
    }
}
