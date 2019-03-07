using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionnaireJeu : MonoBehaviour
{
    private static List<GameObject> respawnPoints = new List<GameObject>();

    // onEnable is called when the object is initialized.
    void OnEnable()
    {
        GestionnaireDegats.init();
        GestionnaireMort.init();
        GestionnaireReapparition.init();
        GestionnaireReapparition.ajouterPointsReapparition(GameObject.Find("Respawn1"),
                                                   GameObject.Find("Respawn2"),
                                                   GameObject.Find("Respawn3"),
                                                   GameObject.Find("Respawn4"));
    }

    // onDisable is called when the object is killed.
    void onDisable()
    {
        GestionnaireMort.remove();
    }

    public static void ajouterPointsReapparition(params GameObject[] entites) {
        foreach (GameObject entity in entites) {
            respawnPoints.Add(entity);
        }
    }

    public static void ecraserPointsReapparition() {
        respawnPoints.Clear();
    }
}
