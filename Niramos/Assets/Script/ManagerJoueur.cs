using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerJoueur : MonoBehaviour
{
    public GameObject objetEnMain = null;
    private bool directionVerDroite = true;
    private float forceLancer = 300;
    private string tagRammasable = "rammasable";

    private void OnEnable()
    {
        if (this.gameObject.GetComponent<mouvement>() != null)
            GestionnaireEvenement.ajouterEvenement("directionChanger", changerDirection);
    }
    public void ramasserObjet(int idObjet)
    {
        GameObject[] liseRammasable = GameObject.FindGameObjectsWithTag(tagRammasable);

        foreach(GameObject objet in liseRammasable)
        {
            ObjetRamasable rammasable = objet.GetComponent<ObjetRamasable>();
            if(rammasable != null)
            {
                if(rammasable.getId() == idObjet)
                {
                    rammasable.attacherAuParent(this.gameObject);
                    return;
                }
            }
        }
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

    public void changerDirection()
    {
        directionVerDroite = !directionVerDroite;
        changerDirectionArme();
        if (directionVerDroite)
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        else
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
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
        this.transform.position = Vector3.Lerp(this.transform.position, position, Time.deltaTime * 5);
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
