using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private float _defaultResolution;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;

        _defaultResolution = _camera.orthographicSize * _camera.aspect;
    }

    private void Update()
    {
        _camera.orthographicSize = _defaultResolution / _camera.aspect;
    }
}
