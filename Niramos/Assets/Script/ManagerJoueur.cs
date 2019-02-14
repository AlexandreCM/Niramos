using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerJoueur : MonoBehaviour
{
    public GameObject objetEnMain = null;
    private bool directionVerDroite = true;
    private float forceLancer = 300;

    private void OnEnable()
    {
        if (this.gameObject.GetComponent<mouvement>() != null)
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
        if (objetEnMain != null)
        {
            ArmeCQC arme = null;
            arme = objetEnMain.GetComponent<ArmeCQC>();
            Arc arc = objetEnMain.GetComponent<Arc>();

            if (arc != null) arc.changerDirection();
            if (arme != null) arme.changerDirection();
        }
    }
    public void setPosition(Vector3 position)
    {
        this.transform.position = position;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && objetEnMain != null && this.gameObject.GetComponent<mouvement>() != null)
            lancerObjet();
    }
    public void lancerObjet()
    {
        Debug.Log("lancer objet");
        objetEnMain.GetComponent<PolygonCollider2D>().enabled = true;
        objetEnMain.GetComponent<CapsuleCollider2D>().enabled = true;
        Transform parent = this.transform.parent;
        objetEnMain.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        objetEnMain.GetComponent<Rigidbody2D>().isKinematic = false;
        if (!directionVerDroite && forceLancer > 0)
            forceLancer = forceLancer * -1;
        else if (directionVerDroite && forceLancer < 0)
            forceLancer = forceLancer * -1;
        objetEnMain.transform.parent = null;
        objetEnMain.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceLancer, 0));

        objetEnMain.GetComponent<ObjetRamasable>().lancer();

        objetEnMain = null;
    }
    public void Attaquer()
    {
        if (objetEnMain != null && objetEnMain.GetComponent<ArmeCQC>() != null)
            objetEnMain.GetComponent<ArmeCQC>().attaquer(true);
        else
            objetEnMain.GetComponent<Arc>().tirer(true);
    }
}
