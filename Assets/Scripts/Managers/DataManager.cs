using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    // Data to be handled by the data manager
    [System.NonSerialized] public GameObject star;
    [System.NonSerialized] public Attractor[] bodies;
    [System.NonSerialized] public Enemy[] enemies;
    [System.NonSerialized] public int enemiesDefeated;
    [System.NonSerialized] public int projectilesUsed;
    [System.NonSerialized] public AttractionData[] attractors;

    public static DataManager instance { get; private set; }

    // Singleton instantiator
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Cache relevant objects and assign saved or default values
    void OnEnable()
    {
        bodies = FindObjectsOfType<Attractor>();
        enemies = FindObjectsOfType<Enemy>();
        star = GameObject.Find("Star");
        enemiesDefeated = PlayerPrefs.GetInt("EnemiesDefeated", 0);
        projectilesUsed = PlayerPrefs.GetInt("ProjectileCount", 0);
    }

    private void Start()
    {
        if (bodies != null)
        {
            attractors = bodies.Select(q => new AttractionData(q.transform.position, q.mass)).ToArray();
        }
    }


    // Save persistent data to PLayerPrefs, unused for now
    public void SetPersistentData()
    {
        PlayerPrefs.SetInt("EnemiesDefeated", enemiesDefeated);
        PlayerPrefs.SetInt("ProjectileCount", projectilesUsed);
    }
    
}
