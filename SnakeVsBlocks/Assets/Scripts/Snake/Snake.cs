using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TailGenerator), typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] private int _startCount;
    [SerializeField] private float _speed;
    [SerializeField] private float _tailSpringing;

    public event UnityAction<int> TailChanged;
    
    private List<TailSegment> _tail;
    private TailGenerator _tailGenerator;
    private SnakeInput _snakeInput;

    private void OnEnable()
    {
        _head.BlockHurted += OnBlockHurted;
        _head.BonusCollected += OnBonusCollected;
    }

    private void OnDisable()
    {
        _head.BlockHurted -= OnBlockHurted;
        _head.BonusCollected -= OnBonusCollected;
    }

    private void Start()
    {
        _tailGenerator = GetComponent<TailGenerator>();
        _tail = _tailGenerator.Generate(_startCount);

        TailChanged?.Invoke(_tail.Count);

        _snakeInput = GetComponent<SnakeInput>();
    }

    private void FixedUpdate()
    {
        Move(_head.transform.position + _head.transform.up * _speed * Time.fixedDeltaTime);

        _head.transform.up = _snakeInput.GetDirectionToClick(_head.transform.position);
    }

    private void Move(Vector2 nextPoint)
    {
        var previousPosition = _head.transform.position;

        foreach (var segment in _tail)
        {
            var tempPosition = segment.transform.position;
            segment.transform.position = Vector2.Lerp(tempPosition, previousPosition, _tailSpringing * Time.fixedDeltaTime);
            previousPosition = tempPosition;
        }

        _head.Move(nextPoint);
    }

    private void OnBlockHurted()
    {
        var tail = _tail[_tail.Count - 1];
        _tail.Remove(tail);

        tail.transform.parent = null;
        Destroy(tail.gameObject);

        TailChanged?.Invoke(_tail.Count);

        if(_tail.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnBonusCollected(int bonusSize)
    {
        _tail.AddRange(_tailGenerator.Generate(bonusSize));
        TailChanged?.Invoke(_tail.Count);
    }
}
