using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _livesText;
    [SerializeField]
    private Image _gameOverDisplay;

    public void UpdateLivesDisplay(int lives)
    {
        _livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        _gameOverDisplay.gameObject.SetActive(true);
    }
}
