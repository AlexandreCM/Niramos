using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAuSol : MonoBehaviour
{
    private bool auSol;
    public LayerMask terrain;

    public bool isAuSol()
    {
       return auSol = Physics2D.OverlapArea(
            new Vector2(transform.position.x -0.5f, transform.position.y - 0.5f), 
            new Vector2(transform.position.x, transform.position.y), 
            terrain
        );
    }
}
