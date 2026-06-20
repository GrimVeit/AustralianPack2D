using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterMovePresenter : ICounterMoveProvider
{
    private readonly CounterMoveView _view;

    public CounterMovePresenter(CounterMoveView view)
    {
        _view = view;
    }

    public int Count => _view.Count;

    public void Clear() => _view.Clear();
    public void AddMove(int value) => _view.AddMove(value);
}

public interface ICounterMoveProvider
{
    public int Count { get; }

    public void Clear();
    public void AddMove(int value);
}
