using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmeCQC : ObjetRamasable
{   
    [SerializeField]
    LayerMask HitLayers;
    public float degats;
    private bool enAttaque = false;
    [SerializeField]
    private float quantitierKnockBackx = 200;
    private float quantitierKnockBacky = 200;
    private float directionRay = 90;
    private float positionBaseRay = -0.2f;
    private float distanceRay = 0.5f;
    private void OnEnable()
    {
        position = new Vector3(3.7f, 3.2f, 0);
    }

    private void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Fire1") || enAttaque)
        {
            //Debug.Log("Fire1");
            attaquer(true);
            //enAttaque = !enAttaque;
        }
    }
    public void attaquer(bool ataqueInterne)
    {
        if (apartienAuJoueur1 || ataqueInterne == false)
        {
            Vector3 departRay = new Vector3(this.transform.position.x + positionBaseRay, this.transform.position.y, this.transform.position.z);
            //Debug.Log(directionRay);
            RaycastHit2D[] toucher = Physics2D.RaycastAll(departRay, new Vector2(directionRay, 0), distanceRay, HitLayers);
            //Debug.DrawRay(departRay, new Vector3(90, 0, 90), Color.red, 5);
            foreach (RaycastHit2D hit in toucher)
            {
                Debug.Log(hit.rigidbody.gameObject.name);
                VieJoueur joueur = null;
                joueur = hit.rigidbody.gameObject.GetComponent<VieJoueur>();
                if (joueur != null)
                {
                    Debug.Log("hit");
                    joueur.faireDegat(degats);
                    if (this.gameObject.transform.parent.transform.position.x > hit.rigidbody.gameObject.transform.position.x && quantitierKnockBackx > 0)
                        quantitierKnockBackx *= -1;
                    else if (this.gameObject.transform.parent.transform.position.x < hit.rigidbody.gameObject.transform.position.x && quantitierKnockBackx < 0)
                        quantitierKnockBackx *= -1;
                    //hit.rigidbody.AddForce(new Vector2(quantitierKnockBackx, quantitierKnockBacky));
                    GestionnaireAttaque.declancherEvenement("VieJ1Changer", degats, joueur.name, quantitierKnockBackx);
                }
            }
        }
    }

    override
    public void changerDirection()
    {
        //Debug.Log("changer direction epee");
        distanceRay = distanceRay * -1;
        positionBaseRay = positionBaseRay * -1;
    }
}
