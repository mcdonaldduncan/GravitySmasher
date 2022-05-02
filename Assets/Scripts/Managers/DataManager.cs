using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Data to be handled by the data manager
    [System.NonSerialized] public GameObject star;
    [System.NonSerialized] public Attractor[] bodies;
    [System.NonSerialized] public int enemiesDefeated;
    [System.NonSerialized] public int projectilesUsed;

    // Cache relevant objects and assign saved or default values
    void OnEnable()
    {
        bodies = FindObjectsOfType<Attractor>();
        star = GameObject.Find("Star");
        enemiesDefeated = PlayerPrefs.GetInt("EnemiesDefeated", 0);
        projectilesUsed = PlayerPrefs.GetInt("ProjectileCount", 0);
    }

    // Save persistent data to PLayerPrefs
    public void SetPersistentData()
    {
        PlayerPrefs.SetInt("EnemiesDefeated", enemiesDefeated);
        PlayerPrefs.SetInt("ProjectileCount", projectilesUsed);
    }
    
}
