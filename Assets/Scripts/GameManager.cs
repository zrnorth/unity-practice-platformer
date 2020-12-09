using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game scalars
    [SerializeField]
    private int _lives = 3;

    // Hookups
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Transform _playerSpawnPoint;

    // State vars
    private int _livesRemaining;


    // Start is called before the first frame update
    void Start()
    {
        _livesRemaining = _lives;

        // Place the player
        _player.transform.position = _playerSpawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GameOver()
    {
        Debug.Log("Game Over"); // todo
    }

    public void PlayerDied()
    {
        _livesRemaining--;
        Debug.Log("Lives remaining: " + _livesRemaining);
        if (_livesRemaining <= 0)
        {
            GameOver();
        }
        _player.transform.position = _playerSpawnPoint.position;

        // If we disable and reenable the player, we reset all physics and velocity values.
        _player.gameObject.SetActive(false);
        _player.gameObject.SetActive(true);
    }
}
