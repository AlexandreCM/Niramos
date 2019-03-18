using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{

    [SerializeField]
    private Text vie1;

[SerializeField]
    private Text vie2;
    [SerializeField]
    private Text vie3;
    [SerializeField]
    private Text vie4;

    /// <summary>
    /// Tableau des labels.
    /// </summary>
    private Text[] labelsVie;

    /// <summary>
    /// Tableau des VieJoueur.
    /// </summary>
    private VieJoueur[] joueurs;

    private int nombreJoueurs = 0;

    // Enabled
    private void OnEnable()
    {
        this.joueurs = new VieJoueur[4];
        this.labelsVie = new Text[4];
        this.labelsVie[0] = this.vie1;
        this.labelsVie[1] = this.vie2;
        this.labelsVie[2] = this.vie3;
        this.labelsVie[3] = this.vie4;
        GestionnaireEvenement.ajouterEvenement("vieChanger", vieJoueur);
    }

    // Disabled
    void onDisable() {
        GestionnaireEvenement.retirerEvenement("vieChanger", vieJoueur);
    }

    private void vieJoueur(){
        if(this.nombreJoueurs <= 0) {
            Debug.LogWarning("WARN    " + this.gameObject.name + ":UI_Health::vieJoueur(): Number of player is empty.");
        }
        else {
            for(int i = 0; i < this.nombreJoueurs; i++) {
                if(this.joueurs[i] == null) {
                    // Don't update.
                }
                else if(this.labelsVie[i] == null){
                    Debug.LogWarning("WARN    " + this.gameObject.name + ":UI_Health::vieJoueur(): Missing label for given player.");
                }
                else {
                    this.labelsVie[i].text = this.joueurs[i].getVie().ToString();
                }
            }
        }
    }

    public void ajouterJoueur(VieJoueur v) {
        if(this.nombreJoueurs >= 4) {
            Debug.LogError("ERRR    " + this.gameObject.name + ":UI_Health::ajouterJoueur(" + v.gameObject.name + "): Number of players is above 4!!!");
        }
        else {
            this.joueurs[this.nombreJoueurs] = v;
            this.nombreJoueurs++;
        }
    }

    public void retirerDernierJoueur() {
        if(this.nombreJoueurs <= 0) {
            Debug.LogError("ERRR    " + this.gameObject.name + ":UI_Health::retirerDernierJoueur(): No player defined.");
        }
        else {
            this.joueurs[this.nombreJoueurs] = null;
            this.nombreJoueurs--;
        }
    }
}
