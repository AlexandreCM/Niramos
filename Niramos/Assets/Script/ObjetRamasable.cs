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
    private int id = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision" + collision.gameObject.name);
        mouvement joueur = null;
        joueur = collision.gameObject.GetComponent<mouvement>();
        Debug.Log("hit");
        if (joueur != null && estEnMain != true) demanderSiRamassable(collision.gameObject.name);
    }
    private void demanderSiRamassable(string nomJoueur)
    {
        Debug.Log("trigger");
        estEnMain = true;
        GestionnaireItem.declancherEvenement("Ramassable", id, nomJoueur);
    }
    public void attacherAuParent(GameObject objet)
    {
        if (objet.layer == this.gameObject.layer)
        {
            if (objet.GetComponent<ManagerJoueur>().objetEnMain == null)
            {
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                this.GetComponent<PolygonCollider2D>().enabled = false;
                this.transform.parent = objet.transform;
                this.GetComponent<Rigidbody2D>().isKinematic = true;
                this.gameObject.transform.localPosition = position;
                this.gameObject.transform.rotation = this.transform.parent.transform.rotation;
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                estEnMain = true;

                this.transform.parent.GetComponent<ManagerJoueur>().objetEnMain = this.gameObject;
                if (!this.transform.parent.GetComponent<ManagerJoueur>().getDirectionVerDroite())
                    changerDirection();

                if (objet.GetComponent<mouvement>() != null)
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
    public void setEstEnMain(bool enMain)
    {
        if(!enMain && this.transform.parent == null)
            estEnMain = enMain;
        else if(enMain)
            estEnMain = enMain;
    }
    public void setId(int id)
    {
        this.id = id;
    }
    public int getId()
    {
        return id;
    }
    public abstract void changerDirection();
}
