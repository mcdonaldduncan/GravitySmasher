using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] float launchForce;

    Rigidbody rb;
    Vector2 startingPosition;

    bool mouseDrag;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startingPosition = rb.position;
    }

    void Update()
    {
        if (mouseDrag)
        {
            Vector2 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mp;
            Vector3 anticipatedForce = (Vector3)startingPosition - transform.position;
            //transform.rotation = Quaternion.LookRotation(new Vector3(anticipatedForce.x, anticipatedForce.y + 90, anticipatedForce.z - 90));
        }


        //transform.rotation = Quaternion.LookRotation(rb.velocity);
        //Vector3 eulerRotation = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y + 90, rotation.eulerAngles.z - 90);
        //transform.rotation = Quaternion.LookRotation(eulerRotation);
    }

    private void OnMouseDown()
    {
        mouseDrag = true;
    }

    private void OnMouseUp()
    {
        mouseDrag = false;
        rb.isKinematic = false;
        Launch();
    }

    void Launch()
    {
        Vector2 force = startingPosition - (Vector2)rb.position;
        float distance = force.magnitude;
        distance = Mathf.Clamp(distance, 10f, 50f);
        force.Normalize();
        force *= distance * launchForce;
        rb.AddForce(force, ForceMode.Impulse);

    }

}
