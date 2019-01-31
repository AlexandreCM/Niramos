using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFinPartieScript : MonoBehaviour
{
    [SerializeField]
    private Button bouttonRejouer;
    [SerializeField]
    private Button bouttonRetourMenu;
    [SerializeField]
    private GameObject panelJoueur1;
    [SerializeField]
    private GameObject panelJoueur2;
    [SerializeField]
    private GameObject panelJoueur3;
    [SerializeField]
    private GameObject panelJoueur4;

    private void OnEnable()
    {
       
        bouttonRejouer.onClick.AddListener(rejouer);
        bouttonRetourMenu.onClick.AddListener(retourMenu);
    }

    private void rejouer()
    {
        //TODO: action rejouer
    }
    private void retourMenu()
    {
        //TODO: charger sceneMenuPrincipale
    }
}
