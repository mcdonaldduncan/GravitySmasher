using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField] Rigidbody starShip;
    [SerializeField] GameObject limitIndicator;
    [SerializeField] float density;
    [SerializeField] float maxDistance;
    [SerializeField] bool attractAll;
    [SerializeField] bool shouldLaunch;
    [SerializeField] bool limitDistance;

    Attractor[] bodies;

    [System.NonSerialized] public Rigidbody rb;

    GameObject Star;
    SphereCollider sphereCollider;
    OptionManager optionManager;

    const float G = 1.67f;
    float radius;
    float volume;
    float mass;

    Dictionary<string, float> bodyType = new Dictionary<string, float>
    {
        {"Neutron Star", 999999999999999.9f },
        {"Black Hole", 999999999999999999999.9f },
        {"Red Giant", 907.185f },
        {"Sun", 1.409f },
        {"Jupiter", 1.3262f },
        {"Saturn", .6871f },
        {"Uranus", 1.270f },
        {"Neptune", 1.638f },
        {"Earth", 5.5136f },
        {"Venus", 5.243f },
        {"Mars", 3.9341f },
        {"Mercury", 5.4291f },
        {"Luna", 3.344f }
    };

    void OnEnable()
    {
        Star = GameObject.Find("Star");
        optionManager = GameObject.Find("OptionManager").GetComponent<OptionManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        sphereCollider = gameObject.GetComponent<SphereCollider>();
    }

    void Start()
    {
        rb.mass = CalculateMass();
        bodies = FindObjectsOfType<Attractor>();

        if (shouldLaunch)
        {
            Vector2 initialForce = InitialVector();
            rb.velocity = initialForce;
            rb.AddForce(initialForce, ForceMode.Impulse);
        }
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

        Vector2 initialForce = Vector2.Perpendicular(Star.transform.position - transform.position) * mass * scale / Vector3.Magnitude(Star.transform.position - transform.position);
        return initialForce;
    }

    void SimulateAttraction()
    {
        if (starShip != null)
        {
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

        if (!attractAll)
            return;

        for (int i = 0; i < bodies.Length; i++)
        {
            if (bodies[i] == null)
                continue;

            if (bodies[i].rb != rb)
            {
                ApplyGravity(bodies[i].rb);
            }
        }
    }

    void ApplyGravity(Rigidbody target)
    {
        Vector2 force = Attract(target);
        target.AddForce(force, ForceMode.Force);
    }

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

    float CalculateMass()
    {
        radius = sphereCollider.radius * transform.localScale.x / 3f;
        volume = (4f / 3f) * Mathf.PI * Mathf.Pow(radius, 3);
        mass = volume * density;
        return mass;
    }

    float GaussianRange(float min, float max)
    {
        return Random.Range(Random.Range(min, max), Random.Range(min, max));
    }

    #region Triggers

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
