using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   La classe AudioScript stocke la liste des AudioSource attachés à une entité (GameObject).
///   Il est possible de récupérer les données de ce dictionnaire pour jouer un son.
///   Les sons sont stockés dans le même ordre qu'ils sont organisés dans l'inspecteur.
/// </summary>
public static class AudioScript2 {
    
    /// <summary>
    /// Retourne tous les composants AudioSource d'un objet dans le monde.
    /// </summary>
    /// <param name="g">L'objet contenant les AudioSource à récupérer.</param>
    /// <returns>Tableau des AudioSource trouvés ; null si aucun.</returns>

    public static AudioSource[] getSoundsOf(GameObject g) {
        return g.GetComponents<AudioSource>();
    }

    /// <summary>
    /// Retourne le premier composant AudioSource d'un objet dans le monde.
    /// </summary>
    /// <param name="g">L'objet contenant l'AudioSource à récupérer.</param>
    /// <returns>Le premier AudioSource de l'objet ; null si aucun.</returns>
    public static AudioSource getFirstSound() {
        AudioSource[] listePistes = this.getSoundsOf(g);
        if (listePistes != null) {
            return listePistes[0];
        }
        else {
            Debug.LogWarning("WARN    AudioScript:getFirstSound(): No AudioSource found for GameObject " + g.name);
            return null;
        }
        
    }
}
