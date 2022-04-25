using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    float rotateX;
    float rotateY;
    float rotateZ;

    void Start()
    {
        rotateX = Random.Range(-10f, 10f);
        rotateY = Random.Range(-10f, 10f);
        rotateZ = Random.Range(-10f, 10f);
    }

    void Update()
    {
        transform.Rotate(rotateX * Time.deltaTime, rotateY * Time.deltaTime, rotateZ * Time.deltaTime);
    }
}
