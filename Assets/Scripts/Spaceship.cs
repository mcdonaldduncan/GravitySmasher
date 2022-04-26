using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] Material launchMaterial;
    [SerializeField] float launchForce;

    Rigidbody rb;
    LineRenderer line;

    Vector2 startingPosition;

    bool mouseDrag;
    
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.material = launchMaterial;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startingPosition = rb.position;
    }

    void Update()
    {
        DragProjectile();
    }

    void DragProjectile()
    {
        if (mouseDrag)
        {
            Vector2 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mp;
            Vector3 anticipatedForce = transform.position - (Vector3)startingPosition;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, startingPosition - (Vector2)anticipatedForce * 4f);
            line.endWidth = .01f;
        }
    }

    private void OnMouseDown()
    {
        mouseDrag = true;
        line.enabled = true;
    }

    private void OnMouseUp()
    {
        line.enabled = false;
        mouseDrag = false;
        rb.isKinematic = false;
        Launch();
    }

    void Launch()
    {
        Vector2 force = startingPosition - (Vector2)rb.position;
        float distance = force.magnitude;
        distance = Mathf.Clamp(distance, 5f, 10f);
        force.Normalize();
        force *= distance * launchForce;
        rb.AddForce(force, ForceMode.Impulse);

    }

}
