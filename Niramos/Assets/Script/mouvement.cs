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

    void Awake(){
        superAttaque = gameObject.GetComponent<SuperAttaque>();
        auSol = gameObject.GetComponent<IsAuSol>();
    }

    void FixedUpdate() {
        
        Deplacer();
        
        Saut();

        // SuperAttaque
        if(Input.GetAxis("Vertical") < 0 && !auSol) {
            if(!stickDownLast) {
                superAttaque.attaque(GetComponent<Rigidbody2D>());
            }
            stickDownLast = true;
        } 
        else if(auSol) {
            stickDownLast = false; 
        }
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
        if (Input.GetButtonDown("Jump") && auSol) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceSaut), ForceMode2D.Impulse);
            return;
        }
    }

}
