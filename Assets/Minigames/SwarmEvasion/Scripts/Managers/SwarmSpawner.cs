using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class SwarmSpawner : MonoBehaviour
{
    [SerializeField] GameObject swarmer;

    [SerializeField] float baseDelay;

    float timeBetweenSpawn;
    float timeSinceSpawn;
    
    Vector2 windowLimits;

    
    
    void Start()
    {
        timeSinceSpawn = 0.0f;
        timeBetweenSpawn = baseDelay;
        windowLimits = FindWindowLimits();
    }

    void Update()
    {
        
    }

    void Spawn()
    {
        Vector2 startPosition = new Vector2(Random.Range(-windowLimits.x, windowLimits.x), Random.Range(-windowLimits.y, windowLimits.y));
        GameObject newSwarmer = Instantiate(swarmer);
        
    }

    void SpawnTimer()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn > timeBetweenSpawn)
        {

        }
    }

    
}
