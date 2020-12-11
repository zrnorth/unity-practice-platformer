using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Game scalars
    [SerializeField]
    private float _speed = 90f;
    [SerializeField]
    private float _jumpForce = 2000f;
    [SerializeField]
    private int _jumps = 1;
    [SerializeField]
    private float _jumpCooldown = 0.1f;
    [SerializeField]
    private float _cameraOffsetSwapMinSpeed = 5f; // Min speed at which the camera will swap offset directions

    // References
    [SerializeField]
    private GameManager _gameManager;

    // State vars
    private int _numJumpsRemaining;
    private float _nextJump;
    private Direction _facingDir;

    // Component references
    private Rigidbody2D _rb;

    private void GetInputAndMove()
    {
        Vector2 moment = new Vector2();
        float horiz = Input.GetAxis("Horizontal");
        moment.x = horiz * _speed;

        if (Input.GetKeyDown(KeyCode.Space) && _numJumpsRemaining > 0 && Time.time > _nextJump)
        {
            moment.y = _jumpForce;
            _numJumpsRemaining--;
            _nextJump = Time.time + _jumpCooldown;
        }
        _rb.AddForce(moment);
    }

    private void UpdateMovementDirection()
    {
        Direction startOfFrameFacing = _facingDir;
        // TODO: vertical camera facing
        if (_rb.velocity.x == 0)
        {
            return; // Don't send any update if we have stopped.
        }

        else if (_rb.velocity.x > _cameraOffsetSwapMinSpeed)
        {
            _facingDir = Direction.EAST;
        }
        else if (_rb.velocity.x < -1 * _cameraOffsetSwapMinSpeed)
        {
            _facingDir = Direction.WEST;
        }
        if (startOfFrameFacing != _facingDir)
        {
            _gameManager.SetPlayerFacing(_facingDir);
        }

    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _numJumpsRemaining = _jumps;
        _nextJump = Time.time + _jumpCooldown;
    }

    private void FixedUpdate()
    {
        GetInputAndMove();
        UpdateMovementDirection();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Platform")
        {
            _numJumpsRemaining = _jumps;
        }
    }

    public void FreezePlayer()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
