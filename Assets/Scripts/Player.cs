using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Game modifiers
    [SerializeField]
    private float _speed = 90f;
    [SerializeField]
    private float _jumpForce = 2000f;
    [SerializeField]
    private int _jumps = 1;

    // State vars
    private int _numJumpsRemaining;

    // Components
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;

    private void GetInputAndMove()
    {
        Vector2 moment = new Vector2();
        float horiz = Input.GetAxis("Horizontal");
        moment.x = horiz * _speed;

        if (Input.GetKeyDown(KeyCode.Space) && _numJumpsRemaining > 0)
        {
            moment.y = _jumpForce;
            _numJumpsRemaining--;
        }
        _rb.AddForce(moment);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _numJumpsRemaining = _jumps;
    }

    private void Update()
    {
        GetInputAndMove();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Platform")
        {
            _numJumpsRemaining = _jumps;
        }
    }
}
