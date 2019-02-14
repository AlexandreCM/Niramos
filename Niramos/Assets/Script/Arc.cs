using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : ObjetRamasable
{

    [SerializeField]
    private GameObject fleche;
    private bool directionDroite = true;
    private float positionxFleche = 0.3f;

    private void OnEnable()
    {
        position = new Vector3(3.5f, 3.2f, 0);
    }
    public void Update()
    {
        if (Input.GetButtonDown("Fire1")) tirer(true);
    }
    public void tirer(bool tireInterne)
    {
        if (apartienAuJoueur1 || tireInterne == false)
        {
            Vector3 position = new Vector3(this.gameObject.transform.position.x + positionxFleche, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            GameObject lancer = Instantiate(fleche, position, this.gameObject.transform.rotation);
            lancer.GetComponent<Fleche>().lancer(directionDroite);
        }
    }

    override
    public void changerDirection()
    {
        directionDroite = !directionDroite;
        positionxFleche = positionxFleche * -1;
        //Debug.Log(directionDroite);
    }
}
