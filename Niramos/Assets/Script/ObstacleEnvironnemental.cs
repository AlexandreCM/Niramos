using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEnvironnemental : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision) {
        VieJoueur vieJ = collision.gameObject.GetComponent<VieJoueur>();
        if (vieJ != null) {
            vieJ.faireDegat(1.0f);
        }
    }
}
