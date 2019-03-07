using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class DegatsJoueur : MonoBehaviour {

    private Rigidbody2D phys; // The RigidBody2D of our GameObject.
    private VieJoueur vie;
    private bool estLocal = false;

    // Use this for initialization
    void Start()
    {
        this.phys = this.gameObject.GetComponent<Rigidbody2D>();
        this.vie = this.gameObject.GetComponent<VieJoueur>();
        if (!(this.phys && this.vie)) {
            Debug.LogError("ERRR    DegatsJoueur::Start: DegatsJoueur attached on invalid player entity.");
        }
    }

    public void clignoterJoueur() {
        StartCoroutine(this.flashRed(5, 0.10f));
    }

    public void repousserJoueur(Vector2 force2d, float multiplier) {
        this.reinitialiserMouvement();
        this.phys.AddForce(force2d * multiplier);
    }

    public void affichageDegats() {
        if (this.vie.getIfAlive()) {
            StartCoroutine(this.flashRed(1, 0.10f));
        }
    }

    public void reinitialiserMouvement() {
        this.phys.velocity = Vector2.zero;
        this.phys.angularVelocity = 0.0f;
    }

    public void tuerJoueur() {
        this.vie.setIfAlive(false);
        this.changeSpriteColor(Color.red);

        if (this.estLocal) {
            mouvement playerMovement = this.gameObject.GetComponent<mouvement>();
            if (playerMovement) {
                playerMovement.setStatut(false);
            }
            else {
                Debug.LogWarning("WARN    DegatsJoueur:playerDeath(vie, respawnPoints): Failed to freeze player movement (missing 'mouvement' component).");
            }
        }
        
    }

    public void reapparaitreJoueur(int number) {
        vie.setVieAuMaximum();
        GestionnaireEvenement.declancherEvenement("vieChanger");
        this.clignoterJoueur();
        if (this.estLocal) {
            mouvement playerMovement = this.gameObject.GetComponent<mouvement>();
            this.gameObject.transform.position = this.selectSpawnPoint(number);
            this.reinitialiserMouvement();
            if (playerMovement) {
                playerMovement.setStatut(true);
            }
            else {
                Debug.LogWarning("WARN    DegatsJoueur::respawnPlayer: Failed to resume player movement (missing 'mouvement' component).");
            }
            this.vie.setIfAlive(true);
        }
        
    }

    private void afficherSpriteMort() {
        this.changeSpriteColor(Color.red);
    }

    private void reinitialiserSprite() {
        this.changeSpriteColor(Color.white);
    }

    private IEnumerator flashRed(int fois, float blink_time) {
        for (int i = 0; i < fois; i++) {
            this.changeSpriteColor(Color.red);
            yield return new WaitForSeconds(blink_time);
            this.changeSpriteColor(Color.white);
            yield return new WaitForSeconds(blink_time);
        }
    }

    private void changeSpriteColor(Color color) {
        SpriteRenderer[] sprites = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sprite in sprites) {
            sprite.material.color = color;
        }
    }

    private Vector3 selectSpawnPoint(int number) {
        // var random = new System.Random();
        // int select = random.Next(GestionnaireReapparition.respawnPoints.Count);
        return GestionnaireReapparition.getRespawnPoints()[number].transform.position;
    }

    public bool getSiJoueurLocal() {
        return this.estLocal;
    }

    public void setSiJoueurLocal(bool statut) {
        this.estLocal = statut;
    }

    public bool getSiJoueurEstEnVie() {
        return this.vie.getIfAlive();
    }
}