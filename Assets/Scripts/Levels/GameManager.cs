using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST,
}

public class GameManager : MonoBehaviour
{
    // Game scalars
    [SerializeField]
    private int _startingLives = 3;

    // Hookups
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Transform _playerSpawnPoint;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private CameraController _cameraController;

    // State vars
    private int _livesRemaining;


    // Start is called before the first frame update
    void Start()
    {
        _livesRemaining = _startingLives;

        // Place the player
        _player.transform.position = _playerSpawnPoint.position;
    }

    private void Update()
    {
        // If player hits Esc, return to the main menu.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // MainMenu
        }
    }

    private IEnumerator GameOver()
    {
        _uiManager.GameOver();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0); // MainMenu
    }

    public void SetPlayerFacing(Direction dir)
    {
        _cameraController.SetOffsetDirection(dir);
    }

    public void PlayerDied()
    {
        _livesRemaining--;
        if (_livesRemaining <= 0)
        {
            StartCoroutine(GameOver());
        }
        _cameraController.ResetCameraSettings();
        _cameraController.ResetCameraPosition();
        _uiManager.UpdateLivesDisplay(_livesRemaining);
        _player.transform.position = _playerSpawnPoint.position;

        // If we disable and reenable the player, we reset all physics and velocity values.
        _player.gameObject.SetActive(false);
        _player.gameObject.SetActive(true);
    }
}
