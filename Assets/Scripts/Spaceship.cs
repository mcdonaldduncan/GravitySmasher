using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    Rigidbody rb;
    Vector2 startingPosition;

    bool mouseDrag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startingPosition = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseDrag)
        {
            Vector2 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mp;
            Vector3 anticipatedForce = (Vector3)startingPosition - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(anticipatedForce.x, anticipatedForce.y + 90, anticipatedForce.z - 90));
        }


        transform.rotation = Quaternion.LookRotation(rb.velocity);
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
        Vector2 force = startingPosition - (Vector2)rb.position;
        rb.AddForce(force, ForceMode.Impulse);
    }



}
