using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{

    [SerializeField]
    private Text vie_1;
    [SerializeField]
    private Text vie_2;
    [SerializeField]
    private Text vie_3;
    [SerializeField]
    private Text vie_4;

    [SerializeField]
    private VieJoueur joueur1;
    [SerializeField]
    private VieJoueur joueur2;
    [SerializeField]
    private VieJoueur joueur3;
    [SerializeField]
    private VieJoueur joueur4;
    // Enabled
    private void OnEnable()
    {
        GestionnaireEvenement.ajouterEvenement("vieChanger", vieJoueur);
    }

    // Disabled
    void onDisable() {
        GestionnaireEvenement.retirerEvenement("vieChanger", vieJoueur);
    }

    private void vieJoueur(){

        if (this.joueur1) {
            if (this.joueur1.getVie() > 0) this.vie_1.text = this.joueur1.getVie().ToString();
            else this.vie_1.text = "X";
        }
        
        if (this.joueur2) {
            if (this.joueur2.getVie() > 0) this.vie_2.text = this.joueur2.getVie().ToString();
            else this.vie_2.text = "X";
        }

        if (this.joueur3) {
            if (this.joueur3.getVie() > 0) this.vie_3.text = this.joueur3.getVie().ToString();
            else this.vie_3.text = "X";
        }

        if (this.joueur4) {
            if (this.joueur4.getVie() > 0) this.vie_4.text = this.joueur4.getVie().ToString();
            else this.vie_4.text = "X";
        }

    }
}
