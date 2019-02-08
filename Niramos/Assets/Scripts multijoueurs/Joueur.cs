using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public string JoueurName;
    public Vector3 position;
    public string id;

    void Start(){
        this.name = JoueurName;
    }
}
