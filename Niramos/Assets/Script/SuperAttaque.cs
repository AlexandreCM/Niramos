using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttaque : MonoBehaviour
{
    public int forceFracasse = 10;
    public int degat = 5;
    private ZoneDetectionSuperAttaque listeJoueurZone;

    public int knockbackHaut = 3;
    public int knockbackDirection = 3;

    public int delaiBase = 500;
    public int delai;



    void Awake()
    {
        listeJoueurZone = gameObject.GetComponent<ZoneDetectionSuperAttaque>();
    }

    public void elan(Rigidbody2D joueur)
    {
        if(delai < 0) joueur.AddForce(new Vector2(0, -forceFracasse), ForceMode2D.Impulse);
    }

    public void attaqueLancer()
    {
        foreach (GameObject joueur in listeJoueurZone.getListeJoueur())
        {

            // Debug.Log(joueur.GetComponent<Rigidbody2D>().name);

            if(joueur.GetComponent<VieJoueur>() != null && delai < 0)
            {
                joueur.GetComponent<VieJoueur>().faireDegat(degat);

                if (this.transform.position.x > joueur.transform.position.x && knockbackDirection > 0)
                {
                    knockbackDirection *= -1;
                }
                else if (this.transform.position.x < joueur.transform.position.x && knockbackDirection < 0)
                {
                    knockbackDirection *= -1;
                }

                joueur.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockbackDirection, knockbackHaut), ForceMode2D.Impulse);
            }

        }
    }
    
    private void FixedUpdate()
    {
        delai --;
    }
    
}
