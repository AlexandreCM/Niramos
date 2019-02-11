using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCommencer : MonoBehaviour {

    Button myButton;
 
     void Awake()
     {
         myButton = GetComponent<Button>(); // <-- you get access to the button component here
 
         myButton.onClick.AddListener( () => {
             myFunctionForOnClickEvent();
             
        });  // <-- you assign a method to the button OnClick event here
         
     }
 
     void myFunctionForOnClickEvent()
     {
        // your code goes here
        print("Bonsoir");
        myButton.enabled = false;      
        myButton.gameObject.SetActive(false);
        
        
        //transform.Translate(new Vector3(0,-0.5f * Time.deltaTime,0));
     }

     void Update() {
        for(int i = 0; i<50; i++){
            //Camera.main.gameObject.transform.Translate(0, i, 0);
        }
     }
}
