using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 1f;
    [SerializeField]
    private int _sceneToLoad = 0;
    [SerializeField]
    private GameManager _gameManager;

    private Transform _outer, _inner;

    void Start()
    {
        _outer = GameObject.Find("White Square").GetComponent<Transform>();
        _inner = GameObject.Find("Black Square").GetComponent<Transform>();
    }
    void Update()
    {
        // The goal spins slightly to catch the eye
        _outer.transform.Rotate(new Vector3(0, 0, _rotateSpeed * Time.deltaTime));
        _inner.transform.Rotate(new Vector3(0, 0, -1 * _rotateSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _gameManager.CompletedLevel(_sceneToLoad);
        }
    }
}
