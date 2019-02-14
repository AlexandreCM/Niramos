using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Test_Joueur : MonoBehaviour {

    private Rigidbody2D phys; // The RigidBody2D of our GameObject.

    // Use this for initialization
    void Start()
    {
        this.phys = this.gameObject.GetComponent<Rigidbody2D>();
        GestionnaireEvenement.ajouterEvenement("vieChanger", affichageDegats);
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
    }

    public void clignoterJoueur() {
        StartCoroutine(this.flashRed(5, 0.10f));
    }

    public void repousserJoueur(Vector2 force2d, float multiplier) {
        this.phys.AddForce(force2d * multiplier);
    }

    public void affichageDegats() {
        StartCoroutine(this.flashRed(1, 0.10f));
    }

    public void reinitialiserMouvement() {
        this.phys.velocity = Vector2.zero;
        this.phys.angularVelocity = 0.0f;
    }

    private IEnumerator flashRed(int fois, float blink_time) {
        for (int i = 0; i < fois; i++) {
            this.gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(blink_time);
            this.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
            yield return new WaitForSeconds(blink_time);
        }
    }

}