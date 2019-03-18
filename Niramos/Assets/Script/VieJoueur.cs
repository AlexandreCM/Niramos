using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VieJoueur : MonoBehaviour
{
    [SerializeField]
    private float vie = 100;
    [SerializeField]
    private float vieMax = 100;

    [SerializeField]
    private bool isAlive;
    private bool estLocal;

    [SerializeField]
    private GameObject gfxManager;

    private void OnEnable()
    {
        this.setIfAlive(true);
        if(this.gfxManager == null) {
            Debug.LogWarning("WARN    " + this.gameObject.name + ":VieJoueur::OnEnable(): GFXManager GameObject not set; damage effects will not show for this player.");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("hit " + collision.gameObject.name);
    }
    public void faireDegat(float quantiter)
    {
        if (this.getIfAlive()) {
            vie -= quantiter;
            this.playDamageSound();
            if(this.gfxManager) SpriteManager.creerEffetDegat(this.gfxManager, 
            
                                                              new Vector3(this.gameObject.transform.position.x,
                                                                          this.gameObject.transform.position.y + 1,
                                                                          this.gameObject.transform.position.z));
            if (vie <= 0 && this.estLocal) {
                GestionnaireMort.getEvent().Invoke(this);
                Debug.Log(this.gameObject.name + " est mort ; transmission au serveur.");
                GestionnaireEvenement.declancherEvenement("JoueurMort");
            }
        }
        GestionnaireEvenement.declancherEvenement("vieChanger");
            
        //Debug.Log(this.gameObject.name + " " + vie);
    }

    public void regenererVie(float quantiter)
    {
        vie += quantiter;
        if (vie > vieMax) vie = vieMax;
        GestionnaireEvenement.declancherEvenement("vieChanger");
    }

    public void setVieAuMaximum() {
        this.vie = this.vieMax;
    }

    public float getVie()
    {
        return vie;
    }

    public float getVieMaximale() {
        return this.vieMax;
    }

    public bool getIfAlive() {
        return this.isAlive;
    }

    public void setIfAlive(bool isLiving) {
        this.isAlive = isLiving;
    }

    /// <summary>
    /// Joue le son de dégâts de l'objet.
    /// </summary>
    private void playDamageSound() {
        AudioSource dmgSound = AudioScript.getFirstSound(this.gameObject);
        if(dmgSound) {
            dmgSound.Play();
        }
        else {
            Debug.LogWarning("WARN    " + this.gameObject.name + ":VieJoueur::playDamageSound(): Failed to play damage sound.");
        }
    }

    public void setEstLocal(bool statut) {
        this.estLocal = statut;
    }
}
