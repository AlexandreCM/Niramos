using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour {

    public float HealAmount = 30;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        Player t_Player = collision.gameObject.GetComponent<Player>();

        if (t_Player)
        {
            t_Player.Heal(HealAmount);

            Destroy(gameObject);
        }
    }

   
}
