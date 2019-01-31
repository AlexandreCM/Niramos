using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VieJoueur : MonoBehaviour
{
    public float vie = 100;
    public float vieMax = 100;

    public void reduireVie(int quantiter)
    {
        vie -= quantiter;
        GestionnaireEvenement.declancherEvenement("vieChanger");
        if (vie <= 0)
        {
            GestionnaireEvenement.declancherEvenement("joueurMort");
        }
        Debug.Log(vie);
    }
}
