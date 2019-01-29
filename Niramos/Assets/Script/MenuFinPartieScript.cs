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
    private Image imageGagant;
    [SerializeField]
    private Text texteFinPartie;

    private void OnEnable()
    {
        //imageGagant.sprite; = sprite gagnant
        //texteFinPartie.text = text gagnant
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
