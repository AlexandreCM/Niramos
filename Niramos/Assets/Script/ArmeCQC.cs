using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmeCQC : MonoBehaviour
{
    private bool estEnMain = false;
    private bool enAttaque = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision" + collision.gameObject.name);
        if (!estEnMain)
        {
            attacherAuParent(collision);
        }
        else
        {

        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || enAttaque)
        {
            Debug.Log("Fire1");
            attaquer();
            //enAttaque = !enAttaque;
        }
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
            estEnMain = true;
        }
    }
    private void attaquer()
    {
        this.GetComponent<BoxCollider2D>().enabled = !this.GetComponent<BoxCollider2D>().enabled;
        this.enabled = false;
        this.enabled = true;
    }
    private void FaireDegat(Collision2D collision)
    {
        VieJoueur joueur = null;
        if (collision.gameObject.layer == this.gameObject.layer)
        {
            joueur = collision.gameObject.GetComponent<VieJoueur>();
            if (joueur != null)
            {
                Debug.Log("hit");
            }
        }
    }
}
