using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchLine;
using System;

public class RepositionCamera : MonoBehaviour
{
    [SerializeField] private float _visibleExtraWidthInPercentage;
    [SerializeField] private Camera _camera;
    [SerializeField] private string _cameraTag = "GameCamera";
    private Vector3 _leftBottomCorner;
    private Vector3 _rightTopCorner;
    private Vector3 _applicationSize;
    private float _currentWidth;
    private float _currentHeight;
    private float _wantedWidth;
    private float _ortographicSize;
    private void Start()
    {
        _camera = GetCamera();
        _applicationSize = new Vector2(Screen.width, Screen.height);
        StartRepositioningCamera();
    }

    private Camera GetCamera()
    {
        var cameras = Camera.allCameras;
        foreach (var cam in cameras)
        {
            if(cam.CompareTag(_cameraTag))
            {
                return cam;
            }
        }        
        return Camera.main;
    }

    private void StartRepositioningCamera()
    {
        _camera.transform.position = new Vector3(((float)Board.Width - 1) / 2, (float)Board.Height / 2, -17);
        UpdateCorners();
        _currentWidth = _rightTopCorner.x - _leftBottomCorner.x;
        while (_currentWidth <= Board.Width+ (Board.Width * _visibleExtraWidthInPercentage))
        {
            _camera.transform.position -= new Vector3(0, 0, 0.5f);
            UpdateCorners();
            _currentWidth = _rightTopCorner.x - _leftBottomCorner.x;
        }

        _currentHeight = _rightTopCorner.y - _leftBottomCorner.y;
        while (_currentHeight < Board.Height + 10)
        {
            _camera.transform.position -= new Vector3(0, 0, 0.5f);
            UpdateCorners();
            _currentHeight = _rightTopCorner.y - _leftBottomCorner.y;
        }        
    }

    private void UpdateCorners()
    {
        _leftBottomCorner = _camera.ScreenToWorldPoint(new Vector3(0, 0, -_camera.transform.position.z));
        _rightTopCorner = _camera.ScreenToWorldPoint(new Vector3(_applicationSize.x, _applicationSize.y, -_camera.transform.position.z));
    }
}
