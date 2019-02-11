using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VieJoueur : MonoBehaviour
{
    [SerializeField]
    private float vie = 100;
    [SerializeField]
    private float vieMax = 100;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("hit " + collision.gameObject.name);
    }
    public void faireDegat(float quantiter)
    {
        vie -= quantiter;
        GestionnaireEvenement.declancherEvenement("vieChanger");
        if (vie <= 0) {
            vie = 0;
            GestionnaireMort.getEvent().Invoke(this);
        }

        Debug.Log(vie);
        //Debug.Log(this.gameObject.name + " " + vie);
    }
    public void regenererVie(float quantiter)
    {
        vie += quantiter;
        if (vie > 100) vie = 100;
    }
    public float getVie()
    {
        return vie;
    }
}
