using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseSwarmer : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float maxSpeed;
    [SerializeField] float maxForce;
    [SerializeField] float minForce;

    Vector2 velocity;
    Vector2 acceleration;

    void Update()
    {
        ApplySteering();   
    }

    void ApplySteering()
    {
        acceleration += CalculateForce();
        velocity += acceleration;
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        transform.position += (Vector3)velocity;
        acceleration = Vector2.zero;
    }

    Vector2 CalculateForce()
    {
        Vector2 desired = target.position - transform.position;
        float distance = desired.magnitude;
        desired.Normalize();
        float strength = Mathf.Clamp(distance, minForce, maxForce);
        desired *= strength;
        Vector2 steer = desired - velocity;
        return steer;
    }

}
