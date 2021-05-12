using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTracker : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private float _offsetY;

    private void FixedUpdate()
    {
        if(_snakeHead != null)
            transform.position = GetTargetPosition();
    }

    private Vector3 GetTargetPosition() => 
        new Vector3(
            transform.position.x, 
            _snakeHead.transform.position.y + _offsetY, 
            transform.position.z
        );
}
