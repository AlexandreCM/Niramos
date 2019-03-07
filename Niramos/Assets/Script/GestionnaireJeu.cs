using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionnaireJeu : MonoBehaviour
{
    // onEnable is called when the object is initialized.
    void OnEnable()
    {
        GestionnaireDegats.init();
        GestionnaireMort.init();
        GestionnaireMort.ajouterPointsReapparition(GameObject.Find("Respawn1"),
                                                   GameObject.Find("Respawn2"),
                                                   GameObject.Find("Respawn3"),
                                                   GameObject.Find("Respawn4"));
    }

    // onDisable is called when the object is killed.
    void onDisable()
    {
        GestionnaireMort.remove();
    }
}
