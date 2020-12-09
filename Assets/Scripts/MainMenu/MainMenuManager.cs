using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Update()
    {
        // If player hits Esc, exit the game.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1); // Level 1
    }
}
