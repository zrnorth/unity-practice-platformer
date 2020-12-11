using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    [SerializeField]
    private CameraController _camera;
    [SerializeField]
    private bool _resetToOriginal = false;
    [SerializeField]
    private CameraSettings _newCameraSettings;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_resetToOriginal)
            {
                _camera.ResetCameraSettings();
                return;
            }
            _camera.SetCameraSettings(_newCameraSettings);
        }
    }
}
