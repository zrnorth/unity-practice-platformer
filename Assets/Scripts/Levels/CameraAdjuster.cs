using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    [SerializeField]
    private CameraController _camera;
    [SerializeField]
    private bool _updateCameraSettings = false;
    [SerializeField]
    private CameraSettings _newCameraSettings;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_updateCameraSettings)
            {
                _camera.SetCameraSettings(_newCameraSettings);
            }
        }

        Destroy(gameObject);
    }

}
