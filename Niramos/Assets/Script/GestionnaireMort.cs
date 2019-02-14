﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class DeathEvent : UnityEvent<VieJoueur>
{
}

public class GestionnaireMort : MonoBehaviour
{
    private static DeathEvent death = new DeathEvent();
    private static List<GameObject> respawnPoints = new List<GameObject>();

    // OnEnable enables the event.
    public static void init()
    {
        death.AddListener(killPlayer);
    }

    public static void remove() {
        death.RemoveListener(killPlayer);
        ecraserPointsReapparition();
    }
    
    public static DeathEvent getEvent() {
        return death;
    }

    public static void ajouterPointsReapparition(params GameObject[] entites) {
        foreach (GameObject entity in entites) {
            respawnPoints.Add(entity);
        }
    }

    public static void ecraserPointsReapparition() {
        respawnPoints.Clear();
    }

    private static void killPlayer(VieJoueur joueur) {
        
        Test_Joueur test_joueur = joueur.gameObject.GetComponent<Test_Joueur>();
        if (test_joueur) {
            Debug.Log("INFO    Player " + joueur.gameObject.name + " died.");
            test_joueur.clignoterJoueur();
            // test_joueur.gameObject.transform.position = new Vector3(-1.38f, 0.0f, 0.0f);
            test_joueur.gameObject.transform.position = selectSpawnPoint();
            joueur.regenererVie(joueur.getVieMaximale());
            GestionnaireEvenement.declancherEvenement("vieChanger");
            test_joueur.reinitialiserMouvement();
        }
        else {
            Debug.LogWarning("WARN    GestionnaireMort:killPlayer(" + joueur.gameObject.name + "): Event called on an invalid Player entity.");
        }
    }

    private static Vector3 selectSpawnPoint() {
        if (respawnPoints.Any()) {
            var random = new System.Random();
            int select = random.Next(respawnPoints.Count);
            return respawnPoints[select].transform.position;
        }
        else {
            Debug.LogWarning("WARN    GestionnaireMort:selectSpawnPoint(): No spawn point configured. Respawning at origin (0, 0, 0).");
            return new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
