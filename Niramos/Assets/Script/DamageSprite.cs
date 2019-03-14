using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SpriteRenderer utilisé pour l'apparition et la disparition
/// des sprites de dégâts.
/// </summary>
public class DamageSprite : MonoBehaviour
{

    /// <summary>
    /// Le SpriteRenderer pour le sprite.
    /// </summary>
    private SpriteRenderer sprRenderer;

    /// <summary>
    /// Initialise le script avant la première image (frame).
    /// </summary>
    void Start()
    {
        this.sprRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if(this.sprRenderer == null) {
            Debug.LogError("ERRR    " + this.gameObject.name + ":DamageSprite::Start(): No sprite found; destroying object.");
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, 0.25f);
    }
    
}
