using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailGenerator : MonoBehaviour
{
    [SerializeField] private TailSegment _tailSegmentPrefab;

    public List<TailSegment> Generate(int count)
    {
        var tail = new List<TailSegment>();

        for (int i = 0; i < count; i++)
        {
            tail.Add(Instantiate(_tailSegmentPrefab, transform));
        }

        return tail;
    }
}
