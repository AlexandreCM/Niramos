﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Text.RegularExpressions;
using SocketIO;
using System.Collections.Generic;
using System;

public class Controller : MonoBehaviour
{
    public LoginController loginPanel;
    // public JoystickController joystick;
    public SocketIOComponent socket;
    private Joueur JoueurGameObject;
    public GameObject prefabJoueur;
    private string tagRammasable = "rammasable";

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(ConnectToServer());
        socket.On("USER_CONNECTED", OnUserConnected);
        socket.On("PLAY", OnUserPlay);
        socket.On("MOVE", onUserMove);
        socket.On("USER_DISCONNECTED", onUserDisconnected);
        socket.On("AUCUNE_SESSION_DISPO", onAucuneSessionDispo);
        socket.On("ITEM_PICKUP_RESPONSE", onItemPickupResponce);
        socket.On("PLAYER_PICKUP_ITEM", onOtherPlayerPickup);
        socket.On("", onPlayerTakingDamage);
        socket.On("", onUserDropItem);
        GestionnaireItem.ajouterEvenement("Ramassable", onUserPickupItem);
        GestionnaireEvenement.ajouterEvenement("ObjetLancer", onPlayerDropItem);
        //joystick.gameObject.SetActive(false);
        loginPanel.playBtn.onClick.AddListener(OnClickPlayBtn);
        //joystick.OnCommandMove += OnCommandMove;
    }

    void onPlayerDropItem()
    {

    }

    void onPlayerTakingDamage(SocketIOEvent obj)
    {
        int degat = int.Parse(JsonToString(obj.data.GetField("degat").ToString(), "\""));
        string nomJoueur = JsonToString(obj.data.GetField("nom").ToString(), "\"");
        GameObject joueur = GameObject.Find(nomJoueur);
        joueur.GetComponent<VieJoueur>().faireDegat(degat);
    }

    void onUserDropItem(SocketIOEvent obj)
    {
        string nomJoueur = JsonToString(obj.data.GetField("nom").ToString(), "\"");
        GameObject joueur = GameObject.Find(nomJoueur);
        joueur.GetComponent<ManagerJoueur>().lancerObjet();
    }

    void onOtherPlayerPickup(SocketIOEvent obj)
    {
        Debug.Log(obj);
        int idObjet = int.Parse(JsonToString(obj.data.GetField("idObjet").ToString(), "\""));
        string nomJoueur = JsonToString(obj.data.GetField("nom").ToString(), "\"");
        GameObject joueur = GameObject.Find(nomJoueur);
        joueur.GetComponent<ManagerJoueur>().ramasserObjet(idObjet);
    }

    void OnCommandMove(Vector3 vec3) // Cette méthod va servir à envoyer au serveur les nouvelles coordonnées du joueur.
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        Vector3 position = new Vector3(vec3.x, vec3.y, vec3.z);
        data["nom"] = JoueurGameObject.JoueurName;
        data["position"] = position.x + "/" + position.y + "/" + position.z;
        socket.Emit("MOVE", new JSONObject(data));
    }
    void onItemPickupResponce(SocketIOEvent evt)
    {
        Debug.Log(evt);
        int idObjet = int.Parse(JsonToString(evt.data.GetField("idObjet").ToString(), "\""));
        bool rammasable = JsonToBool(evt.data.GetField("disponible").ToString(), "\"");
        //Debug.Log(idObjet);
        if (JsonToBool(evt.data.GetField("disponible").ToString(), "\"")) onCanPickup(idObjet);
        else onCantPickup(idObjet);
        
    }
    void onCanPickup(int idObjet)
    {
        //Debug.Log("rammase");
        JoueurGameObject.gameObject.GetComponent<ManagerJoueur>().ramasserObjet(idObjet);
    }
    
    void onCantPickup(int idObjet)
    {
        //Debug.Log("rammase pas");
        GameObject[] liseRammasable = GameObject.FindGameObjectsWithTag(tagRammasable);

        foreach (GameObject objet in liseRammasable)
        {
            ObjetRamasable rammasable = objet.GetComponent<ObjetRamasable>();
            if (rammasable != null)
            {
                if (rammasable.getId() == idObjet)
                {
                    rammasable.setEstEnMain(false);
                }
            }
        }
    }
    void onUserPickupItem(int idObjet, string nom){
        //Debug.Log("send");
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["nom"] = nom;
        data["idObjet"] = idObjet+"";

        socket.Emit("ITEM_PICKUP", new JSONObject(data));
    }

    void onUserMove(SocketIOEvent obj)
    {
        //Debug.Log(JsonToString(obj.data.GetField("nom").ToString(), "\"") + " se déplace vers "+JsonToVector3(obj.data.GetField("position").ToString()));
        Vector3 position = JsonToVector3(obj.data.GetField("position").ToString());

        
        GameObject joueur = GameObject.Find(JsonToString(obj.data.GetField("nom").ToString(), "\"")) as GameObject;
        if (joueur.GetComponent<mouvement>() == null)
        {
            Debug.Log(joueur.transform.position);
            joueur.GetComponent<ManagerJoueur>().setPosition(position);
        }
    }
    bool JsonToBool(string target, string s)
    {
        /*Debug.Log(target);
        string[] newString = Regex.Split(target, s);
        Debug.Log(newString[1] + 1);
        Debug.Log(newString[0] + 0);*/
        if (target == "true") return true;
        return false;
    }
    string JsonToString(string target, string s)
    {
        string[] newString = Regex.Split(target, s);
        return newString[1];
    }

    Vector3 JsonToVector3(string target)
    {
        StringBuilder sb = new StringBuilder(target);
        sb.Remove(0, 1);
        sb.Remove(sb.Length - 1, 1);
        target = sb.ToString();

        char[] delimiterChars = { '/' };
        string[] newString = target.Split(delimiterChars);
        return new Vector3(float.Parse(newString[0]), float.Parse(newString[1]), float.Parse(newString[2]));
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
        GameObject otherJoueur = (GameObject)Instantiate(prefabJoueur);
        //SceneManager.MoveGameObjectToScene(m_MyGameObject, SceneManager.GetSceneByName(m_Scene));
        Joueur otherJoueurCom = otherJoueur.AddComponent<Joueur>();
        otherJoueurCom.JoueurName = JsonToString(evt.data.GetField("nom").ToString(), "\"");
        otherJoueur.gameObject.name = JsonToString(evt.data.GetField("nom").ToString(), "\"");
        otherJoueur.transform.position = JsonToVector3(JsonToString(evt.data.GetField("position").ToString(), "\""));
        otherJoueurCom.id = JsonToString(evt.data.GetField("id").ToString(), "\"");
    }

    void OnClickPlayBtn()
    {
        if (loginPanel.inputField.text != "")
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["nom"] = loginPanel.inputField.text;
            
            Vector3 position = new Vector3(0, 0, 0);
            data["position"] = position.x + "," + position.y + "," + position.z;
            socket.Emit("PLAY", new JSONObject(data));
        }
        else
        {
            loginPanel.inputField.text = "Entrez un nom !";
        }
    }

    void onUserDisconnected(SocketIOEvent obj)
    {
        Destroy(GameObject.Find(JsonToString(obj.data.GetField("nom").ToString(), "\"")));
    }

    private void OnUserPlay(SocketIOEvent evt)
    {
        Debug.Log("Get the message from server is: " + evt.data + " - onUserPlay");

        //cacher le panel de connexion
        loginPanel.gameObject.SetActive(false);
        //joystick.gameObject.SetActive(true);
        GameObject[] listeObjetsJoueur = GameObject.FindGameObjectsWithTag("Player");
        GameObject joueur = null;
        foreach(GameObject joueurtemp in listeObjetsJoueur)
        {
            if(joueurtemp.GetComponent<mouvement>() != null)
            {
                joueurtemp.AddComponent<DeplacementMultijoueur>();
                joueur = joueurtemp;
                break;
            }
        }
        joueur.GetComponent<DeplacementMultijoueur>().OnCommandMove += OnCommandMove;
        //joystick.ActionJoystick();
        JoueurGameObject =  joueur.AddComponent<Joueur>();
        JoueurGameObject.JoueurName = loginPanel.inputField.text;
        joueur.name = loginPanel.inputField.text;
        JoueurGameObject.transform.position = JsonToVector3(JsonToString(evt.data.GetField("position").ToString(), "\""));
        JoueurGameObject.id = JsonToString(evt.data.GetField("id").ToString(), "\"");
        //joystick.JoueurObject = joueur;
    }

    void onAucuneSessionDispo(SocketIOEvent obj){
        Debug.Log("Aucune session dispo");
    }


}
