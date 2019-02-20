using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

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

    public void tuerJoueur(VieJoueur vie, List<GameObject> respawnPoints) {
        StartCoroutine(this.playerDeath(vie, respawnPoints));
    }

    private IEnumerator playerDeath(VieJoueur vie, List<GameObject> respawnPoints) {
        mouvement playerMovement = this.gameObject.GetComponent<mouvement>();
        if (playerMovement) {
            playerMovement.setStatut(false);
        }
        else {
            Debug.LogWarning("WARN    Test_Joueur:playerDeath(vie, respawnPoints): Failed to freeze player movement (missing 'movement' component).");
        }
        this.changeSpriteColor(Color.red);
        yield return new WaitForSeconds(2);
        this.gameObject.transform.position = this.selectSpawnPoint(respawnPoints);
        vie.regenererVie(vie.getVieMaximale());
        GestionnaireEvenement.declancherEvenement("vieChanger");
        this.clignoterJoueur();
        this.reinitialiserMouvement();
        if (playerMovement) {
            playerMovement.setStatut(true);
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
        this.gameObject.GetComponent<SpriteRenderer>().material.color = color;
    }

    private Vector3 selectSpawnPoint(List<GameObject> respawnPoints) {
        if (respawnPoints.Any()) {
            var random = new System.Random();
            int select = random.Next(respawnPoints.Count);
            return respawnPoints[select].transform.position;
        }
        else {
            Debug.LogWarning("WARN    Test_Joueur:selectSpawnPoint(): No spawn point configured. Respawning at origin (0, 0, 0).");
            return new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}