using UnityEngine;
using UnityEngine.UI;

public class ActionCommencer : MonoBehaviour
{
    Button myButton;

    void Awake()
    {
        myButton = GetComponent<Button>();

        myButton.onClick.AddListener(() => {
            myFunctionForOnClickEvent();

        });

    }

    void myFunctionForOnClickEvent()
    {
        print("Bonsoir");
        myButton.enabled = false;
        myButton.gameObject.SetActive(false);
    }
}
