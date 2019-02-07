using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximiterJoueur : MonoBehaviour
{
    private List<ProximiterJoueur> listeJoueur = new List<ProximiterJoueur>();
    [SerializeField]
    private float quantiterPousse = 1;
    private int compteur = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProximiterJoueur joueur = null;
        joueur = collision.gameObject.GetComponent<ProximiterJoueur>();
        if (joueur != null)
            listeJoueur.Add(joueur);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ProximiterJoueur joueur = null;
        joueur = collision.gameObject.GetComponent<ProximiterJoueur>();
        if (joueur != null)
            listeJoueur.Remove(joueur);
    }
    private void FixedUpdate()
    {
        compteur++;
        if (compteur == 5)
        {
            compteur = 0;
            foreach (ProximiterJoueur joueur in listeJoueur)
            {
                if (this.gameObject.transform.position.x > joueur.gameObject.transform.position.x) quantiterPousse *= -1;
                joueur.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(quantiterPousse, 0));
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(quantiterPousse * -1, 0));
            }
        }
    }
}
