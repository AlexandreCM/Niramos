using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementMultijoueur : MonoBehaviour
{
    public delegate void OnMove(Vector3 vec3);
    public event OnMove OnCommandMove;

    private Rigidbody2D rigidbodyJoueur = null;
    private Vector3 positionPrecdante;
    private void OnEnable()
    {
        rigidbodyJoueur = this.gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(rigidbodyJoueur != null)
        {
            if(positionPrecdante != this.gameObject.transform.position)
            {
                if(OnCommandMove != null)
                    OnCommandMove(this.gameObject.transform.position);
            }
            positionPrecdante = this.gameObject.transform.position;
        }
    }
}
