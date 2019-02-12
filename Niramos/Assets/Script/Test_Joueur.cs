using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Test_Joueur : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
    }

    public void clignoterJoueur() {
        StartCoroutine(GestionnaireMort.flashPlayer(this));
    }

}