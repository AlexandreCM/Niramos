using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Fleche : MonoBehaviour
{
    [SerializeField]
    private float forceLancementx = 500;
    [SerializeField]
    private float forceLancementy = 125;
    [SerializeField]
    private LayerMask layerJoueur;
    [SerializeField]
    private float degat = 10;
    [SerializeField]
    private int timerDestruction = 700;

    public void lancer(bool directionDroite)
    {
        if (!directionDroite)
        {
            forceLancementx = -1 * forceLancementx;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceLancementx, forceLancementy));
    }
    private void FixedUpdate()
    {
        timerDestruction -= 1;
        //Debug.Log(timerDestruction);
        if(timerDestruction <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == this.gameObject.layer)
        {
            Debug.Log("collision" + collision.gameObject.name);
            VieJoueur joueur = null;
            joueur = collision.gameObject.GetComponent<VieJoueur>();
            if (joueur != null && collision.gameObject.GetComponent<mouvement>() != null)
            {
                GestionnaireAttaque.declancherEvenement("VieJ1Changer", degat, collision.gameObject.name, 0);
                areterFleche(collision);
            }
            else if(collision.gameObject.GetComponent<ObjetRamasable>() == null)
            {
                areterFleche(collision);
            }
        }
        else
        {
            areterFleche(collision);
        }
    }
    private void areterFleche(Collider2D collision)
    {
        this.transform.parent = collision.gameObject.transform;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
