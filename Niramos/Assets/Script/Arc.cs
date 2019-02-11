using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    private bool estEnMain = false;
    [SerializeField]
    private GameObject fleche;
    private bool DirectionDroite = true;
    private void OnEnable()
    {
        GestionnaireEvenement.ajouterEvenement("changerDirection", changerDirection);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision" + collision.gameObject.name);
        VieJoueur joueur = null;
        joueur = collision.gameObject.GetComponent<VieJoueur>();
        if(joueur != null && estEnMain != true) attacherAuParent(collision);
    }
    public void Update()
    {
        if (Input.GetButtonDown("Fire1")) tirer();
    }
    private void attacherAuParent(Collider2D collision)
    {
        if (collision.gameObject.layer == this.gameObject.layer)
        {
            GameObject Joueur = collision.gameObject;
            this.GetComponents<CapsuleCollider2D>()[0].enabled = false;
            this.GetComponents<CapsuleCollider2D>()[1].enabled = false;
            this.transform.parent = Joueur.transform;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            Vector3 position = new Vector3(3.5f, 3.2f, 0);
            this.gameObject.transform.localPosition = position;
            this.gameObject.transform.rotation = this.transform.parent.transform.rotation;
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            estEnMain = true;
        }
    }
    private void tirer()
    {
        Vector3 position = new Vector3(this.gameObject.transform.position.x + 0.3f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        GameObject lancer = Instantiate(fleche, position, this.gameObject.transform.rotation);
        lancer.GetComponent<Fleche>().lancer(DirectionDroite);
    }
    private void changerDirection()
    {
        DirectionDroite = false;
        Debug.Log(DirectionDroite);
    }
}
