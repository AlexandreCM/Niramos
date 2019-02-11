using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvement : MonoBehaviour
{
    public float vitesse = 5;

    public int forceSaut = 5;
    public bool auSol;
    public LayerMask terrain;

    public int forceFracasse = -10;
    bool stickDownLast;

    enum Direction {Droite, Gauche};
    Direction etatCourant = Direction.Droite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // au sol ?
        auSol = Physics2D.OverlapArea(
            new Vector2(transform.position.x -0.5f, transform.position.y - 0.5f), 
            new Vector2(transform.position.x, transform.position.y), 
            terrain
        );

        // Mouvement du joueur
        var move = new Vector3(Input.GetAxis("Horizontal"), 0);
        

        if(Input.GetAxis("Horizontal") < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
            if(etatCourant != Direction.Gauche) {
                etatCourant = Direction.Gauche;
                GestionnaireEvenement.declancherEvenement("directionChanger");
            }

            Debug.Log("gauche");
        }
        else if(Input.GetAxis("Horizontal") > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
            if(etatCourant != Direction.Droite) {
                etatCourant = Direction.Droite;
                GestionnaireEvenement.declancherEvenement("directionChanger");
            }

            Debug.Log("droite");
        }
        
        transform.position += move * vitesse * Time.deltaTime;
        

        if (Input.GetButtonDown("Jump") && auSol)  //makes player jump

        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceSaut), ForceMode2D.Impulse);
            return;
        }

        // Attaque vers le bas
        if(Input.GetAxis("Vertical") < 0) {
            if(!stickDownLast) {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceFracasse), ForceMode2D.Impulse);
            }
            stickDownLast = true;
        } 
        else {
            stickDownLast = false; 
        }
    }
}
