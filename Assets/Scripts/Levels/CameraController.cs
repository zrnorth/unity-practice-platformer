using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CameraSettings
{
    // Const scalars
    public float xOffset;
    public float yOffset;
    public float trackingSpeed;
    public bool lockXPosition;
    public bool lockYPosition;
}

public class CameraController : MonoBehaviour
{
    // References
    [SerializeField]
    private Transform _trackingTarget;

    [SerializeField]
    private CameraSettings _cameraSettings;

    // Private vars
    private float _xOffsetOriginal;
    private float _yOffsetOriginal;

    private void Start()
    {
        _xOffsetOriginal = _cameraSettings.xOffset;
        _yOffsetOriginal = _cameraSettings.yOffset;
    }

    void Update()
    {
        float xTarget = _trackingTarget.position.x + _cameraSettings.xOffset;
        float yTarget = _trackingTarget.position.y + _cameraSettings.yOffset;
        // Note that the z does not update. it is fixed in the editor.
        float xNew = transform.position.x;
        float yNew = transform.position.y;
        if (!_cameraSettings.lockXPosition)
        {
            xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * _cameraSettings.trackingSpeed);
        }
        if (!_cameraSettings.lockYPosition)
        {
            yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * _cameraSettings.trackingSpeed);
        }

        transform.position = new Vector3(xNew, yNew, transform.position.z);
    }

    public void SetOffsetDirection(Direction dir)
    {
        // Based on the direction the player is moving, update the scene camera
        // to look ahead the appropriate direction.
        if (dir == Direction.EAST)
        {
            _cameraSettings.xOffset = _xOffsetOriginal;
        }
        else if (dir == Direction.WEST)
        {
            _cameraSettings.xOffset = _xOffsetOriginal * -1;
        }
    }

    public void SetCameraSettings(CameraSettings cameraSettings)
    {
        _cameraSettings = cameraSettings;
    }
}
