using UnityEngine;
using UnityEngine.UI;

public class ActionCommencer : MonoBehaviour
{
    Button myButton;
    private Transform pointVue;

    void Awake()
    {
        myButton = GetComponent<Button>();

        myButton.onClick.AddListener(() => {
            myFunctionForOnClickEvent();

        });
    }

    void myFunctionForOnClickEvent()
    {
        print("C'est parti");
        myButton.enabled = false;
        myButton.gameObject.SetActive(false);
        pointVue = GameObject.FindGameObjectWithTag("pointVueCamera").transform;
        pointVue.position = new Vector3(0.0f, 3.0f, 0.0f);
    }
}
