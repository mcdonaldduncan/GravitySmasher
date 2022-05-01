using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Data to be handled by the data manager
    [System.NonSerialized] public GameObject star;
    [System.NonSerialized] public Attractor[] bodies;

    // Cache relevant objects
    void Start()
    {
        bodies = FindObjectsOfType<Attractor>();
        star = GameObject.Find("Star");
    }

    
}
