using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère l'affichage des sprites (effets 2D) dans le monde.
/// </summary>
public class SpriteManager : MonoBehaviour {

    [SerializeField]
    private GameObject damageEffect;

    public void spawnDamageSprite(Vector3 position) {
        Instantiate(this.damageEffect, position, Quaternion.identity);
    }
}