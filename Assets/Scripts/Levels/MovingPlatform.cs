using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float _platformSpeed = 1f;

    private List<Transform> _riders; // Other items "riding" the platform. They should move with it.

    private Transform _pointA;
    private Transform _pointB;
    private Transform _target;

    private void SwapTargets()
    {
        if (_target.position == _pointA.position)
        {
            _target = _pointB;
        }
        else
        {
            _target = _pointA;
        }
    }

    private void Start()
    {
        _pointA = transform.parent.Find("Point A");
        _pointB = transform.parent.Find("Point B");
        _target = _pointA;
        _riders = new List<Transform>();
    }

    private void FixedUpdate()
    {
        float step = _platformSpeed * Time.deltaTime;

        // If we have reached the target, we start moving towards the other target.
        if (Vector2.Distance(transform.position, _target.position) < .001f)
        {
            SwapTargets();
        }
        Vector2 oldPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(transform.position, _target.position, step);
        Vector2 mvmt = newPos - oldPos;

        transform.position = newPos;

        foreach (Transform rider in _riders)
        {
            if (!rider)// If the rider got destroyed somehow
            {
                _riders.Remove(rider);
                break;
            }

            rider.transform.position = new Vector2(rider.transform.position.x + mvmt.x,
                                                   rider.transform.position.y + mvmt.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _riders.Add(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _riders.Remove(other.transform);
    }
}
