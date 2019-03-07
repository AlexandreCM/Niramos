using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DmgEvent : UnityEvent<VieJoueur>
{
}

public class GestionnaireDegats : MonoBehaviour
{
    private static DmgEvent dmg = new DmgEvent();

    // OnEnable enables the event.
    public static void init()
    {
        dmg.AddListener(damagePlayer);
    }

    public static void remove() {
        dmg.RemoveListener(damagePlayer);
    }
    
    public static DmgEvent getEvent() {
        return dmg;
    }

    private static void damagePlayer(VieJoueur joueur) {
        
        DegatsJoueur test_joueur = joueur.gameObject.GetComponent<DegatsJoueur>();
        if (test_joueur) {
            if (joueur.getIfAlive()) {
                test_joueur.affichageDegats();
            }
            else {
                Debug.Log("INFO    GestionnaireDegats::dmgPlayer: Dead player " + joueur.gameObject.name + " took damage while dead.");
            }
            
        }
        else {
            Debug.LogWarning("WARN    GestionnaireDegats::dmgPlayer: Damage Event called on an invalid Player entity.");
        }
    }
}
