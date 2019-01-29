using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPauseScript : MonoBehaviour
{
    [SerializeField]
    private Button bouttonReprendre;
    [SerializeField]
    private Button bouttonRetourMenu;

    private void OnEnable()
    {
        bouttonReprendre.onClick.AddListener(reprendre);
        bouttonRetourMenu.onClick.AddListener(retourMenu);
    }

    private void reprendre()
    {
        //TODO: action reprendre
    }
    private void retourMenu()
    {
        //TODO: charger sceneMenuPrincipale
    }
}
