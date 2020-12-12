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
        if (Global.global.HasSavedPlayerLives())
        {
            _livesRemaining = Global.global.GetPlayerLives();
        }
        else // First level
        {
            _livesRemaining = _startingLives;
        }
        // Place the player
        _player.transform.position = _playerSpawnPoint.position;
        _uiManager.UpdateLivesDisplay(_livesRemaining);
    }

    private void Update()
    {
        // If player hits Esc, return to the main menu.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // MainMenu
        }
    }

    private void OnDestroy()
    {
        if (_livesRemaining > 0)
        {
            Global.global.SavePlayerLives(_livesRemaining);
        }
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
            _uiManager.GameOver();
            Global.global.Reset();
            StartCoroutine(CompletedLevelCoroutine(0));
            return;
        }
        _cameraController.ResetCameraSettings();
        _cameraController.ResetCameraPosition();
        _uiManager.UpdateLivesDisplay(_livesRemaining);
        _player.transform.position = _playerSpawnPoint.position;

        // If we disable and reenable the player, we reset all physics and velocity values.
        _player.gameObject.SetActive(false);
        _player.gameObject.SetActive(true);
    }

    public void CompletedLevel(int nextLevel)
    {
        StartCoroutine(CompletedLevelCoroutine(nextLevel));
    }
    IEnumerator CompletedLevelCoroutine(int nextLevel)
    {
        _player.FreezePlayer();
        _cameraController.CompletedLevel();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextLevel);
    }
}
