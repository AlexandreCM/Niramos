using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{

    public delegate void OnMove(Vector3 vec3);
    public event OnMove OnCommandMove;

    public WatchButton Left;
    public WatchButton Right;
    public WatchButton Backward;
    public WatchButton Forward;

    
    public GameObject JoueurObject;

    public bool leftMove;
    public bool rightMove;
    public bool backMove;
    public bool frontMove;

    public void ActionJoystick()
    {
        Left.onPress += OnPress;
        Right.onPress += OnPress;
        Backward.onPress += OnPress;
        Forward.onPress += OnPress;
    }

    void OnPress(GameObject unit, bool state)
    {
        switch (unit.name)
        {
            case "btnGauche":
                LeftMove(state);
                break;
            case "btnDroite":
                RightMove(state);
                break;
            case "btnReculer":
                BackMove(state);
                break;
            case "btnAvancer":
                FrontMove(state);
                break;
        }

        Debug.Log("OnPress JoystickController "+ unit.name+" "+state.ToString());
    }

    private void LeftMove(bool state)
    {
        Debug.Log("LeftMove");
        leftMove = state;
    }
    private void RightMove(bool state)
    {
        Debug.Log("RightMove");
        rightMove = state;
    }
    private void BackMove(bool state)
    {
        Debug.Log("BackMove");
        backMove = state;
    }
    private void FrontMove(bool state)
    {
        Debug.Log("FrontMove");
        frontMove = state;
    }




    // Update is called once per frame
    void Update()
    {
        Transform tranf = JoueurObject.transform;
        
        if (leftMove)
        {
            Debug.Log("LeftMove TRUE");
            JoueurObject.transform.position = new Vector3(tranf.position.x - (2f * Time.deltaTime), tranf.position.x, tranf.position.z);
           if (OnCommandMove != null)
            {
                OnCommandMove(JoueurObject.transform.position);
            }
        }

        if (rightMove)
        {
            Debug.Log("RightMove TRUE");
            JoueurObject.transform.position = new Vector3(tranf.position.x + (2f * Time.deltaTime), tranf.position.x, tranf.position.z);
            if (OnCommandMove != null)
            {
                OnCommandMove(JoueurObject.transform.position);
            }
        }

        if (backMove)
        {
            Debug.Log("BackMove TRUE");
            JoueurObject.transform.position = new Vector3(tranf.position.x, tranf.position.x, tranf.position.z - (2f * Time.deltaTime));
            if (OnCommandMove != null)
            {
                OnCommandMove(JoueurObject.transform.position);
            }
        }

        if (frontMove)
        {
            Debug.Log("Frontmove TRUE");
            JoueurObject.transform.position = new Vector3(tranf.position.x, tranf.position.x, tranf.position.z + (2f * Time.deltaTime));
            if (OnCommandMove != null)
            {
                OnCommandMove(JoueurObject.transform.position);
            }
        }
    }
}
