using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Menu : IStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Menu(UIMainMenuRoot sceneRoot, IBookPageProvider bookPageProvider)
    {
        states[typeof(MainState_Menu)] = new MainState_Menu(this, sceneRoot);
        states[typeof(LevelState_Menu)] = new LevelState_Menu(this, sceneRoot);
        states[typeof(SettingsState_Menu)] = new SettingsState_Menu(this, sceneRoot);

        states[typeof(ShopState_Menu)] = new ShopState_Menu(this, sceneRoot);
        states[typeof(ShopCoverState_Menu)] = new ShopCoverState_Menu(this, sceneRoot);

        states[typeof(AlbumState_Menu)] = new AlbumState_Menu(this, sceneRoot, bookPageProvider);
        states[typeof(AlbumTableState_Menu)] = new AlbumTableState_Menu(this, sceneRoot);
    }

    public void Initialize()
    {
        EnterState(GetState<MainState_Menu>());
    }

    public void Dispose()
    {

    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void EnterState(IState state)
    {
        _currentState?.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
