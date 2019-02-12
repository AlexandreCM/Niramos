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
        if (joueur1.getVie() > 0) vie_1.text = joueur1.getVie().ToString();
        else vie_1.text = "X";

        if (joueur2.getVie() > 0) vie_2.text = joueur2.getVie().ToString();
        else vie_2.text = "X";

        if (joueur3.getVie() > 0) vie_3.text = joueur3.getVie().ToString();
        else vie_3.text = "X";

        if (joueur4.getVie() > 0) vie_4.text = joueur4.getVie().ToString();
        else vie_4.text = "X";
    }
}
