using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text scoreText;

    DataManager dataManager;

    // Assign data manager and update score text on start
    void Start()
    {
        dataManager = Utility.AssignDataManager();
        UpdateScore();
    }

    // Update the score text
    public void UpdateScore()
    {
        scoreText.text = $"Projectiles Used:{dataManager.projectilesUsed}\nEnemies Destroyed: {dataManager.enemiesDefeated}";
    }
}
