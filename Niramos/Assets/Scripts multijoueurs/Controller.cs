using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using SocketIO;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public LoginController loginPanel;
    public JoystickController joystick;
    public SocketIOComponent socket;
    public Joueur JoueurGameObject;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(ConnectToServer());
        socket.On("USER_CONNECTED", OnUserConnected);
        socket.On("PLAY", OnUserPlay);
        socket.On("MOVE", onUserMove);
        socket.On("USER_DISCONNECTED", onUserDisconnected);
        joystick.gameObject.SetActive(false);
        loginPanel.playBtn.onClick.AddListener(OnClickPlayBtn);
        joystick.OnCommandMove += OnCommandMove;
    }

    void OnCommandMove(Vector3 vec3)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        Vector3 position = new Vector3(vec3.x, vec3.y, vec3.z);
        data["position"] = position.x + "," + position.y + "," + position.z;
        socket.Emit("MOVE", new JSONObject(data));
    }

    void onUserMove(SocketIOEvent obj)
    {
        GameObject Joueur = GameObject.Find(JsonToString(obj.data.GetField("name").ToString(), "\"")) as GameObject;
        Joueur.transform.position = JsonToVector3(JsonToString(obj.data.GetField("position").ToString(), "\""));
    }

    string JsonToString(string target, string s)
    {
        string[] newString = Regex.Split(target, s);
        return newString[1];
    }

    Vector3 JsonToVector3(string target)
    {
        Vector3 newVector;
        string[] newString = Regex.Split(target, ",");
        newVector = new Vector3(float.Parse(newString[0]), float.Parse(newString[1]), float.Parse(newString[2]));
        return newVector;
    }

    IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);
        socket.Emit("USER_CONNECT");

        yield return new WaitForSeconds(1f);

        /* Dictionary<string, string> data = new Dictionary<string, string>();
        data["name"] = "NomDeTest";
        Vector3 position = new Vector3(0, 0, 0);
        data["position"] = position.x + "," + position.y + "," + position.z;
        socket.Emit("PLAY", new JSONObject(data)); */
    }

    private void OnUserConnected(SocketIOEvent evt)
    {
        Debug.Log("Get the message from server is: " + evt.data + " - onUserConnected");
        GameObject otherJoueur = GameObject.Instantiate(JoueurGameObject.gameObject, JoueurGameObject.position, Quaternion.identity) as GameObject;
        Joueur otherJoueurCom = otherJoueur.GetComponent<Joueur>();
        otherJoueurCom.JoueurName = JsonToString(evt.data.GetField("name").ToString(), "\"");
        otherJoueur.transform.position = JsonToVector3(JsonToString(evt.data.GetField("position").ToString(), "\""));
        otherJoueurCom.id = JsonToString(evt.data.GetField("id").ToString(), "\"");
    }

    void OnClickPlayBtn()
    {
        Debug.Log("onClickPlayBtn");
        if (loginPanel.inputField.text != "")
        {
            Debug.Log("OnClickPlayBtn : if");
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["name"] = loginPanel.inputField.text;
            Vector3 position = new Vector3(0, 0, 0);
            data["position"] = position.x + "," + position.y + "," + position.z;
            socket.Emit("PLAY", new JSONObject(data));
        }
        else
        {
            Debug.Log("OnClickPlayBtn : else");
            loginPanel.inputField.text = "Entrez un nom !";
        }
    }

    void onUserDisconnected(SocketIOEvent obj)
    {
        Destroy(GameObject.Find(JsonToString(obj.data.GetField("name").ToString(), "\"")));
    }

    private void OnUserPlay(SocketIOEvent evt)
    {
        Debug.Log("Get the message from server is: " + evt.data + " - onUserPlay");

        //cacher le panel de connexion
        loginPanel.gameObject.SetActive(false);
        joystick.gameObject.SetActive(true);
        joystick.ActionJoystick();

        GameObject Joueur = GameObject.Instantiate(JoueurGameObject.gameObject, JoueurGameObject.position, Quaternion.identity) as GameObject;
        Joueur JoueurCom = Joueur.GetComponent<Joueur>();

        JoueurCom.JoueurName = JsonToString(evt.data.GetField("name").ToString(), "\"");
        JoueurCom.transform.position = JsonToVector3(JsonToString(evt.data.GetField("position").ToString(), "\""));
        JoueurCom.id = JsonToString(evt.data.GetField("id").ToString(), "\"");
        joystick.JoueurObject = Joueur;
    }


}
