using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvement : MonoBehaviour
{
    public float vitesse = 5;

    IsAuSol auSol;
    public int forceSaut = 5;

    bool stickDownLast;
    SuperAttaque superAttaque;

    enum Direction {Droite, Gauche};
    Direction etatCourant = Direction.Droite;

    private bool vivant = true;

    void Awake(){
        superAttaque = gameObject.GetComponent<SuperAttaque>();
        auSol = gameObject.GetComponent<IsAuSol>();
    }

    void FixedUpdate() {
        
        if (vivant) { 

            Deplacer();
        
            Saut();

            // SuperAttaque
            if(Input.GetAxis("Vertical") < 0 && !auSol.isAuSol())
            {
                if(!stickDownLast)
                {
                    superAttaque.elan(GetComponent<Rigidbody2D>());
                }
                stickDownLast = true;
            }
            else if(auSol && stickDownLast)
            {
                stickDownLast = false; 
                superAttaque.attaqueLancer();
            }
            else if (Input.GetAxis("Vertical") < 0 && Input.GetButtonDown("Jump"))
            {
                stickDownLast = true; 
            }

        }
    }

    public void setStatut(bool statut) {
        this.vivant = statut;
    }

    private void Deplacer()
    {
        // deplacerGauche ?
        if(Input.GetAxis("Horizontal") < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);

            if(etatCourant != Direction.Gauche) {
                etatCourant = Direction.Gauche;
                GestionnaireEvenement.declancherEvenement("directionChanger");
            }
        }
        // deplacerDroite ?
        else if(Input.GetAxis("Horizontal") > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
            if(etatCourant != Direction.Droite) {
                etatCourant = Direction.Droite;
                GestionnaireEvenement.declancherEvenement("directionChanger");
            }
        }

        // Mouvement du joueur
        var move = new Vector3(Input.GetAxis("Horizontal"), 0);
        transform.position += move * vitesse * Time.deltaTime;
    }

    private void Saut()
    {
        if (Input.GetButtonDown("Jump") && auSol.isAuSol()) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceSaut), ForceMode2D.Impulse);
            return;
        }
    }

}
