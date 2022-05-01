using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    // Material assigned in inspector
    [SerializeField] Material launchMaterial;
    
    // Launch force assigned in inspector
    [SerializeField] float launchForce;

    // Objects assigned at runtime
    Rigidbody rb;
    LineRenderer line;

    // Starting position of the spaceship assigned at runtime
    Vector2 startingPosition;

    // Boolean to handle mouse being dragged
    bool mouseDrag;
    
    // Cache relevant objects and assign runtime variables
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

    // Snap object to mouse position after clciking on object, generate a line to indicate anticipated launch
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

    // On mouse click over object set mouseDrag to true and enable line
    private void OnMouseDown()
    {
        mouseDrag = true;
        line.enabled = true;
    }

    // On mouse release set mouseDrag to false, disable line and launch object
    private void OnMouseUp()
    {
        line.enabled = false;
        mouseDrag = false;
        rb.isKinematic = false;
        Launch();
    }

    // Calculate launch vector by taking starting position less the current position
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
