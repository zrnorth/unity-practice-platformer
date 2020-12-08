using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float _platformSpeed = 1f;

    private Transform _platform;
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

    void Start()
    {
        _platform = transform.Find("Platform");
        _pointA = transform.Find("Point A");
        _pointB = transform.Find("Point B");
        _target = _pointA;
    }

    void FixedUpdate()
    {
        float step = _platformSpeed * Time.deltaTime;

        // If we have reached the target, we start moving towards the other target.
        if (Vector2.Distance(_platform.transform.position, _target.position) < .001f)
        {
            SwapTargets();
        }

        _platform.transform.position = Vector2.MoveTowards(_platform.transform.position, _target.position, step);
    }
}
