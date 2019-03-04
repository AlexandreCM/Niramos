using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAuSol : MonoBehaviour
{
    [SerializeField]
    private float distCalc = 0.5f;

    private bool auSol;
    public LayerMask terrain;

    public bool isAuSol()
    {
       return auSol = Physics2D.OverlapArea(
            new Vector2(transform.position.x - this.distCalc, transform.position.y - this.distCalc), 
            new Vector2(transform.position.x, transform.position.y), 
            terrain
        );
    }
}
