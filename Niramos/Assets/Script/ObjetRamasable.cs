using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjetRamasable : MonoBehaviour
{

    private bool estEnMain = false;
    protected Vector3 position;
    protected bool apartienAuJoueur1 = false;
    private bool estEnDrop = false;
    private int delaisDrop = 0;
    private int delaiDropBase = 50;
    private int idServeur;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision" + collision.gameObject.name);
        VieJoueur joueur = null;
        joueur = collision.gameObject.GetComponent<VieJoueur>();
        if (joueur != null && estEnMain != true) attacherAuParent(collision);
    }
    private void attacherAuParent(Collider2D collision)
    {
        if (collision.gameObject.layer == this.gameObject.layer)
        {
            if (collision.gameObject.GetComponent<ManagerJoueur>().objetEnMain == null)
            {
                GameObject Joueur = collision.gameObject;
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                this.GetComponent<PolygonCollider2D>().enabled = false;
                this.transform.parent = Joueur.transform;
                this.GetComponent<Rigidbody2D>().isKinematic = true;
                this.gameObject.transform.localPosition = position;
                this.gameObject.transform.rotation = this.transform.parent.transform.rotation;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                estEnMain = true;

                this.transform.parent.GetComponent<ManagerJoueur>().objetEnMain = this.gameObject;
                if (!this.transform.parent.GetComponent<ManagerJoueur>().getDirectionVerDroite())
                    changerDirection();

                if (collision.gameObject.GetComponent<mouvement>() != null)
                    apartienAuJoueur1 = true;
            }
        }
    }
    public void lancer()
    {
        delaisDrop = delaiDropBase;
        estEnDrop = true;
    }
    protected void Update()
    {
        delaisDrop--;
        if(estEnDrop && delaisDrop < 0)
        {
            estEnDrop = false;
            estEnMain = false;
        }
            
    }
    
    private void setIdServeur(int id)
    {
        idServeur = id;
    }
    private int getIdServeur()
    {
        return idServeur;
    }
    public abstract void changerDirection();
}
