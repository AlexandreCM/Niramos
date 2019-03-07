using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RespawnEvent : UnityEvent<DegatsJoueur, int>
{
}

public class GestionnaireReapparition : MonoBehaviour
{
    private static RespawnEvent respawn = new RespawnEvent();
    private static List<GameObject> respawnPoints = new List<GameObject>();

    // OnEnable enables the event.
    public static void init()
    {
        respawn.AddListener(respawnPlayer);
    }

    public static void remove() {
        respawn.RemoveListener(respawnPlayer);
        ecraserPointsReapparition();
    }
    
    public static RespawnEvent getEvent() {
        return respawn;
    }

    public static void ajouterPointsReapparition(params GameObject[] entites) {
        foreach (GameObject entity in entites) {
            respawnPoints.Add(entity);
        }
    }

    public static void ecraserPointsReapparition() {
        respawnPoints.Clear();
    }

    private static void respawnPlayer(DegatsJoueur joueur, int number) {

        if (joueur) {
            if (!(joueur.getSiJoueurEstEnVie())) {
                Debug.Log("INFO    Player " + joueur.gameObject.name + " respawns.");
                if(number < respawnPoints.Count) {
                    joueur.reapparaitreJoueur(number);
                }
                else {
                    Debug.LogWarning("WARN    GestionnaireReapparition::respawnPlayer: INVALID RESPAWN INDEX!!!");
                }
                
            }
            else {
                Debug.LogWarning("WARN    GestionnaireReapparition::respawnPlayer: Respawn event called on an alive player!!!");
            }
            
        }
        else {
            Debug.LogWarning("WARN    GestionnaireReapparition::respawnPlayer: Event called on an invalid Player entity.");
        }
    }

    public static List<GameObject> getRespawnPoints() {
        return respawnPoints;
    }
}
