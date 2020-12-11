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
    private float _jumpingGravityReduction = 4f;
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
    private bool _didJump;
    private Direction _facingDir;

    private Vector2 _moment = new Vector2(0, 0);

    // Component references
    private Rigidbody2D _rb;

    private float _originalGravityScale;

    private void ApplyForces()
    {
        // Apply jump forces. Double-jumps should have the same height, so reset
        // the y velocity before adding the moment.
        if (_didJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _didJump = false;
        }
        _rb.AddForce(_moment);
        _moment = new Vector2(0, 0);
    }

    private void GetInputAndApplyMoment()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        _moment.x += horiz * _speed;

        if (Input.GetKeyDown(KeyCode.Space) && _numJumpsRemaining > 0 && Time.time > _nextJump)
        {
            _moment.y += _jumpForce;
            _numJumpsRemaining--;
            _nextJump = Time.time + _jumpCooldown;
            _didJump = true;
        }

        // Reduce gravity while jump is held, so that the player can more
        // granularly choose how high to jump.
        if (Input.GetKey(KeyCode.Space) && this._rb.velocity.y > 0f)
        {
            _rb.gravityScale = _originalGravityScale / _jumpingGravityReduction;
        }
        else
        {
            _rb.gravityScale = _originalGravityScale;
        }
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
        _originalGravityScale = _rb.gravityScale;
    }

    private void Update()
    {
        GetInputAndApplyMoment();
    }

    private void FixedUpdate()
    {
        ApplyForces();
        UpdateMovementDirection();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Platform" && _rb.velocity.y <= 0)
        {
            _numJumpsRemaining = _jumps;
        }
    }

    public void FreezePlayer()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
