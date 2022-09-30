using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    /// <summary>
    /// Used only on rotate island gameobject
    /// </summary>
    [SerializeField] private float _speed = 90f;
    private Transform _myTransform;
    
    private void Awake()
    {
        _myTransform = transform;
    }

    private void Update()
    {
        _myTransform.RotateAround(_myTransform.position, _myTransform.up, Time.deltaTime * _speed);
    }
}
