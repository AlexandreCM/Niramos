using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmeCQC : MonoBehaviour
{
    private bool estPickup = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.layer == this.gameObject.layer)
        {
            GameObject Joueur = collision.gameObject;
            this.GetComponents<CapsuleCollider2D>()[0].enabled = false;
            this.GetComponents<CapsuleCollider2D>()[1].enabled = false;
            this.transform.parent = Joueur.transform;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.gameObject.transform.position.Set(5,0,0);
            Transform transformParent = this.transform.parent.transform;
            this.gameObject.transform.rotation.Set(transformParent.rotation.x, transformParent.rotation.y, transformParent.rotation.z, transformParent.rotation.w);
        }
    }
}
