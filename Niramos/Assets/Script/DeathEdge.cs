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
        VieJoueur vieJ = collision.gameObject.GetComponent<VieJoueur>();
        if (vieJ)
        {
            if (vieJ.getIfAlive()) {
                vieJ.faireDegat(vieJ.getVieMaximale());
            }        
        }
 
    }
}
