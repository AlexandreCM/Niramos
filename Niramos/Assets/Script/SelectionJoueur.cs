using UnityEngine;
using UnityEngine.UI;

public class SelectionJoueur : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void Awake()
    {
        //SelectJoueur1 = GetComponent<Button>().name = ;

        /*myButton.onClick.AddListener(() => {
            myFunctionForOnClickEvent();

        });*/
    }

    public void btnClick()
    {
        player.GetComponent<mouvement>().enabled = true;
        player.GetComponent<Rigidbody2D>().simulated = true;

        /*print("C'est parti");
        myButton.enabled = false;
        myButton.gameObject.SetActive(false);
        pointVue = GameObject.FindGameObjectWithTag("pointVueCamera").transform;
        pointVue.position = new Vector3(0.0f, 3.0f, 0.0f);*/
    }
}
