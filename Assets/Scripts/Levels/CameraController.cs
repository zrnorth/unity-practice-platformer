﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // References
    [SerializeField]
    private Transform _trackingTarget;

    // Const scalars
    [SerializeField]
    private float _xOffset = 0f;
    [SerializeField]
    private float _yOffset = 0f;
    [SerializeField]
    private float _trackingSpeed = 1f;
    [SerializeField]
    private bool _lockXPosition = false;
    [SerializeField]
    private bool _lockYPosition = false;


    void Update()
    {
        float xTarget = _trackingTarget.position.x + _xOffset;
        float yTarget = _trackingTarget.position.y + _yOffset;
        // Note that the z does not update. it is fixed in the editor.
        float xNew = transform.position.x;
        float yNew = transform.position.y;
        if (!_lockXPosition)
        {
            xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * _trackingSpeed);
        }
        if (!_lockYPosition)
        {
            yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * _trackingSpeed);
        }

        transform.position = new Vector3(xNew, yNew, transform.position.z);
    }
}