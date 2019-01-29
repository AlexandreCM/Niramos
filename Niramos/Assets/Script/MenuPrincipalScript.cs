using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipalScript : MonoBehaviour
{
    public Button btnJouer, btnOption, btnQuitter;

    // Start is called before the first frame update
    void Start()
    {
        btnJouer.onClick.AddListener(actionJouer);
        btnOption.onClick.AddListener(actionOption);
        btnQuitter.onClick.AddListener(actionQuitter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void actionJouer()
    {
        Debug.Log("Clic sur 'Jouer'");
    }
    void actionOption(){
        Debug.Log("Clic sur 'Option'");
    }

    void actionQuitter(){
        Debug.Log("Clic sur 'Quitter'");
    }
}
