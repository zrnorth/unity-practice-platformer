using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraSettings
{
    // Const scalars
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float trackingSpeed = 1f;
    public bool lockXPosition = false;
    public bool lockYPosition = false;

    override public string ToString()
    {
        return xOffset + " " +
               yOffset + " " +
               trackingSpeed + " " +
               lockXPosition + " " +
               lockYPosition;
    }
}

public class CameraController : MonoBehaviour
{
    // References
    [SerializeField]
    private Transform _trackingTarget;

    [SerializeField]
    private CameraSettings _defaultCameraSettings;

    // Private vars
    private CameraSettings _currentCameraSettings;
    private Transform _originalCameraPosition;

    private void Start()
    {
        ResetCameraSettings();
        _originalCameraPosition = transform;
    }

    void Update()
    {
        float xTarget = _trackingTarget.position.x + _currentCameraSettings.xOffset;
        float yTarget = _trackingTarget.position.y + _currentCameraSettings.yOffset;
        // Note that the z does not update. it is fixed in the editor.
        float xNew = transform.position.x;
        float yNew = transform.position.y;
        if (!_currentCameraSettings.lockXPosition)
        {
            xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * _currentCameraSettings.trackingSpeed);
        }
        if (!_currentCameraSettings.lockYPosition)
        {
            yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * _currentCameraSettings.trackingSpeed);
        }

        transform.position = new Vector3(xNew, yNew, transform.position.z);
    }

    public void SetOffsetDirection(Direction dir)
    {
        // Based on the direction the player is moving, update the scene camera
        // to look ahead the appropriate direction.
        if (dir == Direction.EAST)
        {
            _currentCameraSettings.xOffset = Mathf.Abs(_currentCameraSettings.xOffset);
        }
        else if (dir == Direction.WEST)
        {
            _currentCameraSettings.xOffset = Mathf.Abs(_currentCameraSettings.xOffset) * -1;
        }
    }

    public CameraSettings GetCameraSettings()
    {
        return _currentCameraSettings;
    }
    public void SetCameraSettings(CameraSettings cameraSettings)
    {
        _currentCameraSettings = cameraSettings;
    }

    public IEnumerator SetCameraSettingsTemporarily(CameraSettings cameraSettings, float time)
    {
        CameraSettings oldCameraSettings = _currentCameraSettings;
        SetCameraSettings(cameraSettings);
        yield return new WaitForSeconds(time);
        SetCameraSettings(oldCameraSettings);
    }

    public void ResetCameraSettings()
    {
        SetCameraSettings(_defaultCameraSettings);
    }

    public void ResetCameraPosition()
    {
        transform.position = _originalCameraPosition.position;
    }

    public void CompletedLevel()
    {
        _currentCameraSettings.trackingSpeed = 0f;
        GetComponent<Camera>().backgroundColor = Color.white;
    }
}
