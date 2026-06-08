using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBattleModel
{
    private readonly ISpawnerChickenListener _spawnerChickenListener;
    private readonly IChooseChickenListener _chooseChickenListener;
    private List<IChickenUnit> _chickens = new();

    private readonly System.Random _random = new();

    private IEnumerator _eventsCoroutine;
    private IEnumerator _leaderCoroutine;

    private ChickenType _chickenTypeChoose;
    private ChickenType _chickenTypeWinner;

    private readonly HashSet<IChickenUnit> _finished = new();

    private bool _hasWinner = false;

    private IChickenUnit _leader;

    public ChickenBattleModel(
        ISpawnerChickenListener spawnerChickenListener,
        IChooseChickenListener chooseChickenListener)
    {
        _spawnerChickenListener = spawnerChickenListener;
        _chooseChickenListener = chooseChickenListener;

        _spawnerChickenListener.OnSpawnChickens += SetChickens;
        _chooseChickenListener.OnChoose += SetChooseChicken;
    }

    public void Dispose()
    {
        _spawnerChickenListener.OnSpawnChickens -= SetChickens;
        _chooseChickenListener.OnChoose -= SetChooseChicken;

        Clear();
    }

    public void CheckWinner()
    {
        if (_finished.Count != _chickens.Count)
            return;

        if (_chickenTypeChoose == _chickenTypeWinner)
            OnWin?.Invoke();
        else
            OnLose?.Invoke();
    }

    public void StartGame()
    {
        for (int i = 0; i < _chickens.Count; i++)
        {
            if (_chickens[i] == null) continue;
            _chickens[i].SetRun();
        }

        StartEventsLoop();
    }

    public void StopBattle()
    {
        StopEventsLoop();

        for (int i = 0; i < _chickens.Count; i++)
        {
            var chicken = _chickens[i];

            if (chicken == null)
                continue;

            chicken.OnEndMove -= EndMove;
            chicken.SetIdle();

            _finished.Add(chicken);
        }

        _hasWinner = false;

        _leader = null;

        _chickenTypeChoose = ChickenType.None;
        _chickenTypeWinner = ChickenType.None;
    }

    private void SetChooseChicken(ChickenType chickenType)
    {
        _chickenTypeChoose = chickenType;
    }

    private void SetChickens(List<IChickenUnit> chickens)
    {
        Clear();

        _chickens = chickens;
        _finished.Clear();

        for (int i = 0; i < _chickens.Count; i++)
        {
            if (_chickens[i] == null) continue;
            _chickens[i].OnEndMove += EndMove;
        }
    }

    private void EndMove(IChickenUnit chicken)
    {
        if (chicken == null) return;

        if (_finished.Contains(chicken)) return;

        _finished.Add(chicken);

        if (!_hasWinner)
        {
            _hasWinner = true;
            _chickenTypeWinner = chicken.Type;
        }

        chicken.OnEndMove -= EndMove;
        chicken.SetIdle();

        if (_finished.Count == _chickens.Count)
        {
            StopEventsLoop();
            OnEndGame?.Invoke();
        }
    }

    private void StartEventsLoop()
    {
        if (_eventsCoroutine != null)
            return;

        _eventsCoroutine = EventsLoop();
        Coroutines.Start(_eventsCoroutine);

        _leaderCoroutine = LeaderLoop();
        Coroutines.Start(_leaderCoroutine);
    }

    private void StopEventsLoop()
    {
        if (_eventsCoroutine != null)
        {
            Coroutines.Stop(_eventsCoroutine);
            _eventsCoroutine = null;
        }

        if (_leaderCoroutine != null)
        {
            Coroutines.Stop(_leaderCoroutine);
            _leaderCoroutine = null;
        }
    }

    // =========================
    // 🎲 EVENTS LOOP (random)
    // =========================
    private IEnumerator EventsLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_random.Next(800, 2500) / 1000f);

            if (_chickens.Count == 0)
                continue;

            var chicken = _chickens[_random.Next(_chickens.Count)];

            if (chicken == null)
                continue;

            if (_finished.Contains(chicken))
                continue;

            TriggerRandomEvent(chicken);
        }
    }

    // =========================
    // 🧠 LEADER LOOP (fixed 0.1s)
    // =========================
    private IEnumerator LeaderLoop()
    {
        var wait = new WaitForSeconds(0.1f);

        while (true)
        {
            RecalculateLeader();
            yield return wait;
        }
    }

    private void TriggerRandomEvent(IChickenUnit chicken)
    {
        if (_finished.Contains(chicken))
            return;

        if (_random.Next(0, 100) < 30)
            chicken.ActivateGoodState();
        else
            chicken.ActivateBadState();
    }

    // =========================
    // 🧠 LEADER LOGIC
    // =========================
    private void RecalculateLeader()
    {
        if (_hasWinner || _chickens.Count == 0)
            return;

        IChickenUnit best = null;
        float bestX = float.MinValue;

        for (int i = 0; i < _chickens.Count; i++)
        {
            var c = _chickens[i];

            if (c == null)
                continue;

            if (_finished.Contains(c))
                continue;

            float x = c.LocalPosition.x;

            if (x > bestX)
            {
                bestX = x;
                best = c;
            }
        }

        if (best != _leader)
        {
            _leader = best;
            OnLeaderChanged?.Invoke(_leader);
        }
    }

    private void Clear()
    {
        StopEventsLoop();

        for (int i = 0; i < _chickens.Count; i++)
        {
            if (_chickens[i] != null)
                _chickens[i].OnEndMove -= EndMove;
        }

        _hasWinner = false;
        _chickenTypeChoose = ChickenType.None;
        _chickenTypeWinner = ChickenType.None;

        _chickens.Clear();
        _finished.Clear();

        _leader = null;
    }

    #region Output

    public event Action OnWin;
    public event Action OnLose;
    public event Action OnEndGame;
    public event Action<IChickenUnit> OnLeaderChanged;

    #endregion
}
