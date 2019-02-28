using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDetectionSuperAttaque : MonoBehaviour
{
    public List<GameObject> listeJoueur;

    public List<GameObject> getListeJoueur()
    {
        return listeJoueur;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        //We know we just hit a player
        //add collision player to a list
        if (collision.gameObject.tag == "Player")
        {
            listeJoueur.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        //We know we just hit a player
        //remove collision player from list
        if (collision.gameObject.tag == "Player")
        {
            listeJoueur.Remove(collision.gameObject);
        }

    }
}
