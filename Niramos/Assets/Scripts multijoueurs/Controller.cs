using System.Collections;
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
    [SerializeField]
    private GameObject[] listeArmePossible;
    [SerializeField]
    private GameObject[] listeSpawnPoint;

    /// <summary>
    /// L'objet contenant le script UI_Time.
    /// </summary>
    [SerializeField]
    private GameObject UITime;

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
        socket.On("PLAYER_LOSE_HEALTH", onPlayerTakingDamage);
        socket.On("DROP_RESPONSE", onUserDropItem);
        socket.On("UN_JOUEUR_EST_MORT", onUserDeath);
        socket.On("RESPAWN", onUserRespawn);
        socket.On("SPAWN_ARME", onWeaponSpawn);
        socket.On("FIRE_BOW", onUserFireBow);
        socket.On("GAME_OVER", onGameOver);
        socket.On("BEGIN_GAME", onGameBegin);
        GestionnaireAttaque.ajouterEvenement("VieJ1Changer", onHitPlayer);
        GestionnaireItem.ajouterEvenement("Ramassable", onUserPickupItem);
        GestionnaireEvenement.ajouterEvenement("ObjetLancer", onPlayerDropItem);
        GestionnaireEvenement.ajouterEvenement("JoueurMort", onPlayerDeath);
        GestionnaireEvenement.ajouterEvenement("arcTirer", onArcTirer);
        //joystick.gameObject.SetActive(false);
        loginPanel.playBtn.onClick.AddListener(OnClickPlayBtn);
        //joystick.OnCommandMove += OnCommandMove;

        if(this.UITime == null) {
            Debug.LogWarning("WARN    " + this.gameObject.name + ":Controller::start(): No GameObject set for countdown display; timer will not work.");
        }
        else if(this.UITime.GetComponent<UI_Time>() == null) {
            Debug.LogWarning("WARN    " + this.gameObject.name + ":Controller::start(): No UI_Time script set for countdown display; timer will not work.");
        }
    }

    private void onGameBegin(SocketIOEvent obj) {
        JoueurGameObject.gameObject.GetComponent<mouvement>().enabled = true;
        this.UITime.GetComponent<UI_Time>().start();

    }

    void onGameOver(SocketIOEvent obj)
    {
        JoueurGameObject.gameObject.GetComponent<mouvement>().enabled = false;
        this.UITime.GetComponent<UI_Time>().stop();
    }

    void onWeaponSpawn(SocketIOEvent obj)
    {
        Debug.Log(obj);
        int typeArme = JsonToInt(obj.data.GetField("typeArme").ToString());
        Debug.Log(typeArme);
        int point = JsonToInt(obj.data.GetField("pointSpawn").ToString());
        Debug.Log(point);
        int idArme = JsonToInt(obj.data.GetField("idObjet").ToString());
        GameObject arme = (GameObject)Instantiate(listeArmePossible[typeArme], listeSpawnPoint[point].transform.position, listeSpawnPoint[point].transform.rotation);
        arme.GetComponent<ObjetRamasable>().setId(idArme);
    }
    void onUserFireBow(SocketIOEvent obj)
    {
        string nomJoueur = JsonToString(obj.data.GetField("nomJoueur").ToString(), "\"");
        GameObject joueur = GameObject.Find(nomJoueur);
        Arc arc = joueur.GetComponent<ManagerJoueur>().objetEnMain.GetComponent<Arc>();
        if (arc != null)
            arc.tirer(false);
    }
    void onArcTirer()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["nomJoueur"] = JoueurGameObject.JoueurName;
        socket.Emit("BOW_FIRED", new JSONObject(data));
    }

    void onUserRespawn(SocketIOEvent obj)
    { 
        Debug.Log(obj);
        int point =JsonToInt(obj.data.GetField("pointRespawn").ToString());
        string nomJoueur = JsonToString(obj.data.GetField("nomJoueur").ToString(), "\"");
        //Debug.Log(point + " " + nomJoueur);
        GameObject joueur = GameObject.Find(nomJoueur);
        DegatsJoueur degatsJoueur = joueur.GetComponent<DegatsJoueur>();
        GestionnaireReapparition.getEvent().Invoke(degatsJoueur, point);
    }

    void onUserDeath(SocketIOEvent obj)
    {
        string nomJoueur = JsonToString(obj.data.GetField("nomJoueur").ToString(), "\"");
        GameObject joueur = GameObject.Find(nomJoueur);
        GestionnaireMort.getEvent().Invoke(joueur.GetComponent<VieJoueur>());
    }

    void onPlayerDeath()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["nomJoueur"] = JoueurGameObject.JoueurName;
        socket.Emit("MORT", new JSONObject(data));
    }

    void onUserDropItem(SocketIOEvent obj)
    {
        string nomJoueur = JsonToString(obj.data.GetField("nomJoueur").ToString(), "\"");
        GameObject joueur = GameObject.Find(nomJoueur);
        joueur.GetComponent<ManagerJoueur>().lancerObjet();
    }

    void onPlayerDropItem()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["nomJoueur"] = JoueurGameObject.JoueurName;
        data["idObjet"] = JoueurGameObject.GetComponent<ManagerJoueur>().objetEnMain.GetComponent<ObjetRamasable>().getId().ToString();
        socket.Emit("DROP", new JSONObject(data));
    }

    void onHitPlayer(float degat, string nom, float knockback)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["nomJoueur"] = nom;
        data["degat"] = degat.ToString();
        data["knockback"] = knockback.ToString();
        socket.Emit("HIT", new JSONObject(data));
    }

    void onPlayerTakingDamage(SocketIOEvent obj)
    {
        int degat = int.Parse(JsonToString(obj.data.GetField("degat").ToString(), "\""));
        string nomJoueur = JsonToString(obj.data.GetField("nomJoueur").ToString(), "\"");
        float knockback = float.Parse(JsonToString(obj.data.GetField("knockback").ToString(), "\""));
        Debug.Log(nomJoueur + " hit");
        GameObject joueur = GameObject.Find(nomJoueur);
        joueur.GetComponent<VieJoueur>().faireDegat(degat);
        joueur.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback, Mathf.Abs(knockback)));
    }

    void onOtherPlayerPickup(SocketIOEvent obj)
    {
        Debug.Log(obj);
        int idObjet = int.Parse(JsonToString(obj.data.GetField("idObjet").ToString(), "\""));
        string nomJoueur = JsonToString(obj.data.GetField("nomJoueur").ToString(), "\"");
        GameObject joueur = GameObject.Find(nomJoueur);
        joueur.GetComponent<ManagerJoueur>().ramasserObjet(idObjet);
    }

    void OnCommandMove(Vector3 vec3) // Cette méthod va servir à envoyer au serveur les nouvelles coordonnées du joueur.
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        Vector3 position = new Vector3(vec3.x, vec3.y, vec3.z);
        data["nom"] = JoueurGameObject.JoueurName;
        data["position"] = position.x + "/" + position.y + "/" + position.z;
        data["direction"] = JoueurGameObject.gameObject.GetComponent<ManagerJoueur>().getDirectionVerDroite().ToString();
        socket.Emit("MOVE", new JSONObject(data));
    }
    void onItemPickupResponce(SocketIOEvent evt)
    {
        Debug.Log(evt);
        int idObjet = int.Parse(JsonToString(evt.data.GetField("idObjet").ToString(), "\""));
        bool rammasable = JsonToBool(evt.data.GetField("disponible").ToString(), "\"");
        //Debug.Log(rammasable);
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
        data["idObjet"] = idObjet.ToString();

        socket.Emit("ITEM_PICKUP", new JSONObject(data));
    }

    void onUserMove(SocketIOEvent obj)
    {
        //Debug.Log(JsonToString(obj.data.GetField("nom").ToString(), "\"") + " se déplace vers "+JsonToVector3(obj.data.GetField("position").ToString()));
        Vector3 position = JsonToVector3(obj.data.GetField("position").ToString());

        GameObject joueur = GameObject.Find(JsonToString(obj.data.GetField("nom").ToString(), "\"")) as GameObject;
        if (joueur.GetComponent<mouvement>() == null)
        {
            //Debug.Log(joueur.transform.position);
            joueur.GetComponent<ManagerJoueur>().setPosition(position);
        }
        //Debug.Log(JsonToBool(obj.data.GetField("direction").ToString(), "\"").ToString());
        joueur.GetComponent<ManagerJoueur>().setDirectionVerDroite(JsonToBool(obj.data.GetField("direction").ToString(), "\""));
    }
    bool JsonToBool(string target, string s)
    {
        //Debug.Log(target);
        /*string[] newString = Regex.Split(target, s);
        Debug.Log(newString[1] + 1);
        Debug.Log(newString[0] + 0);*/
        
        if (target.Equals("\"True\"")) return true;
        return false;
    }
    string JsonToString(string target, string s)
    {
        //Debug.Log(target);
        string[] newString = Regex.Split(target, s);
        return newString[1];
    }
    int JsonToInt(string target)
    {
        return int.Parse(target);
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
        if(JsonToString(evt.data.GetField("nom").ToString(), "\"") == loginPanel.inputField.text)
            return;

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
