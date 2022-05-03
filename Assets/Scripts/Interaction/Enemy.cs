using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class Enemy : MonoBehaviour
{
    //DataManager dataManager;
    UIManager uiManager;
    LevelManager levelManager;

    // Assign dataManager and uiManager
    private void Start()
    {
        //dataManager = AssignDataManager();
        uiManager = AssignUIManager();
        levelManager = AssignLevelManager();
    }

    // On trigger enter, destroy gameobject, increment score and call UpdateScore()
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        DataManager.instance.enemiesDefeated++;
        uiManager.UpdateScore();
        levelManager.CheckAdvance();
    }
}
