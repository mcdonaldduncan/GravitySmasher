using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    DataManager dataManager;
    UIManager uiManager;

    // Assign datamanager and uimanager
    private void Start()
    {
        dataManager = Utility.AssignDataManager();
        uiManager = Utility.AssignUIManager();
    }

    // On trigger enter, destroy gameobject, increment score and call UpdateScore()
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        dataManager.enemiesDefeated++;
        uiManager.UpdateScore();
    }
}
