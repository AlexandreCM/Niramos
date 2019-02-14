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
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
    }

    public void clignoterJoueur() {
        StartCoroutine(GestionnaireMort.flashPlayer(this));
    }

    public void repousserJoueur(Vector2 force2d, float multiplier) {
        this.phys.AddForce(force2d * multiplier);
    }
}