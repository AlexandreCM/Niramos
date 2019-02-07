using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmeCQC : MonoBehaviour
{   
    [SerializeField]
    LayerMask HitLayers;
    public float degats;
    private bool estEnMain = false;
    private bool enAttaque = false;
    [SerializeField]
    private float quantitierKnockBackx = 200;
    private float quantitierKnockBacky = 200;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision" + collision.gameObject.name);
        VieJoueur joueur = null;
        joueur = collision.gameObject.GetComponent<VieJoueur>();
        if (joueur != null) attacherAuParent(collision);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || enAttaque)
        {
            //Debug.Log("Fire1");
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
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            estEnMain = true;
        }
    }
    private void attaquer()
    {
        Vector3 departRay = new Vector3(this.transform.position.x - 0.2f, this.transform.position.y, this.transform.position.z);
        RaycastHit2D[] toucher = Physics2D.RaycastAll(departRay, new Vector3(90, 0, 0), 0.5f, HitLayers);
        Debug.DrawRay(departRay, new Vector3(90, 0, 0), Color.red, 5);
        foreach (RaycastHit2D hit in toucher)
        {
            Debug.Log(hit.rigidbody.gameObject.name);
            VieJoueur joueur = null;
            joueur = hit.rigidbody.gameObject.GetComponent<VieJoueur>();
            if (joueur != null)
            {
                Debug.Log("hit");
                joueur.faireDegat(degats);
                if (this.gameObject.transform.parent.transform.position.x > hit.rigidbody.gameObject.transform.position.x) quantitierKnockBackx *= -1;
                hit.rigidbody.AddForce(new Vector2(quantitierKnockBackx, quantitierKnockBacky));
            }
        }
        
    }
}
