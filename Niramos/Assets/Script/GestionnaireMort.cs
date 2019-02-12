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
        Debug.Log("Dead. Entity: " + joueur.gameObject.name);
        joueur.clignoterJoueur();
        joueur.gameObject.transform.position = new Vector3(-1.38f, 0.0f, 0.0f);
    }
}
