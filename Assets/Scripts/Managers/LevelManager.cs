using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int nextLevel;

    DataManager dataManager;
    WaitForSeconds delay = new WaitForSeconds(2);

    bool isLoading;

    private void Start()
    {
        dataManager = Utility.AssignDataManager();
        isLoading = false;
    }

    public void CheckAdvance()
    {
        if (isLoading)
            return;

        if (dataManager.enemiesDefeated >= dataManager.enemies.Length)
        {
            StartCoroutine(AdvanceLevelAfterDelay());
            isLoading = true;
        }
    }

    // Move to next level after delay
    IEnumerator AdvanceLevelAfterDelay()
    {
        yield return delay;
        //dataManager.SetPersistentData();
        SceneManager.LoadScene(nextLevel);
    }

    // Reset current level
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
