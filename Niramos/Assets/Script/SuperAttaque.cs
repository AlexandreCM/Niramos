using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttaque : MonoBehaviour
{
    public int forceFracasse = -10;

    // public int delaiBase = 500;
    // public int delai;

    

    void Awake()
    {
        
    }

    public void elan(Rigidbody2D joueur)
    {
        joueur.AddForce(new Vector2(0, forceFracasse), ForceMode2D.Impulse);
    }

    private bool proche(Rigidbody2D joueur)
    {
        joueur.GetComponentInParent<GameObject>();
        return false;
    }

    

    public void attaqueLancer(Rigidbody2D joueur)
    {
        Debug.Log("test");
        
        /*if (proche())
        {
            degat();
        }*/
    }
    
    
    private void FixedUpdate()
    {
        // delai --;
    }
    
}
