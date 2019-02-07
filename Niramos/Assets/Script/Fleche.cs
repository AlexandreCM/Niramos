using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleche : MonoBehaviour
{
    [SerializeField]
    private float forceLancement = 500;
    [SerializeField]
    private LayerMask layerJoueur;
    [SerializeField]
    private float degat = 10;

    public void lancer(bool directionDroite)
    {
        if (!directionDroite)
        {
            forceLancement *= -1;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceLancement, forceLancement / 4));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision" + collision.gameObject.name);
        if (collision.gameObject.layer == layerJoueur)
        {
            VieJoueur joueur = null;
            joueur = collision.gameObject.GetComponent<VieJoueur>();
            if (joueur != null)
            {
                joueur.faireDegat(degat);
                this.transform.parent = collision.gameObject.transform;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
