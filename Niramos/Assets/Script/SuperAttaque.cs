using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttaque : MonoBehaviour
{
    public int forceFracasse = -10;
    public ZoneDetectionSuperAttaque listeJoueurZone;

    public int knockbackHaut = 3;
    public int knockbackDirection = 3;


    // public int delaiBase = 500;
    // public int delai;



    void Awake()
    {
        listeJoueurZone = gameObject.GetComponent<ZoneDetectionSuperAttaque>();
    }

    public void elan(Rigidbody2D joueur)
    {
        joueur.AddForce(new Vector2(0, forceFracasse), ForceMode2D.Impulse);
    }

    public void attaqueLancer()
    {
        foreach (GameObject joueur in listeJoueurZone.getListeJoueur())
        {
            joueur.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockbackDirection, knockbackHaut), ForceMode2D.Impulse);
        }
    }
    
    
    private void FixedUpdate()
    {
        // delai --;
    }
    
}
