using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public Vector2 GetDirectionToClick(Vector2 headPosition)
    {
        var mousePosition = Input.mousePosition;

        mousePosition = _camera.ScreenToViewportPoint(mousePosition);
        mousePosition.y = 1;
        mousePosition = _camera.ViewportToWorldPoint(mousePosition);

        var direction = new Vector2(mousePosition.x - headPosition.x, mousePosition.y - headPosition.y);

        return direction;
    }
}
