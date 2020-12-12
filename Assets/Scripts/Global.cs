using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    // Singleton
    public static Global global;

    // Fields
    private int _lives;
    private bool _hasSavedLives = false;

    private void Awake()
    {
        if (!global)
        {
            DontDestroyOnLoad(gameObject);
            global = this;
        }
        else if (global != this)
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerLives(int lives)
    {
        Global.global._lives = lives;
        Global.global._hasSavedLives = true;
    }

    public int GetPlayerLives()
    {
        if (!Global.global._hasSavedLives)
        {
            Debug.LogError("You are loading player lives from the Global store without saving them first!");
        }
        return Global.global._lives;
    }

    public bool HasSavedPlayerLives()
    {
        return Global.global._hasSavedLives;
    }

    public void Reset()
    {
        _hasSavedLives = false;
    }
}
