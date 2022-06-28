using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    [System.NonSerialized] public bool gameOver;
    [System.NonSerialized] public int totalSwarmers;

    public static EndManager instance;

    void Start()
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

    void Update()
    {
        if(gameOver)
        {
            Time.timeScale = 0;
        }
    }
}
