using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;

    private int _bonusSize;

    public int BonusSize
    {
        get => _bonusSize;
        set
        {
            if (_bonusSize == 0)
            {
                _bonusSize = value;
                _view.text = _bonusSize.ToString();
            }
        }
    }

    public int Collect()
    {
        Destroy(gameObject);
        return _bonusSize;
    }
}
