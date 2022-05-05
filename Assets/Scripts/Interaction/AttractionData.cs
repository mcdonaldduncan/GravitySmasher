using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttractionData
{
    public Vector3 position;
    public float mass;

    public AttractionData(Vector3 _position, float _mass)
    {
        position = _position;
        mass = _mass;
    }
    
}
