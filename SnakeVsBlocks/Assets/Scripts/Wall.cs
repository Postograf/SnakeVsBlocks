using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private static Vector2 _scale;

    public static Vector2 Scale => _scale;

    private void Awake()
    {
        if (_scale == Vector2.zero)
            _scale = transform.localScale;
    }
}
