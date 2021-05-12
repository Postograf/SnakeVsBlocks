using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private int _health;

    private static Vector2 _scale = Vector2.zero;

    public event UnityAction<int> HeathChanged;
    public static Vector2 Scale => _scale;

    public int Health
    {
        set
        {
            if (_health == 0)
            {
                _health = value;
                HeathChanged?.Invoke(_health);
            }
        }
    }


    private void Awake()
    {
        if (_scale == Vector2.zero)
            _scale = transform.localScale;
    }

    public void Hurt()
    {
        _health--;

        HeathChanged?.Invoke(_health);

        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
