using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   La classe AudioScript stocke la liste des AudioSource attachés à une entité (GameObject).
   Il est possible de récupérer les données de ce dictionnaire pour jouer un son.
   Les sons sont stockés dans le même ordre qu'ils sont organisés dans l'inspecteur.
*/

public class AudioScript : MonoBehaviour
{

    private AudioSource[] listePistes;

    // Start is called before the first frame update
    void Start()
    {
        this.listePistes = this.gameObject.GetComponents<AudioSource>();
    }

    public AudioSource[] getSoundList() {
        return this.listePistes;
    }

    public AudioSource getFirstSound() {
        if (this.listePistes != null) {
            return this.listePistes[0];
        }
        else {
            Debug.LogWarning("WARN    AudioScript:getFirstSound(): No AudioSource found for this GameObject.");
            return null;
        }
        
    }
}
