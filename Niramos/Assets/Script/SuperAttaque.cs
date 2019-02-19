using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttaque : MonoBehaviour
{
    public int forceFracasse = -10;
    // public int delaiBase = 500;
    // public int delai;

    [SerializeField]
    LayerMask HitLayers;
    public float degats;
    private bool estEnMain = false;
    private bool enAttaque = false;
    [SerializeField]
    private float quantitierKnockBackx = 200;
    private float quantitierKnockBacky = 200;
    private float directionRay = 90;
    private float positionBaseRay = -0.2f;
    private float distanceRay = 0.5f;
    
    public void attaque(Rigidbody2D joueur)
    {
        joueur.AddForce(new Vector2(0, forceFracasse), ForceMode2D.Impulse);
        test();
        
        
        
        
        /*if(delai <= 0) {
            joueur.AddForce(new Vector2(0, forceFracasse), ForceMode2D.Impulse);
            delai = delaiBase;
        }*/
        
    }

    private void test()
    {
        Debug.Log("test");
        //Vector2 departRay = new Vector2(this.transform.position.x, this.transform.position.y);
        RaycastHit2D[] toucher = Physics2D.RaycastAll(transform.position, new Vector2(directionRay, 0), distanceRay, HitLayers);
        //Debug.DrawRay(transform.position, new Vector3(90, 0, 90), Color.red, 5);
        /* foreach (RaycastHit2D hit in toucher)
        {
            Debug.Log(hit.rigidbody.gameObject.name);
            VieJoueur joueur = null;
            joueur = hit.rigidbody.gameObject.GetComponent<VieJoueur>();
            if (joueur != null)
            {
                Debug.Log("hit");
                joueur.faireDegat(degats);
                if (this.gameObject.transform.position.x > hit.rigidbody.gameObject.transform.position.x && quantitierKnockBackx > 0)
                    quantitierKnockBackx *= -1;
                else if (this.gameObject.transform.position.x < hit.rigidbody.gameObject.transform.position.x && quantitierKnockBackx < 0)
                    quantitierKnockBackx *= -1;
                hit.rigidbody.AddForce(new Vector2(quantitierKnockBackx, quantitierKnockBacky));
            }
        }*/
    }
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        // delai --;
    }
    
}
