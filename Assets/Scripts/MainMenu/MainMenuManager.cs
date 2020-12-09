using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1); // Level 1
    }
}
