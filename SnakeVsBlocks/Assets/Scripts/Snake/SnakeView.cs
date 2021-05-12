using TMPro;

using UnityEngine;

[RequireComponent(typeof(Snake))]
public class SnakeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;

    private Snake _snake;

    private void Awake()
    {
        _snake = GetComponent<Snake>();
    }

    private void OnEnable()
    {
        _snake.TailChanged += OnTailChanged;
    }

    private void OnDisable()
    {
        _snake.TailChanged -= OnTailChanged;
    }

    private void OnTailChanged(int count)
    {
        _view.text = count.ToString();
    }
}
