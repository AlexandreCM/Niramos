using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEnvironnemental : MonoBehaviour
{
    [SerializeField]
    private float dmg; // Damage done when touched.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("INFO    ObstacleEnvironnemental:OnCollisionEnter2D(Collision2D collision) triggered for " + this.gameObject.name);
        VieJoueur vieJ = collision.gameObject.GetComponent<VieJoueur>();
        Test_Joueur testJ = collision.gameObject.GetComponent<Test_Joueur>();
        if (vieJ && testJ) {
            Vector3 force3d = collision.gameObject.transform.position - this.gameObject.transform.position;
            Vector2 force2d = new Vector2(force3d.x, force3d.y);
            testJ.repousserJoueur(force2d, this.dmg * 128.0f);
            vieJ.faireDegat(this.dmg);
        }
        else {
            Debug.Log("INFO    ObstacleEnvironnemental:OnCollisionEnter2D(Collision2D collision) Colliding entity is not a player.");
        }
    }
}
