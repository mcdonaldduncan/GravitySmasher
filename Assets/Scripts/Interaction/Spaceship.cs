using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class Spaceship : MonoBehaviour
{
    // Material assigned in inspector
    [SerializeField] Material launchMaterial;
    
    // Launch force assigned in inspector
    [SerializeField] float launchForce;

    // Objects assigned at runtime
    Rigidbody rb;
    LineRenderer line;
    UIManager uiManager;

    AttractionData attractionData;

    // Starting position of the spaceship assigned at runtime, maximum bounds of screen assigned at runtime
    Vector2 startingPosition;
    [SerializeField] Vector2 maximumPosition;

    // Boolean to handle mouse being dragged
    bool mouseDrag;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        attractionData = new AttractionData(transform.position, rb.mass);
    }

    // Cache relevant objects and assign runtime variables
    void Start()
    {
        uiManager = AssignUIManager();
        maximumPosition = FindWindowLimits();
        line = gameObject.AddComponent<LineRenderer>();
        line.material = launchMaterial;
        startingPosition = rb.position;
    }

    void Update()
    {
        DragProjectile();
        CheckWindowLimits();
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
        distance = Mathf.Clamp(distance, 5f, 20f);
        force.Normalize();
        force *= distance * launchForce;
        rb.AddForce(force, ForceMode.Impulse);
        IncrementProjectilesUsed();
    }

    // Check if the projectile has left the window limits
    void CheckWindowLimits()
    {
        if (transform.position.x > maximumPosition.x || transform.position.x < -maximumPosition.x)
        {
            ResetProjectile();
        }

        if (transform.position.y > maximumPosition.y || transform.position.y < -maximumPosition.y)
        {
            ResetProjectile();
        }
    }

    // Reset the projectile to start position and set it to be ready for launch
    void ResetProjectile()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startingPosition;
    }

    // Reset projectile on collision
    private void OnCollisionEnter(Collision collision)
    {
        ResetProjectile();
    }

    // Increment the number of projectiles used and update UI
    void IncrementProjectilesUsed()
    {
        DataManager.instance.projectilesUsed++;
        uiManager.UpdateScore();
    }

    
}
