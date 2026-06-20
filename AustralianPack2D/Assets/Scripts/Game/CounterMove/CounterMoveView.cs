using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterMoveView : View
{
    public int Count => _count;

    [SerializeField] private TextMeshProUGUI textCount;

    private int _count = 0;

    public void Clear()
    {
        _count = 0;

        textCount.text = _count.ToString();
    }

    public void AddMove(int count)
    {
        _count += count;

        textCount.text = _count.ToString();
    }
}
