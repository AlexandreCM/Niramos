using UnityEngine;

public class ControlleurCamera : MonoBehaviour
{
    public Vector3 positionCamera = Vector3.zero;
    public Transform pointVue;

    void FixedUpdate()
    {
        positionCamera = new Vector3(
            Mathf.SmoothStep(transform.position.x, pointVue.transform.position.x, 0.15f),
            Mathf.SmoothStep(transform.position.y, pointVue.transform.position.y, 0.15f));
    }

    private void LateUpdate()
    {
        transform.position = positionCamera + Vector3.forward * -10;
    }
}
