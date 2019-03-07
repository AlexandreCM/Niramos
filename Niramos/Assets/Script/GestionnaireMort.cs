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

    // OnEnable enables the event.
    public static void init()
    {
        death.AddListener(killPlayer);
    }

    public static void remove() {
        death.RemoveListener(killPlayer);
    }
    
    public static DeathEvent getEvent() {
        return death;
    }

    private static void killPlayer(VieJoueur joueur) {
        
        DegatsJoueur test_joueur = joueur.gameObject.GetComponent<DegatsJoueur>();
        if (test_joueur) {
            if (joueur.getIfAlive()) {
                Debug.Log("INFO    Player " + joueur.gameObject.name + " died.");
                test_joueur.tuerJoueur();
            }
            else {
                Debug.LogWarning("WARN    GestionnaireMort:killPlayer(" + joueur.gameObject.name + "): Death event called on a dead player!!!");
            }
            
        }
        else {
            Debug.LogWarning("WARN    GestionnaireMort:killPlayer(" + joueur.gameObject.name + "): Event called on an invalid Player entity.");
        }
    }
}
