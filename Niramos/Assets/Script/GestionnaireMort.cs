using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            test_joueur.setSiJoueurVivant(false);
            test_joueur.tuerJoueur(joueur, respawnPoints);
        }
        else {
            Debug.LogWarning("WARN    GestionnaireMort:killPlayer(" + joueur.gameObject.name + "): Event called on an invalid Player entity.");
        }
    }
}
