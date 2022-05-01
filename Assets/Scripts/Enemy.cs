using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Collision behavior for enemies
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
