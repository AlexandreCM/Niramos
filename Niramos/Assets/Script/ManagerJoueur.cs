using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerJoueur : MonoBehaviour
{
    public GameObject objetEnMain = null;
    private bool directionVerDroite = true;

    private void OnEnable()
    {
        if(this.gameObject.GetComponent<mouvement>().enabled)
            GestionnaireEvenement.ajouterEvenement("directionChanger", changerDirection);
    }

    public bool getDirectionVerDroite()
    {
        return directionVerDroite;
    }

    public void setDirectionVerDroite(bool direction)
    {
        if(directionVerDroite != direction)
        {
            directionVerDroite = direction;
            changerDirectionArme();
        }
    }

    private void changerDirection()
    {
        directionVerDroite = !directionVerDroite;
        changerDirectionArme();
    }

    private void changerDirectionArme()
    {
        ArmeCQC arme = null;
        arme = objetEnMain.GetComponent<ArmeCQC>();
        Arc arc = objetEnMain.GetComponent<Arc>();

        if (arc != null) arc.changerDirection();
        if (arme != null) arme.changerDirection();
    }
}
