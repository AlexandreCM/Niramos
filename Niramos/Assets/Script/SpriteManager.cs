using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère l'affichage des sprites (effets 2D) dans le monde.
/// </summary>
public class SpriteManager : MonoBehaviour {

    /// <summary>
    /// La ressource à afficher en cas de réception de dommage.
    /// </summary>
    [SerializeField]
    private GameObject damageEffect;

    private bool damageEffectSet = false;

    /// <summary>
    /// Fonction exécutée quand le script est instancié.
    /// Utilisée ici seulement à des fins de vérification.
    /// </summary>
    void onEnable() {

        if(this.damageEffect == null) {
            Debug.LogWarning("WARN    " + this.gameObject.name + ":SpriteManager::onEnable(): No damage sprite set; damage effects will not display.");
        }
        else {
            // If the damage effect is set, we can show them!
            this.damageEffectSet = true;
        }
    }

    public static void creerEffetDegat(GameObject gfxsys, Vector3 position) {
        if(gfxsys == null) {
            Debug.LogWarning("WARN    SpriteManager::creerEffetDegat(Vector3 position): Referenced GameObject is NULL, discarded.");
        }
        else if (position == null) {
            Debug.LogWarning("WARN    SpriteManager::creerEffetDegat(Vector3 position): No position given to spawn the effect to.");
        }
        SpriteManager spr = gfxsys.GetComponent<SpriteManager>();
        if(spr == null) {
            Debug.LogWarning("WARN    SpriteManager::creerEffetDegat(Vector3 position): Given GameObject has no SpriteManager!!!");
        }
        else if(spr.getDamageEffect() == null) {
            Debug.LogWarning("WARN    SpriteManager::creerEffetDegat(Vector3 position): Requested SpriteManager from " + spr.gameObject.name + " has no damage sprite set.");
        }
        else {
            spr.spawnDamageSprite(position);
        }
    }

    public void spawnDamageSprite(Vector3 position) {
        Instantiate(this.damageEffect, position, Quaternion.identity);
    }

    public bool isDamageEffectSet() {
        return this.damageEffectSet;
    }

    public GameObject getDamageEffect() {
        return this.damageEffect;
    }
}