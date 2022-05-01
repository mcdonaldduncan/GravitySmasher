using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    // Objects assigned in the inspector
    [SerializeField] Rigidbody starShip;
    [SerializeField] GameObject limitIndicator;

    // Fields designated in the inspector
    [SerializeField] float density;
    [SerializeField] float maxDistance;
    [SerializeField] bool attractAll;
    [SerializeField] bool shouldLaunch;
    [SerializeField] bool limitDistance;

    [System.NonSerialized] public Rigidbody rb;

    // Objects assigned at runtime
    SphereCollider sphereCollider;
    OptionManager optionManager;
    DataManager dataManager;

    // Arbitrarily defined gravitational constant
    const float G = 1.67f;
    float radius;
    float volume;
    float mass;

    // Cache relevant objects and scripts
    void OnEnable()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        optionManager = GameObject.Find("OptionManager").GetComponent<OptionManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        sphereCollider = gameObject.GetComponent<SphereCollider>();
    }

    void Start()
    {
        rb.mass = CalculateMass();

        // Launch the body if shouldLaunch is selected
        if (shouldLaunch)
        {
            Vector2 initialForce = InitialVector();
            rb.velocity = initialForce;
            rb.AddForce(initialForce, ForceMode.Impulse);
        }

        // Create a visual representation of the limited range of attraction
        if (limitDistance)
        {
            GameObject indicator = Instantiate(limitIndicator);
            indicator.transform.position = transform.position;
            indicator.transform.localScale = new Vector3(maxDistance * 2f, maxDistance * 2f, maxDistance * 2f);
        }
    }

    void FixedUpdate()
    {
        SimulateAttraction();
    }

    // Calculate an initial vector to launch at perpendicular to the central star
    Vector2 InitialVector()
    {
        float scale = Random.Range(-1f, 1f);

        if (scale >= 0)
        {
            scale = GaussianRange(5f, 6f);
        }
        else
        {
            scale = GaussianRange(-6f, -5f);
        }

        if (mass > 10f)
        {
            scale = .5f;
        }

        Vector2 initialForce = Vector2.Perpendicular(dataManager.star.transform.position - transform.position) * mass * scale / Vector3.Magnitude(dataManager.star.transform.position - transform.position);
        return initialForce;
    }

    void SimulateAttraction()
    {
        if (starShip != null)
        {
            // If gravitational atraction on the starship should be limited only apply within designated distance
            if (limitDistance)
            {
                if (Vector2.Distance(rb.position, starShip.position) < maxDistance)
                    ApplyGravity(starShip);
            }
            else
            {
                ApplyGravity(starShip);
            }
        }

        // Return if the attractor should only attract the spaceship, else attract each body in list
        if (!attractAll)
            return;

        // Loop through all bodies in the Data Manager
        for (int i = 0; i < dataManager.bodies.Length; i++)
        {
            if (dataManager.bodies[i] == null)
                continue;

            if (dataManager.bodies[i].rb != rb)
            {
                ApplyGravity(dataManager.bodies[i].rb);
            }
        }
    }

    // Method to call application of calculated force
    void ApplyGravity(Rigidbody target)
    {
        Vector2 force = Attract(target);
        target.AddForce(force, ForceMode.Force);
    }

    // Calculate gravitational attraction of one body on another
    Vector2 Attract(Rigidbody toAttract)
    {
        Vector2 force = rb.position - toAttract.position;
        float distance = force.magnitude;
        distance = Mathf.Clamp(distance, .1f, 100f);
        force.Normalize();
        float strength = G * (mass * toAttract.mass) / Mathf.Pow(distance, 2);
        force *= strength;
        return force;
    }

    // Calculate the mass of a planetary body by calculating volume * density
    float CalculateMass()
    {
        radius = sphereCollider.radius * transform.localScale.x / 2f;
        volume = (4f / 3f) * Mathf.PI * Mathf.Pow(radius, 3);
        mass = volume * density;
        return mass;
    }

    // Generate a standard deviation of numbers by taking a random range over two random ranges
    float GaussianRange(float min, float max)
    {
        return Random.Range(Random.Range(min, max), Random.Range(min, max));
    }

    #region Triggers

    // Check trigger collision, destroy on impact with central star or all other objects if selected
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Star"))
            Destroy(gameObject);

        if (!optionManager.destroyOnImpact)
            return;

        if (other.gameObject.transform.localScale.x > gameObject.transform.localScale.x)
            Destroy(gameObject);
    }

    #endregion
}
