using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionnaireJeu : MonoBehaviour
{
    // onEnable is called when the object is initialized.
    void OnEnable()
    {
        GestionnaireMort.init();
    }

    // onDisable is called when the object is killed.
    void onDisable()
    {
        GestionnaireMort.remove();
    }
}
