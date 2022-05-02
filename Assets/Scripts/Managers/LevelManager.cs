using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int nextLevel;

    // Move to next level, not yet implemented
    public void AdvanceLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    // Reset current level
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
